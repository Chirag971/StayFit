using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using StayFitz.Models;

namespace StayFitz.Services
{
    public class QuoteService
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<string> GetQuoteAsync()
        {
            var url = "https://zenquotes.io/api/random";

            var response = await _httpClient.GetStringAsync(url);

            var data = JsonSerializer.Deserialize<List<ZenQuote>>(response);

            return $"\"{data[0].q}\"\n- {data[0].a}";
        }
    }
}
