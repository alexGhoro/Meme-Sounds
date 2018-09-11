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
      var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
      Resource.Culture = ci;
      DependencyService.Get<ILocalize>().SetLocale(ci);
    }

    public static string Accept => Resource.Accept;

    public static string EmailValidation => Resource.EmailValidation;

    public static string Error => Resource.Error;
  }
}
