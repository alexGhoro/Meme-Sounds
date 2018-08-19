using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using Xamarin.Forms;

namespace MemeSounds.ViewModels
{
  public class LoginViewModel : BaseViewModel
  {
    #region Properties
    public string Email { get; set; }

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
    #endregion

    private string email;
    private string password;
    private bool isRunning;
    private bool isEnabled;

    public LoginViewModel()
    {
      IsRemembered = true;
      IsEnabled = true;
    }

    public ICommand LoginCommand
    { get
      {
        return new RelayCommand(Login);
      }
    }

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

      if (this.Email != "amatute.dev@gmail.com" || this.Password != "123" )
      {
        await Application.Current.MainPage.DisplayAlert("Error", "Email or Password incorrect.", "Accept");
        this.Password = string.Empty;
        return;
      }

      await Application.Current.MainPage.DisplayAlert("Ok", "You are now logged in!", "Accept");

      return;

    }

    public ICommand RegisterCommand { get; set; }

  }
}
