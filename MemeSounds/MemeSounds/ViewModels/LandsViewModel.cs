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
    private ObservableCollection<Land> lands;
    private List<Land> landsList;
    private bool isRefreshing;
    private ApiService apiService;
    private string filter;

    public ObservableCollection<Land> Lands
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
      var connection = await apiService.CheckConnection();
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

      landsList = (List<Land>)response.Result;
      Lands = new ObservableCollection<Land>(landsList);
      IsRefreshing = false;
    }

    private void Search()
    {
      if (string.IsNullOrEmpty(this.Filter))
      {
        Lands = new ObservableCollection<Land>(landsList);
      }
      else
      {
        Lands = new ObservableCollection<Land>(landsList.Where( 
          l => l.Name.ToLower().Contains(Filter.ToLower()) ||
            l.Capital.ToLower().Contains(Filter.ToLower() ))
        );
      }
    }

    public ICommand ResfreshCommand
    {
      get
      {
        return new RelayCommand(LoadLands);
      }
    }

    public ICommand SearchCommand
    {
      get
      {
        return new RelayCommand(Search);
      }
    }

  }
}
