using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StayFitz.Models;

namespace StayFitz.ViewModels
{
    [QueryProperty(nameof(Article), "Article")]
    public partial class ArticleDetailViewModel : ObservableObject
    {
        private HealthArticle _article;
        public HealthArticle Article
        {
            get => _article;
            set
            {
                _article = value;
                OnPropertyChanged();
            }
        }
        [RelayCommand]
        public async Task ShareAsync()
        {
            if (Article == null) return;

            var text = $"{Article.Title}\n\n{Article.Description}";

            await Share.Default.RequestAsync(new ShareTextRequest
            {
                Text = text,
                Title = "Share Article"
            });
        }
    }
}


