using GalaSoft.MvvmLight.Command;
using MemeSounds.Models;
using MemeSounds.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace MemeSounds.ViewModels
{
  public class LandsViewModel : BaseViewModel
  {
    private ObservableCollection<LandItemViewModel> lands;
    private bool isRefreshing;
    private ApiService apiService;
    private string filter;

    public ICommand RefreshCommand => new RelayCommand(LoadLands);

    public ICommand SearchCommand => new RelayCommand(Search);

    public ObservableCollection<LandItemViewModel> Lands
    {
      get { return this.lands;}
      set { SetValue(ref this.lands, value); }
    }

    public bool IsRefreshing
    {
      get { return this.isRefreshing; }
      set { SetValue(ref this.isRefreshing, value); }
    }

    public string Filter
    {
      get { return this.filter; }
      set
      {
        SetValue(ref this.filter, value);
        Search();
      }
    }

    public LandsViewModel()
    {
      apiService = new ApiService();
      LoadLands();
    }

    private async void LoadLands()
    {
      IsRefreshing = true;
      var connection = apiService.CheckConnection();
      if (!connection.IsSuccess)
      {
        IsRefreshing = false;
        await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Accept");
        await Application.Current.MainPage.Navigation.PopAsync();
        return;
      }

      var response = await apiService.GetList<Land>("http://restcountries.eu", "/rest", "/v2/all");

      if (!response.IsSuccess)
      {
        IsRefreshing = false;
        await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
        await Application.Current.MainPage.Navigation.PopAsync();
        return;
      }

      MainViewModel.GetInstance().LandsList = (List<Land>)response.Result;
      Lands = new ObservableCollection<LandItemViewModel>(ToLandItemViewModel());
      IsRefreshing = false;
    }

    private IEnumerable<LandItemViewModel> ToLandItemViewModel()
    {
      return MainViewModel.GetInstance().LandsList.Select(l => new LandItemViewModel
      {
        Alpha2Code = l.Alpha2Code,
        Alpha3Code = l.Alpha3Code,
        AltSpellings = l.AltSpellings,
        Area = l.Area,
        Borders = l.Borders,
        CallingCodes = l.CallingCodes,
        Capital = l.Capital,
        Cioc = l.Cioc,
        Currencies = l.Currencies,
        Demonym = l.Demonym,
        Flag = l.Flag,
        Gini = l.Gini,
        Languages = l.Languages,
        Latlng = l.Latlng,
        Name = l.Name,
        NativeName = l.NativeName,
        NumericCode = l.NumericCode,
        Population = l.Population,
        Region = l.Region,
        RegionalBlocs = l.RegionalBlocs,
        Subregion = l.Subregion,
        Timezones = l.Timezones,
        TopLevelDomain = l.TopLevelDomain,
        Translations = l.Translations,
      });
    }

    private void Search()
    {
      if (string.IsNullOrEmpty(this.Filter))
      {
        Lands = new ObservableCollection<LandItemViewModel>(ToLandItemViewModel());
      }
      else
      {
        Lands = new ObservableCollection<LandItemViewModel>(ToLandItemViewModel().Where( 
          l => l.Name.ToLower().Contains(Filter.ToLower()) ||
            l.Capital.ToLower().Contains(Filter.ToLower() ))
        );
      }
    }

   

  }
}
