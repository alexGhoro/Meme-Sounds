using GalaSoft.MvvmLight.Command;
using MemeSounds.Views;
using MemeSounds.Helpers;
using System.Windows.Input;
using Xamarin.Forms;

namespace MemeSounds.ViewModels
{
  public class MenuItemViewModel
  {
    public string Icon { get; set; }
    public string Title { get; set; }
    public string PageName { get; set; }

    public ICommand NavigateCommand => new RelayCommand(Navigate);

    private void Navigate()
    {
      if (this.PageName == "LoginPage")
      {
        Settings.Token = string.Empty;
        Settings.TokenType = string.Empty;

        var mainViewModel = MainViewModel.GetInstance();
        mainViewModel.Token = string.Empty;
        mainViewModel.TokenType = string.Empty;

        Application.Current.MainPage = new NavigationPage(new LoginPage());
      }
    }
  }
}
