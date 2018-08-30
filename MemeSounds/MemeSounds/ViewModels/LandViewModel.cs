using MemeSounds.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MemeSounds.ViewModels
{
  public class LandViewModel : BaseViewModel
  {
    private ObservableCollection<Border> borders;
    
    public Land Land { get; set; }

    public ObservableCollection<Border> Borders
    {
      get { return this.borders; }
      set { this.SetValue(ref this.borders, value); }
    }

    public LandViewModel(Land land)
    {
      Land = land;
      LoadBorders();
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
