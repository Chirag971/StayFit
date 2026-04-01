using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using StayFitz.Models;

namespace StayFitz.Services
{
    public class ShopDataService
    {
        public async Task<ShopData> GetShopDataAsync()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("shopdata.json");

            using var reader = new StreamReader(stream);
            var json = await reader.ReadToEndAsync();

            return JsonSerializer.Deserialize<ShopData>(json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }
    }
}
