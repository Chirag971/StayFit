using StayFitz.ViewModels;

namespace StayFitz.Views;

public partial class LoginPage : ContentPage
{
    bool isPasswordVisible = false;
    public LoginPage(LoginPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
    private void PinEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (e.NewTextValue.Length == 8)
        {
            PasswordEntry.Focus();
        }
    }
    private void PasswordEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        // Show eye icon only when password entered
        EyeButton.IsVisible = !string.IsNullOrEmpty(e.NewTextValue);
    }

    private void EyeButton_Clicked(object sender, EventArgs e)
    {
        isPasswordVisible = !isPasswordVisible;

        PasswordEntry.IsPassword = !isPasswordVisible;

        EyeButton.Source = isPasswordVisible ? "eye_open.png" : "eye.png";
    }
}	