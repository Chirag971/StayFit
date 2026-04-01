using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StayFitz.Models;
using StayFitz.Views;

namespace StayFitz.ViewModels
{
    public partial class HealthPageViewModel : ObservableObject
    {
        
            public ObservableCollection<HealthCategory> Categories { get; set; }
            public ObservableCollection<HealthArticle> Articles { get; set; }

            [ObservableProperty]
            private string searchText;
            public ICommand CategoryCommand { get; }
            public ICommand ArticleCommand { get; }

              private List<HealthArticle> _allArticles;



        [RelayCommand]
        public async Task OpenCategoryAsync(string category)
        {
            await Shell.Current.GoToAsync(nameof(CategoryPage),
                new Dictionary<string, object>
                {
            { "CategoryName", category }
                });
        }
       

        public HealthPageViewModel()
            {
                Categories = new ObservableCollection<HealthCategory>
        {
            new HealthCategory { Name="Nutrition", Icon="leaf.png", BackgroundColor="#FFFFFF" },
            new HealthCategory { Name="Fitness", Icon="run.png", BackgroundColor="#FFFFFF" },
            new HealthCategory { Name="Mental", Icon="heart.png", BackgroundColor="#FFFFFF" }
        };

            _allArticles = new List<HealthArticle>
            {
                new HealthArticle {
                    Title="Benefits of Daily Walking",
                    Description="Walking 30 minutes daily improves heart health...",
                    Image="walking.jpg",
                    Category="Fitness"
                },
                new HealthArticle {
                    Title="Healthy Diet Tips",
                    Description="Include fruits, vegetables and protein...",
                    Image="dietimg.jpg",
                    Category="Nutrition"
                },
                new HealthArticle {
                    Title="Meditation Guide", 
                    Description="Meditation reduces stress and promotes relaxation...",
                    Image="meditation.jpg",
                    Category="Mental"
                }
            };

            Articles = new ObservableCollection<HealthArticle>(_allArticles);


            CategoryCommand = new Command<string>(FilterByCategory);
                ArticleCommand = new Command<HealthArticle>(OpenArticle);
            }

        partial void OnSearchTextChanged(string value)
        {
            ApplyFilters();
        }
        private void ApplyFilters(string category = null)
        {
            var filtered = _allArticles.AsEnumerable();

            // Filter by category (if selected)
            if (!string.IsNullOrWhiteSpace(category))
            {
                filtered = filtered.Where(x => x.Category == category);
            }

            // Filter by search text
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


        //private void FilterByCategory(string category)
        //    {
        //        // You can filter from API later
        //        var filtered = Articles.Where(x => x.Category == category).ToList();
        //        Articles.Clear();
        //        foreach (var item in filtered)
        //            Articles.Add(item);
        //    }
        private void FilterByCategory(string category)
        {
            ApplyFilters(category);
        }

        //private async void OpenArticle(HealthArticle article)
        //    {
        //        await Shell.Current.GoToAsync("ArticleDetailPage",
        //            new Dictionary<string, object>
        //            {
        //        { "Article", article }
        //            });
        //    }
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

