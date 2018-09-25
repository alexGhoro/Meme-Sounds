using MemeSounds.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MemeSounds.Helpers;
using System.Text;

namespace MemeSounds.ViewModels
{
  public class MainViewModel
  {
    private static  MainViewModel instance;

    public LoginViewModel Login { get; set; }
    public LandsViewModel Lands { get; set; }
    public LandViewModel Land { get; set; }

    public List<Land> LandsList { get; set; }
    //public TokenResponse Token { get; internal set; }

    public string Token { get; set; }
    public string TokenType { get; set; }


    public ObservableCollection<MenuItemViewModel> Menus { get; set; }

    public MainViewModel()
    {
      instance = this;
      Login = new LoginViewModel();
      LoadMenu();
    }

    private void LoadMenu()
    {
      Menus = new ObservableCollection<MenuItemViewModel>();

      Menus.Add(new MenuItemViewModel
      {
        Icon = "ic_settings",
        PageName = "MyProfilePage",
        Title = Languages.MyProfile
      });

      Menus.Add(new MenuItemViewModel
      {
        Icon = "ic_insert_chart",
        PageName = "Stats",
        Title = Languages.Stats
      });

      Menus.Add(new MenuItemViewModel
      {
        Icon = "ic_exit_to_app",
        PageName = "LoginPage",
        Title = Languages.LogOut
      });
    }


    public static MainViewModel GetInstance()
    {
      if (instance == null)
      {
        return new MainViewModel();
      }

      return instance;
    }
  }
}
