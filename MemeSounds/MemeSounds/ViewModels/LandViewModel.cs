using MemeSounds.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MemeSounds.ViewModels
{
  public class LandViewModel : BaseViewModel
  {
    private ObservableCollection<Border> borders;
    private ObservableCollection<Currency> currencies;
    private ObservableCollection<Language> languages;

    public Land Land { get; set; }

    public ObservableCollection<Border> Borders
    {
      get => borders;
      set => SetValue(ref borders, value);
    }

    public ObservableCollection<Currency> Currencies
    {
      get => currencies;
      set => SetValue(ref currencies, value);
    }

    public ObservableCollection<Language> Languages
    {
      get => languages;
      set => SetValue(ref languages, value);
    }

    public LandViewModel(Land land)
    {
      Land = land;
      LoadBorders();
      Currencies = new ObservableCollection<Currency>(Land.Currencies);
      Languages = new ObservableCollection<Language>(Land.Languages);
    }

    private void LoadBorders()
    {
      Borders = new ObservableCollection<Border>();
      foreach (var border in Land.Borders)
      {
        var land = MainViewModel.GetInstance().LandsList.Where(l => l.Alpha3Code == border).FirstOrDefault();

        if (land != null)
        {
          this.Borders.Add(new Border
          {
            Code = land.Alpha3Code,
            Name = land.Name,
          });
        }
      }
    }
  }
}
