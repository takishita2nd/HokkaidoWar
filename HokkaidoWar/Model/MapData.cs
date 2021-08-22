using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HokkaidoWar.Model
{

    public class MapData
    {
        public Citydata[] citydata { get; set; }
    }

    public class Citydata
    {
        public int id { get; set; }
        public string name { get; set; }
        public int population { get; set; }
        public int money { get; set; }
        public Point point { get; set; }
        public int[] link { get; set; }
    }

    public class Point
    {
        public int x { get; set; }
        public int y { get; set; }
    }

}