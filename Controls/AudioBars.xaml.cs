using Plugin.Maui.Audio;
using StayFitz.Helper;

namespace StayFitz.Controls;

public partial class AudioBars : ContentView
{
    IAudioPlayer audioPlayer;
    bool isAnimate = false;

    public AudioBars()
    {
        InitializeComponent();
        _ = SetAudio();
    }

    private async Task SetAudio()
    {
        try
        {
            if (GlobalHelper.MemoryStream == null)
            {
                GlobalHelper.AudioFilePath = "bergs.mp3";
                using var stream = await FileSystem.OpenAppPackageFileAsync(GlobalHelper.AudioFilePath);
                var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);
                memoryStream.Position = 0; // Reset position 
                GlobalHelper.MemoryStream = memoryStream;
            }
            audioPlayer = AudioManager.Current.CreatePlayer(GlobalHelper.MemoryStream);
        }
        catch (Exception ex)
        {

        }
    }

    // Call this when audio starts playing
    //async Task AnimateBars()
    //{
    //    while (isAnimate)
    //    {
    //        // Randomize heights for effect
    //        var rnd = new Random();
    //        await Task.WhenAll(
    //            Bar1.ScaleYTo(rnd.NextDouble() * 2 + 0.5, 150),
    //            Bar2.ScaleYTo(rnd.NextDouble() * 2 + 0.5, 150),
    //            Bar3.ScaleYTo(rnd.NextDouble() * 2 + 0.5, 150),
    //            Bar4.ScaleYTo(rnd.NextDouble() * 2 + 0.5, 150)

    //        );
    //    }
    //}
    Random rnd = new Random();

    async Task AnimateBars()
    {
        while (isAnimate)
        {
            await Task.WhenAll(
                Bar1.ScaleYTo(rnd.NextDouble() * 2 + 0.5, 120),
                Bar2.ScaleYTo(rnd.NextDouble() * 2 + 0.5, 120),
                Bar3.ScaleYTo(rnd.NextDouble() * 2 + 0.5, 120),
                Bar4.ScaleYTo(rnd.NextDouble() * 2 + 0.5, 120)
            );
        }
    }
    //private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    //{
    //    isAnimate = !isAnimate;
    //    if (audioPlayer != null)
    //    {
    //        audioPlayer.Volume = 1;
    //        if (!audioPlayer.IsPlaying)
    //        {
    //            audioPlayer.Play();
    //        }
    //        else
    //        {
    //            audioPlayer.Pause();
    //        }
    //        AnimateBars();
    //    }
    //}
    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        if (audioPlayer == null)
            return;

        if (!audioPlayer.IsPlaying)
        {
            audioPlayer.Play();
            isAnimate = true;
            _ = AnimateBars();
        }
        else
        {
            audioPlayer.Pause();
            isAnimate = false;
        }
    }

    //private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
    //    {
    //        if (sliderVolume != null && audioPlayer != null)
    //        {
    //            audioPlayer.Volume = sliderVolume.Value;
    //        }
    //    }

    private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        if (audioPlayer != null)
            audioPlayer.Volume = e.NewValue;
    }
    public void Stop()
    {
        if (audioPlayer != null)
        {
            audioPlayer.Stop();
            isAnimate = false;
        }
    }
    public void Pause(bool isPaused)
    {
        if (audioPlayer != null)
        {
            if (isPaused)
                audioPlayer.Pause();
            else
                audioPlayer.Play();

            isAnimate = !isPaused;
        }
    }
}