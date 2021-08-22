﻿using System;
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
        public CityData[] Citydata { get; set; }
    }

    public class CityData
    {
        public int id { get; set; }
        public string name { get; set; }
        public int money { get; set; }
        public int power { get; set; }
        public float bonus { get; set; }
        public int[] mapid { get; set; }
    }
}
