using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StayFitz.Models.StayFitz.Models;

namespace StayFitz.Models
{

    public class ProductGroup : ObservableCollection<ShopItem>
    {
        public string Title { get; set; }

        public ProductGroup(string title, IEnumerable<ShopItem> items) : base(items)
        {
            Title = title;
        }
    }
}
