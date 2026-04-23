using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using StayFitz.Models;
using StayFitz.Models.StayFitz.Models;
using StayFitz.Services;
using StayFitz.Views;

namespace StayFitz.ViewModel
{
    public class ShoptDetailViewModel : INotifyPropertyChanged
    {
        // Product
        public ShopItem Product { get; set; }

         
        public ICommand AddAndOpenCartCommand { get; }
        //Bindings
        public string Title => Product.Title;
        public string BrandName => Product.BrandName;
        public string ImageSource => Product.ImageSource;
        public string FullDescription => Product.FullDescription;
        public string PriceText => $"₹{Product.Price * Quantity:N0}";
        // Quantity
        private int _quantity = 1;
        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
                OnPropertyChanged(nameof(PriceText)); // 🔥IMPORTANT
            }
        }

        // Commands
        public ICommand IncreaseCommand { get; }
        public ICommand DecreaseCommand { get; }
        public ICommand AddToCartCommand { get; }
        public ICommand BuyCommand { get; }

        

        // Constructor
        public ShoptDetailViewModel(ShopItem item)
        {
            BuyCommand = new Command(async () =>
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Order Placed",
                    $"Your total is",
                    "OK");
            });
            AddAndOpenCartCommand = new Command<ShopItem>(async (item) =>
            {
                if (item == null) return;

                // 🔥 Add to cart
                var existing = CartService.CartItems
                    .FirstOrDefault(x => x.Title == item.Title);

                if (existing != null)
                {
                    existing.Quantity++;
                }
                else
                {
                    CartService.CartItems.Add(new ShopItem
                    {
                        Title = item.Title,
                        BrandName = item.BrandName,
                        Price = item.Price,
                        ImageSource = item.ImageSource,
                        Quantity = 1
                    });
                }

                CartService.NotifyChange(); // 🔥 update UI

                // 🔥 Open Cart Page
                await Shell.Current.GoToAsync(nameof(CartPage));
            });
            Product = item;

            // Check if already in cart
            var existing = CartService.CartItems
                .FirstOrDefault(x => x.Title == Product.Title);

            if (existing != null)
                Quantity = existing.Quantity;

            // Increase
            IncreaseCommand = new Command(() =>
            {
                Quantity++;
                UpdateCart();
            });

            // Decrease
            DecreaseCommand = new Command(() =>
            {
                if (Quantity > 1)
                {
                    Quantity--;
                    UpdateCart();
                }
            });

            //  Add To Cart
            //AddToCartCommand = new Command(async () =>
            //{
            //    UpdateCart();

            //    await Application.Current.MainPage.DisplayAlert(
            //        "Cart",
            //        $"{Product.Title} added to cart ",
            //        "OK");
            //});

            AddToCartCommand = new Command(async () =>
            {
                UpdateCart();

                await Shell.Current.GoToAsync(nameof(CartPage));
            });


            //  Buy Now
            BuyCommand = new Command(async () =>
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Success",
                    $"{Product.Title} purchased successfully ",
                    "OK");
            });

            //  Sync with CartPage
            CartService.CartUpdated += () =>
            {
                var itemInCart = CartService.CartItems
                    .FirstOrDefault(x => x.Title == Product.Title);

                Quantity = itemInCart?.Quantity ?? 1;
            };
        }

        //  Update Cart
        private void UpdateCart()
        {
            var existing = CartService.CartItems
                .FirstOrDefault(x => x.Title == Product.Title);

            if (existing != null)
            {
                existing.Quantity = Quantity;
            }
            else
            {
                CartService.CartItems.Add(new ShopItem
                {
                    Title = Product.Title,
                    BrandName = Product.BrandName,
                    Price = Product.Price,
                    ImageSource = Product.ImageSource,
                    Quantity = Quantity
                });
            }

            CartService.NotifyChange(); //  IMPORTANT
        }

        //  Property Changed
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}