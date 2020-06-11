using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HokkaidoWar
{
    class FileAccess
    {
        private const string _filename = "hokkaido.json";
        public static MapData Load()
        {
            string str = string.Empty;
            using (var stream = new StreamReader(_filename, true))
            {
                str = stream.ReadToEnd();
            }
            return JsonConvert.DeserializeObject<MapData>(str);
        }
    }
}
