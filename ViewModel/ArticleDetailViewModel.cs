using CommunityToolkit.Mvvm.ComponentModel;
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
    }
}

