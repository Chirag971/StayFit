using StayFitz.Models;
using StayFitz.ViewModel;
using StayFitz.ViewModels;
using StayFitz.Views;

namespace StayFitz.Extensions
{
    /// <summary>
    /// Services extension to register View and ViewModels
    /// </summary>
    internal static class ServiceCollectionExtensions
    {
        public static void RegisterViewModels(this IServiceCollection services)
        {
            services.AddViewWithViewModel<SettingsPage, SettingsPageViewModel>();
            services.AddViewWithViewModel<DietPage, DietPageViewModel>();
            services.AddViewWithViewModel<HealthPage, HealthPageViewModel>();
            services.AddViewWithViewModel<ExercisePage, ExercisePageViewModel>();
            services.AddViewWithViewModel<HomePage, HomePageViewModel>();
            services.AddViewWithViewModel<ProductPage, ProductPageViewModel>();
            services.AddViewWithViewModel<SplashPage, SplashPageViewModel>();
            services.AddViewWithViewModel<ArticleDetailPage, ArticleDetailViewModel>();
            services.AddViewWithViewModel<BalancedMealPage, BalancedMealViewModel>();
            services.AddViewWithViewModel<DietTrackerPage, DietTrackerViewModel>();
            services.AddViewWithViewModel<ProfilePage, ProductPageViewModel>();
            services.AddViewWithViewModel<ShopPage, ShopViewModel>();
            services.AddViewWithViewModel<LoginPage, LoginPageViewModel>();
            services.AddViewWithViewModel<HistoryPage, HistoryViewModel>();
            services.AddViewWithViewModel<ProgressPage, ProgressViewModel>();
            services.AddViewWithViewModel<ReminderPage, ReminderPageViewModel>();
           
          
        }

        public static IServiceCollection AddViewWithViewModel<TView, TViewModel>(
           this IServiceCollection services,
           ServiceLifetime viewLifetime = ServiceLifetime.Transient,
           ServiceLifetime viewModelLifetime = ServiceLifetime.Transient)
           where TView : class
           where TViewModel : class
        {
            services.Add(new ServiceDescriptor(typeof(TViewModel), typeof(TViewModel), viewModelLifetime));
            services.Add(new ServiceDescriptor(typeof(TView), typeof(TView), viewLifetime));

            return services;
        }
    }
}
