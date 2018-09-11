using System.Globalization;

namespace MemeSounds.Interfaces
{
  public interface ILocalize
  {
    CultureInfo GetCurrentCultureInfo();

    void SetLocale(CultureInfo ci);
  }
}
