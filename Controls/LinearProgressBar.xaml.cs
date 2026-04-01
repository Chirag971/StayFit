using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace StayFitz.Controls;

public partial class LinearProgressBar : ContentView
{
    public LinearProgressBar()
    {
        InitializeComponent();
    }

    // Progress from 0 to 1
    public static readonly BindableProperty ProgressProperty =
        BindableProperty.Create(
            nameof(Progress),
            typeof(double),
            typeof(LinearProgressBar),
            0.0,
            propertyChanged: OnProgressChanged);

    public double Progress
    {
        get => (double)GetValue(ProgressProperty);
        set => SetValue(ProgressProperty, Math.Clamp(value, 0, 1));
    }

    // Progress color
    public static readonly BindableProperty ProgressColorProperty =
        BindableProperty.Create(
            nameof(ProgressColor),
            typeof(Color),
            typeof(LinearProgressBar),
            Colors.Green,
            propertyChanged: OnProgressColorChanged);

    public Color ProgressColor
    {
        get => (Color)GetValue(ProgressColorProperty);
        set => SetValue(ProgressColorProperty, value);
    }

    private static void OnProgressChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((LinearProgressBar)bindable).UpdateProgress();
    }

    private static void OnProgressColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((LinearProgressBar)bindable).ProgressFill.BackgroundColor = (Color)newValue;
    }

    private void UpdateProgress()
    {
        if (ProgressFill == null || Width <= 0) return;

        // Calculate width based on Progress (0 → 1)
        double newWidth = Width * Progress;

        // Directly set width (faster and simpler than LayoutTo)
        ProgressFill.WidthRequest = newWidth;
    }

    // Update width whenever the control resizes
    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        UpdateProgress();
    }
}