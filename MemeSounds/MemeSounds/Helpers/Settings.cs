using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MemeSounds.Helpers
{
  public static class Settings
  {
    static ISettings AppSettings => CrossSettings.Current;

    const string isRemembered = "IsRemembered";
    const string token = "Token";
    const string tokenType = "TokenType";
    static readonly string stringDefault = string.Empty;

    public static string IsRemembered
    {
      get => AppSettings.GetValueOrDefault(isRemembered, stringDefault);
      set => AppSettings.AddOrUpdateValue(isRemembered, value);
    }

    public static string Token
    {
      get => AppSettings.GetValueOrDefault(token, stringDefault);
      set => AppSettings.AddOrUpdateValue(token, value);
    }

    public static string TokenType
    {
      get => AppSettings.GetValueOrDefault(tokenType, stringDefault);
      set => AppSettings.AddOrUpdateValue(TokenType, value);
    }
  }
}
