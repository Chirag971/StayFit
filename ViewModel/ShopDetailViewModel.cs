using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using StayFitz.Models;

namespace StayFitz.ViewModel
{
    public class ShoptDetailViewModel
    {
        public ShopItem Product { get; set; }

        public string Title => Product.Title;
        public string BrandName => Product.BrandName;
        public string PriceText => Product.PriceText;
        public string ImageSource => Product.ImageSource;
        public string FullDescription => Product.FullDescription;
        public string ShortDescription => Product.ShortDescription;

        private int _quantity = 1;
        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        public ICommand IncreaseCommand { get; }
        public ICommand DecreaseCommand { get; }
        public ICommand AddToCartCommand { get; }

        public ShoptDetailViewModel(ShopItem item)
        {
            Product = item;

            IncreaseCommand = new Command(() => Quantity++);
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

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
