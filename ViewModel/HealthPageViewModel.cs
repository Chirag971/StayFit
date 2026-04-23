using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using StayFitz.Models;
using StayFitz.Services;
using StayFitz.Views;

namespace StayFitz.ViewModels
{
    public partial class HealthPageViewModel : ObservableObject
    {
        private readonly DataService _dataService;

        public ObservableCollection<HealthCategory> Categories { get; set; }
        public ObservableCollection<HealthArticle> Articles { get; set; }

        [ObservableProperty]
        private string searchText;

        public ICommand CategoryCommand { get; }
        public ICommand ArticleCommand { get; }

        private List<HealthArticle> _allArticles = new();

        public HealthPageViewModel()
        {
            // 🔥 FIX: Initialize service
            _dataService = new DataService();

            Categories = new ObservableCollection<HealthCategory>();
            Articles = new ObservableCollection<HealthArticle>();

            CategoryCommand = new Command<string>(FilterByCategory);
            ArticleCommand = new Command<HealthArticle>(OpenArticle);

            LoadData();
        }

        // LOAD DATA FROM JSON
        private async void LoadData()
        {
            try
            {
                var data = await _dataService.GetHealthDataAsync();

                if (data == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Data not loaded", "OK");
                    return;
                }

                Categories.Clear();
                if (data.Categories != null)
                {
                    foreach (var cat in data.Categories)
                        Categories.Add(cat);
                }

                _allArticles = data.Articles ?? new List<HealthArticle>();

                ApplyFilters();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        // 🔍 SEARCH FILTER
        partial void OnSearchTextChanged(string value)
        {
            ApplyFilters();
        }

        // 🔥 FILTER LOGIC
        private void ApplyFilters(string category = null)
        {
            var filtered = _allArticles.AsEnumerable();

            // Category filter
            if (!string.IsNullOrWhiteSpace(category))
            {
                filtered = filtered.Where(x => x.Category == category);
            }

            // Search filter
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                filtered = filtered.Where(a =>
                    a.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                    a.Description.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
            }

            Articles.Clear();
            foreach (var item in filtered)
                Articles.Add(item);
        }

        private void FilterByCategory(string category)
        {
            ApplyFilters(category);
        }

        // 🔗 OPEN ARTICLE
        private async void OpenArticle(HealthArticle article)
        {
            if (article == null) return;

            await Shell.Current.GoToAsync(nameof(ArticleDetailPage),
                new Dictionary<string, object>
                {
                    { "Article", article }
                });
        }
    }
}