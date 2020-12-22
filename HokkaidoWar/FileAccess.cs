using HokkaidoWar.Model;
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
            string json;
            using (var stream = new StreamReader(_filename, true))
            {
                json = stream.ReadToEnd();
            }
            //string str = string.Empty;
            //asd.StreamFile stream = asd.Engine.File.CreateStreamFile(_filename);
            //List<byte> buffer = new List<byte>();
            //stream.Read(buffer, stream.Size);
            //string json = Encoding.UTF8.GetString(buffer.ToArray());
            return JsonConvert.DeserializeObject<MapData>(json);
        }
    }
}
