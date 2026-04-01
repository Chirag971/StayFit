using System.ComponentModel;
using System.Windows.Input;
using StayFitz.Models;

namespace StayFitz.ViewModel
{
    public class ShoptDetailViewModel : INotifyPropertyChanged   // ✅ FIX
    {
        public ShopItem Product { get; set; }

        public string Title => Product.Title;
        public string BrandName => Product.BrandName;
        public string PriceText => $"₹{Product.Price * Quantity}";
        public string ImageSource => Product.ImageSource;
        public string FullDescription => Product.FullDescription;
        public string ShortDescription => Product.ShortDescription;

        private int _quantity = 1;
        public int Quantity
        {
            get => _quantity;
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;

                    OnPropertyChanged(nameof(Quantity));
                    OnPropertyChanged(nameof(PriceText));  // 🔥 IMPORTANT
                }
            }
        }

        public ICommand IncreaseCommand { get; }
        public ICommand DecreaseCommand { get; }
        public ICommand AddToCartCommand { get; }

        public ShoptDetailViewModel(ShopItem item)
        {
            Product = item;

            IncreaseCommand = new Command(() =>
            {
                Quantity++;
            });

            DecreaseCommand = new Command(() =>
            {
                if (Quantity > 1)
                    Quantity--;
            });

            AddToCartCommand = new Command(async () =>
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Cart",
                    $"{Product.Title} added with quantity {Quantity}",
                    "OK");
            });
        }

        // ✅ Proper implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}