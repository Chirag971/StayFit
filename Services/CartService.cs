using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StayFitz.Models;
using StayFitz.Models.StayFitz.Models;

namespace StayFitz.Services
{
    public static class CartService
    {
        public static ObservableCollection<ShopItem> CartItems { get; set; } = new();
        public static event Action CartUpdated;
        public static int CartCount => CartItems.Sum(x => x.Quantity);

        public static void AddToCart(ShopItem item)
        {
            var existing = CartItems.FirstOrDefault(x => x.Title == item.Title);

            if (existing != null)
            {
                existing.Quantity += item.Quantity; // 🔥 SAME OBJECT UPDATED
            }
            else
            {
                CartItems.Add(new ShopItem
                {
                    Title = item.Title,
                    BrandName = item.BrandName,
                    Price = item.Price,
                    ImageSource = item.ImageSource,
                    Quantity = item.Quantity
                });
            }

            CartUpdated?.Invoke();
        }
        public static void RemoveFromCart(ShopItem item)
        {
            if (CartItems.Contains(item))
                CartItems.Remove(item);


            CartUpdated?.Invoke();
            NotifyChange(); 
        }
        public static void NotifyChange()
        {
            CartUpdated?.Invoke();
        }
    }
}
