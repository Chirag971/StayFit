using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StayFitz.Models
{

    public class ShopItem : INotifyPropertyChanged
    {
        private bool _addedToCart;

        public string Title { get; set; }
        public string BrandName { get; set; }
        public string PriceText { get; set; }
        public string ImageSource { get; set; }
        public double Rating { get; set; }
        public string Section { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }

        public decimal Price { get; set; }
        public bool AddedToCart
        {
            get => _addedToCart;
            set
            {
                _addedToCart = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
