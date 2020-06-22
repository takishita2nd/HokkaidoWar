using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HokkaidoWar
{
    [JsonObject("MapDataModel")]
    public class MapData
    {
        [JsonProperty("list")]
        public List[] list { get; set; }
    }

    [JsonObject("List")]
    public class List
    {
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("point")]
        public Point[] point { get; set; }
        [JsonProperty("population")]
        public int population { get; set; }
    }

    [JsonObject("Point")]
    public class Point
    {
        [JsonProperty("x")]
        public int x { get; set; }
        [JsonProperty("y")]
        public int y { get; set; }
    }

}
