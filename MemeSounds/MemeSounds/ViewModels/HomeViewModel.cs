using MemeSounds.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MemeSounds.ViewModels
{
  public class HomeViewModel
  {
    public List<Audio> Audios;

    public HomeViewModel()
    {
      Audios = AudioManager.GetAudios();
    }
  }
}
