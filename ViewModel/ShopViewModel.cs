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

        public ShopViewModel()
        {
            AllItems = new ObservableCollection<ShopItem>();
            FilteredItems = new ObservableCollection<ShopItem>();

            //FilterCommand = new Command<string>(FilterProducts);
            _service = new ShopDataService();

            Categories = new ObservableCollection<ShopCategory>();
            //FeaturedItems = new ObservableCollection<ShopItem>();
            //GearItems = new ObservableCollection<ShopItem>();

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

        private void FilterProducts(string category)
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


    }

}

