using System;
using System.Collections.Generic;
using System.Text;

namespace MemeSounds.ViewModels
{
  public class MainViewModel
  {
    public LoginViewModel Login { get; set; }
    public LandsViewModel Lands { get; set; }
    public LandViewModel Land { get; set; }

    public MainViewModel()
    {
      instance = this;
      Login = new LoginViewModel();
    }

    private static  MainViewModel instance;

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
