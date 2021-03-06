﻿using GalaSoft.MvvmLight.Command;
using MemeSounds.Models;
using MemeSounds.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MemeSounds.ViewModels
{
  public class LandItemViewModel : Land
  {
    #region Commands
    public ICommand SelectLandCommand
    {
      get
      {
        return new RelayCommand(SelectLand);
      }
    }

    private async void SelectLand()
    {
      MainViewModel.GetInstance().Land = new LandViewModel(this);
      await App.Navigator.PushAsync(new LandTabbedPage());
    }
    #endregion
  }
}
