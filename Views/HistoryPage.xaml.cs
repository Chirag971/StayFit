using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using StayFitz.ViewModels;
using StayFitz.Models;
using StayFitz.Services;

namespace StayFitz.Views;



public partial class HistoryPage : ContentPage
{
    HistoryViewModel vm;

    public HistoryPage()
    {
        InitializeComponent();
        vm = new HistoryViewModel();
        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        //await vm.LoadHistory();
        vm.LoadHistory();
    }
}