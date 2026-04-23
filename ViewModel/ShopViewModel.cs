using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using StayFitz.Models;
using StayFitz.Models.StayFitz.Models;
using StayFitz.Services;
using StayFitz.Utilities;
using StayFitz.Views;

namespace StayFitz.ViewModel
{
    public class ShopViewModel 
    {
        private readonly ShopDataService _service;
        public ICommand OpenDetailCommand { get; }
        public ICommand AddToCartAndOpenCommand { get; }
        public ICommand FilterCommand { get; }
        public ObservableCollection<ShopCategory> Categories { get; set; }
        //public ObservableCollection<ShopItem> FeaturedItems { get; set; }
        //public ObservableCollection<ShopItem> GearItems { get; set; }
        public ObservableCollection<ShopItem> AllItems { get; set; }   // all products
        public ObservableCollection<ShopItem> FilteredItems { get; set; } // filtered UI
        private string selectedCategory;
        public string SelectedCategory
        {
            get => selectedCategory;
            set
            {
                selectedCategory = value;
              
            }
        }
        public int CartCount => CartService.CartItems.Sum(x => x.Quantity);
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public ShopViewModel()
        {
            AddToCartAndOpenCommand = new Command<ShopItem>(async (item) =>
            {
                if (item == null) return;

                // 🔥 Check if already exists  
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

                CartService.NotifyChange(); // 🔥 Update UI  

                // 🔥 OPEN CART PAGE  
                await Shell.Current.GoToAsync(nameof(CartPage));
            });

            OpenDetailCommand = new Command<ShopItem>(async (item) =>
            {
                if (item == null) return;

                await Application.Current.MainPage.Navigation.PushAsync(
                    new ShopDetailPage(item));
            });
            AllItems = new ObservableCollection<ShopItem>();
            FilteredItems = new ObservableCollection<ShopItem>();

            // Correctly initialize FilterProducts as a Command  
            FilterCommand = new Command<string>(FilterProductsMethod);

            _service = new ShopDataService();

            Categories = new ObservableCollection<ShopCategory>();

            LoadData();
        }

        private void FilterProductsMethod(string category)
        {
            FilteredItems.Clear();

            // FIX CATEGORY MAPPING  
            string section = category;

            if (category == "Supplements")
                section = "Supplement";

            else if (category == "Fitness Gear")
                section = "Gear";

            foreach (var item in AllItems)
            {
                if (item.Section == section)
                {
                    FilteredItems.Add(item);
                }
            }
        }
        public ICommand AddItemCommand => new Command<ShopItem>(static async (item) =>
        {
            await Application.Current.MainPage.Navigation.PushAsync(
                new ShopDetailPage(item));
        });


        private async void LoadData()
        {
            var data = await _service.GetShopDataAsync();

            Categories.Clear();

            foreach (var cat in data.Categories)
                Categories.Add(cat);

            AllItems.Clear();
            FilteredItems.Clear();

            foreach (var item in data.Products)
            {
                AllItems.Add(item);
                FilteredItems.Add(item); // default show all
            }
        }

        //public ObservableCollection<ProductGroup> AllProducts { get; set; }

        //private async void LoadData()
        //{
        //    var data = await _service.GetShopDataAsync();

        //    var supplements = data.Products.Where(p => p.Section == "Supplement");
        //    var gear = data.Products.Where(p => p.Section == "Gear");

        //    AllProducts = new ObservableCollection<ProductGroup>
        //{
        //    new ProductGroup("Top Supplements", supplements),
        //    new ProductGroup("Fitness Gear", gear)
        //};
        //}

        //private void FilterProducts(string category)
        //{
        //    FilteredItems.Clear();

        //    // FIX CATEGORY MAPPING
        //    string section = category;

        //    if (category == "Supplements")
        //        section = "Supplement";

        //    else if (category == "Fitness Gear")
        //        section = "Gear";

        //    foreach (var item in AllItems)
        //    {
        //        if (item.Section == section)
        //        {
        //            FilteredItems.Add(item);
        //        }
        //    }
        //}

        


    }

}

