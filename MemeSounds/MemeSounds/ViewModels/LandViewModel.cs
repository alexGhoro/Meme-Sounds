using MemeSounds.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MemeSounds.ViewModels
{
  public class LandViewModel
  {
    public Land Land { get; set; }

    public LandViewModel(Land land)
    {
      this.Land = land;
    }
  }
}
