using GalaSoft.MvvmLight.Command;
using MemeSounds.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace MemeSounds.ViewModels
{
  public class LoginViewModel : BaseViewModel
  {    
    public string Email
    {
      get { return this.email; }
      set { SetValue(ref this.email, value); }
    }

    public string Password
    {
      get { return this.password; }
      set { SetValue(ref this.password, value); }
    }

    public bool IsRunning
    {
      get { return this.isRunning; }
      set { SetValue(ref this.isRunning, value); }
    }

    public bool IsRemembered { get; set; }

    public bool IsEnabled
    {
      get { return this.isEnabled; }
      set { SetValue(ref this.isEnabled, value); }
    }

    public ICommand LoginCommand
    {
      get
      {
        return new RelayCommand(Login);
      }
    }

    private string email;
    private string password;
    private bool isRunning;
    private bool isEnabled;

    public LoginViewModel()
    {
      IsRemembered = true;
      IsEnabled = true;
      this.Email = "amatute.dev@gmail.com";
      this.Password = "123";
    }

    public ICommand RegisterCommand { get; set; }

    private async void Login()
    {
      if (string.IsNullOrEmpty(this.Email))
      {
        await Application.Current.MainPage.DisplayAlert("Error", "You must enter an email.", "Accept");
        return;
      }

      if (string.IsNullOrEmpty(this.Password))
      {
        await Application.Current.MainPage.DisplayAlert("Error", "You must enter an password.", "Accept");
        return;
      }

      this.IsRunning = true;
      this.IsEnabled = false;

      if (this.Email != "amatute.dev@gmail.com" || this.Password != "123" )
      {
        await Application.Current.MainPage.DisplayAlert("Error", "Email or Password incorrect.", "Accept");
        this.Password = string.Empty;
        return;
      }

      this.IsRunning = false;
      this.IsEnabled = true;

      this.Email = string.Empty;
      this.Password = string.Empty;

      MainViewModel.GetInstance().Lands = new LandsViewModel();
      await Application.Current.MainPage.Navigation.PushAsync(new LandsPage());

    }
  }
}
