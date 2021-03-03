using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HokkaidoWar.Model
{
    public class SaveData
    {
        public int Turn { get; set; }
        public int PlayerId { get; set; }
        public List<CityData> Citydata { get; set; }
    }

    public class CityData
    {
        public int id { get; set; }
        public int money { get; set; }
        public int power { get; set; }
        public List<int> mapid { get; set; }
    }
}
