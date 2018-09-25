using MemeSounds.Helpers;
using MemeSounds.ViewModels;
using MemeSounds.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MemeSounds
{
  public partial class App : Application
  {
    public static NavigationPage Navigator { get; internal set; }

    public App()
    {
      InitializeComponent();

      if (string.IsNullOrEmpty(Settings.Token))
      {
        MainPage = new NavigationPage(new LoginPage());
      }
      else
      {
        var mainViewModel = MainViewModel.GetInstance();
        mainViewModel.Token = Settings.Token;
        mainViewModel.TokenType = Settings.TokenType;
        mainViewModel.Lands = new LandsViewModel();
        MainPage = new MasterPage();
      }
    }

    protected override void OnStart()
    {
      // Handle when your app starts
    }

    protected override void OnSleep()
    {
      // Handle when your app sleeps
    }

    protected override void OnResume()
    {
      // Handle when your app resumes
    }
  }
}
