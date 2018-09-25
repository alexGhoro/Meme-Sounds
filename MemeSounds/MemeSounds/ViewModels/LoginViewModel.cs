using GalaSoft.MvvmLight.Command;
using MemeSounds.Helpers;
using MemeSounds.Services;
using MemeSounds.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace MemeSounds.ViewModels
{
  public class LoginViewModel : BaseViewModel
  {
    private ApiService apiService;
    private string email;
    private string password;
    private bool isRunning;
    private bool isEnabled;

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

    public ICommand LoginCommand => new RelayCommand(Login);

    public ICommand RegisterCommand { get; set; }

    public LoginViewModel()
    {
      apiService = new ApiService();
      IsRemembered = true;
      IsEnabled = true;
    }

    private async void Login()
    {
      if (string.IsNullOrEmpty(this.Email))
      {
        await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.EmailValidation, Languages.Accept);
        return;
      }

      if (string.IsNullOrEmpty(this.Password))
      {
        await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.EmailValidation, Languages.Accept);
        return;
      }

      this.IsRunning = true;
      this.IsEnabled = false;

      var internetConnection = apiService.CheckConnection();

      if (!internetConnection.IsSuccess)
      {
        this.IsRunning = false;
        this.IsEnabled = true;
        await Application.Current.MainPage.DisplayAlert(Languages.Error, internetConnection.Message, Languages.Accept);
        return;
      }

      var token = await apiService.GetToken("https://memesoundsapi.azurewebsites.net", Email, Password);

      if (token == null)
      {
        IsRunning = false;
        IsEnabled = true;
        await Application.Current.MainPage.DisplayAlert(Languages.Error,"Something went wrong, try again later", Languages.Accept);
        return;
      }

      if (string.IsNullOrEmpty(token.AccessToken))
      {
        IsRunning = false;
        IsEnabled = true;
        await Application.Current.MainPage.DisplayAlert(Languages.Error, "Token is null or empty", Languages.Accept);
        this.Password = string.Empty;
        return;
      }

      var mainViewModel = MainViewModel.GetInstance();
      mainViewModel.Token = token.AccessToken;
      mainViewModel.TokenType = token.TokenType;

      if (this.IsRemembered)
      {
        Settings.Token = token.AccessToken;
        Settings.TokenType = token.TokenType;
      }

      mainViewModel.Lands = new LandsViewModel();
      Application.Current.MainPage = new MasterPage();

      this.IsRunning = false;
      this.IsEnabled = true;

      this.Email = string.Empty;


    }
  }
}
