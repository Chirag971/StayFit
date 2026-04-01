//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using CommunityToolkit.Mvvm.ComponentModel;
//using CommunityToolkit.Mvvm.Input;
//using StayFitz.Models;
//using StayFitz.Views;

//namespace StayFitz.ViewModel
//{
//    [QueryProperty(nameof(CategoryName), "CategoryName")]
//    public partial class CategoryViewModel : ObservableObject
//    {
//        private readonly IHealthService _healthService;

//        [ObservableProperty]
//        private string categoryName;

//        [ObservableProperty]
//        private ObservableCollection<HealthArticle> articles = new();

//        [ObservableProperty]
//        private bool isBusy;

//        public CategoryViewModel(IHealthService healthService)
//        {
//            _healthService = healthService;
//        }

//        partial void OnCategoryNameChanged(string value)
//        {
//            LoadArticlesCommand.Execute(null);
//        }

//        [RelayCommand]
//        public async Task LoadArticlesAsync()
//        {
//            if (IsBusy) return;

//            try
//            {
//                IsBusy = true;

//                var allArticles = await _healthService.GetArticlesAsync();
//                var filtered = allArticles
//                    .Where(x => x.Category == CategoryName)
//                    .ToList();

//                Articles.Clear();
//                foreach (var item in filtered)
//                    Articles.Add(item);
//            }
//            finally
//            {
//                IsBusy = false;
//            }
//        }

//        [RelayCommand]
//        public async Task OpenArticleAsync(HealthArticle article)
//        {
//            await Shell.Current.GoToAsync(nameof(ArticleDetailPage),
//                new Dictionary<string, object>
//                {
//                { "Article", article }
//                });
//        }
//    }
//}
