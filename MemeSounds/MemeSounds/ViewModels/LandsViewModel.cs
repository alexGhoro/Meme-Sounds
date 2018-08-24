using MemeSounds.Models;
using MemeSounds.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace MemeSounds.ViewModels
{
  public class LandsViewModel : BaseViewModel
  {
    private ObservableCollection<Land> lands;
    private ApiService apiService;

    public ObservableCollection<Land> Lands
    {
      get { return this.lands;}
      set { SetValue(ref this.lands, value); }
    }

    public LandsViewModel()
    {
      apiService = new ApiService();
      LoadLands();
    }

    private async void LoadLands()
    {
      var response = await apiService.GetList<Land>("http://restcountries.eu", "/rest", "/v2/all");

      if (!response.IsSuccess)
      {
        await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
      }

      var list = (List<Land>)response.Result;
      Lands = new ObservableCollection<Land>(list);

    }
  }
}
