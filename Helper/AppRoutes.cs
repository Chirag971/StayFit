using StayFitz.ViewModels;
using StayFitz.Views;

namespace StayFitz.Helper
{
    public static class AppRoutes
    {
        public static void RegisterRoutes()
        {
            Routing.RegisterRoute(nameof(ExercisePage), typeof(ExercisePage));
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            //Routing.RegisterRoute(nameof(ProductPage), typeof(ProductPage));
            //Routing.RegisterRoute(nameof(SplashPage), typeof(SplashPage));
            Routing.RegisterRoute(nameof(DietPage), typeof(DietPage));
            Routing.RegisterRoute(nameof(HealthPage), typeof(HealthPage));
            Routing.RegisterRoute(nameof(ArticleDetailPage), typeof(ArticleDetailPage));
            Routing.RegisterRoute(nameof(BalancedMealPage), typeof(BalancedMealPage));
            Routing.RegisterRoute(nameof(DietTrackerPage), typeof(DietTrackerPage));
            //Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
            //Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
            //Routing.RegisterRoute(nameof(ShopPage), typeof(ShopPage));
            //Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(HistoryPage), typeof(HistoryPage));
            Routing.RegisterRoute(nameof(ProgressPage), typeof(ProgressPage));
            //Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
            Routing.RegisterRoute(nameof(ReminderPage), typeof(ReminderPage));
            Routing.RegisterRoute(nameof(WishlistPage), typeof(WishlistPage));
            Routing.RegisterRoute(nameof(CartPage), typeof(CartPage));
            Routing.RegisterRoute(nameof(RecipesPage), typeof(RecipesPage));
         
        }
    }
}
