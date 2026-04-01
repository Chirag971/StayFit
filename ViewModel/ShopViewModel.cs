using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using StayFitz.Models;
using StayFitz.Services;
using StayFitz.Utilities;
using StayFitz.Views;

namespace StayFitz.ViewModel
{
    public class ShopViewModel
    {
        private readonly ShopDataService _service;

        public ObservableCollection<ShopCategory> Categories { get; set; }
        public ObservableCollection<ShopItem> FeaturedItems { get; set; }
        public ObservableCollection<ShopItem> GearItems { get; set; }
        public ObservableCollection<ShopItem> AllItems { get; set; }   // all products
        public ObservableCollection<ShopItem> FilteredItems { get; set; } // filtered UI

      
        public ShopViewModel()
        {
            AllItems = new ObservableCollection<ShopItem>();
            FilteredItems = new ObservableCollection<ShopItem>();

            //FilterCommand = new Command<string>(FilterProducts);
            _service = new ShopDataService();

            Categories = new ObservableCollection<ShopCategory>();
            FeaturedItems = new ObservableCollection<ShopItem>();
            GearItems = new ObservableCollection<ShopItem>();

            LoadData();
        }
        public ICommand AddItemCommand => new Command<ShopItem>(static async (item) =>
        {
            await Application.Current.MainPage.Navigation.PushAsync(
                new ShopDetailPage(item));
        });

        private async void LoadData()
        {
            var data = await _service.GetShopDataAsync();

            // Categories
            foreach (var cat in data.Categories)
                Categories.Add(cat);

            // Products filter
            var supplements = data.Products.Where(p => p.Section == "Supplement");
            var gear = data.Products.Where(p => p.Section == "Gear");

            foreach (var item in supplements)
                FeaturedItems.Add(item);

            foreach (var item in data.Products)
            {
                AllItems.Add(item);
                FilteredItems.Add(item); // default = show all
            }

            foreach (var item in gear)
                GearItems.Add(item);
        }
        //    public ObservableCollection<ProductGroup> AllProducts { get; set; }

        //    private async void LoadData()
        //    {
        //        var data = await _service.GetShopDataAsync();

        //        var supplements = data.Products.Where(p => p.Section == "Supplement");
        //        var gear = data.Products.Where(p => p.Section == "Gear");

        //        AllProducts = new ObservableCollection<ProductGroup>
        //{
        //    new ProductGroup("Top Supplements", supplements),
        //    new ProductGroup("Fitness Gear", gear)
        //};
        //    }





        //private void FilterProducts(string category)
        //{
        //    SelectedCategory = category;

        //    FilteredItems.Clear();

        //    if (category == "All")
        //    {
        //        foreach (var item in AllItems)
        //            FilteredItems.Add(item);
        //    }
        //    else
        //    {
        //        var filtered = AllItems.Where(x => x.Section == category);

        //        foreach (var item in filtered)
        //            FilteredItems.Add(item);
        //    }
        //}
    }

}

