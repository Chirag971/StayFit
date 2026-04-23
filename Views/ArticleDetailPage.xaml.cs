using StayFitz.ViewModels;

namespace StayFitz.Views;

public partial class ArticleDetailPage : ContentPage
{
	public ArticleDetailPage(ArticleDetailViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
  
}	