using System;
using System.Collections.Generic;
using System.Text;

namespace MemeSounds.Models
{
  public class Audio
  {
    public int AudioId { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
  }

  public static class AudioManager
  {
    public static List<Audio> GetAudios()
    {
      var audios = new List<Audio>
      {
        new Audio { AudioId = 1, Name = "Audio 1", Path = "Asets/" },
        new Audio { AudioId = 2, Name = "Audio 2", Path = "Asets/" },
        new Audio { AudioId = 3, Name = "Audio 3", Path = "Asets/" },
        new Audio { AudioId = 4, Name = "Audio 4", Path = "Asets/" },
        new Audio { AudioId = 5, Name = "Audio 5", Path = "Asets/" },
        new Audio { AudioId = 6, Name = "Audio 6", Path = "Asets/" }
      };

      return audios;
    }

  }
}
