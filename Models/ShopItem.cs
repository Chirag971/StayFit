    using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StayFitz.Models
{

    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using global::StayFitz.Services;

    namespace StayFitz.Models
    {
        public class ShopItem : INotifyPropertyChanged
        {
            private bool _addedToCart;
            private int _quantity = 1;

            public string Title { get; set; }
            public string BrandName { get; set; }
            public string PriceText { get; set; }
            public string ImageSource { get; set; }
            public string Section { get; set; }
            public string ShortDescription { get; set; }
            public string FullDescription { get; set; }

            public decimal Price { get; set; }

            // 🔥 FIXED Quantity (IMPORTANT)
            public int Quantity
            {
                get => _quantity;
                set
                {
                    if (_quantity != value)
                    {
                        _quantity = value;
                        OnPropertyChanged();
                        OnPropertyChanged(nameof(TotalPrice));
                        OnPropertyChanged(nameof(TotalPriceText));
                        OnPropertyChanged(nameof(PriceTextWithQty));
                       CartService.NotifyChange(); // ✅ WORKS NOW
                    }
                }
            }

            // 🔥 Calculations
            public decimal TotalPrice => Price * Quantity;
            public string TotalPriceText => $"₹{TotalPrice:N0}";
            public string PriceTextWithQty => $"₹{Price:N0} x {Quantity}";

            public bool AddedToCart
            {
                get => _addedToCart;
                set
                {
                    _addedToCart = value;
                    OnPropertyChanged();
                }
            }

            // 🔥 INotifyPropertyChanged
            public event PropertyChangedEventHandler PropertyChanged;

            protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
