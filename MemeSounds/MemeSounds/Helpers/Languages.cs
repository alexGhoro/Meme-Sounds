using MemeSounds.Interfaces;
using MemeSounds.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MemeSounds.Helpers
{
  class Languages
  {
    static Languages()
    {
      var cultureInfo = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
      Resource.Culture = cultureInfo;
      DependencyService.Get<ILocalize>().SetLocale(cultureInfo);
    }

    public static string Accept => Resource.Accept;

    public static string EmailValidation => Resource.EmailValidation;

    public static string Error => Resource.Error;

    public static string MyProfile => Resource.MyProfile;

    public static string Stats => Resource.Stats;

    public static string LogOut => Resource.LogOut;
  }
}
