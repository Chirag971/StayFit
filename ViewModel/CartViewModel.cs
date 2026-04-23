using StayFitz.Services;

namespace StayFitz.ViewModel
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Input;
    using global::StayFitz.Models.StayFitz.Models;

    namespace StayFitz.ViewModel
    {
        public class CartViewModel
        {
            public ObservableCollection<ShopItem> CartItems => CartService.CartItems;
            public ShopItem Product { get; set; }
            public ICommand IncreaseCommand { get; }
            public ICommand DecreaseCommand { get; }
            public ICommand RemoveCommand { get; }

            public ICommand CheckoutCommand { get; }
            public CartViewModel()
            {
                RemoveCommand = new Command<ShopItem>((item) =>
                {
                    if (item != null)
                    {
                        CartItems.Remove(item);

                        //  FORCE UI UPDATE
                        OnPropertyChanged(nameof(GrandTotalText));
                    }
                });

                // 🔥THIS LINE FIXES EVERYTHING
                CartItems.CollectionChanged += (s, e) =>
                {
                    OnPropertyChanged(nameof(GrandTotalText));
                };
                CartService.CartUpdated += () =>
                {
                    OnPropertyChanged(nameof(CartItems));
                    OnPropertyChanged(nameof(GrandTotalText));
                };
                CheckoutCommand = new Command(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Order Placed",
                        $"Your total is {GrandTotalText}",
                        "OK");
                });

                //IncreaseCommand = new Command(() =>
                //{
                //    var item = CartService.CartItems
                //        .FirstOrDefault(x => x.Title == Product.Title);

                //    if (item != null)
                //        item.Quantity++;
                //});
                IncreaseCommand = new Command<ShopItem>((item) =>
                {
                    if (item != null)
                    {
                        item.Quantity++;

                        OnPropertyChanged(nameof(CartItems));
                        OnPropertyChanged(nameof(GrandTotalText));

                       // CartService.NotifyChange();
                    }
                });
                DecreaseCommand = new Command<ShopItem>((item) =>
                {
                    if (item != null && item.Quantity > 1)
                    {
                        item.Quantity--;

                        OnPropertyChanged(nameof(CartItems));
                        OnPropertyChanged(nameof(GrandTotalText));

                        CartService.NotifyChange();
                    }
                });
                RemoveCommand = new Command<ShopItem>((item) =>
                {
                    CartService.RemoveFromCart(item);

                    OnPropertyChanged(nameof(CartItems));
                    OnPropertyChanged(nameof(GrandTotalText));
                });
            }

            public event PropertyChangedEventHandler PropertyChanged;

            void OnPropertyChanged(string name)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }

            // Grand Total
            public decimal GrandTotal => CartItems.Sum(x => x.TotalPrice);
            public string GrandTotalText => $"₹{GrandTotal:N0}";
        }
    }
}
