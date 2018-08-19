using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MemeSounds.Views;
using MemeSounds.ViewModels;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MemeSounds
{
  public partial class App : Application
  {
    public App()
    {
      InitializeComponent();

      this.MainPage = new NavigationPage(new LoginPage()) ;
    }

    protected override void OnStart()
    {
    }

    protected override void OnSleep()
    {
    }

    protected override void OnResume()
    {
    }
  }
}
