using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HokkaidoWar
{
    class Battle
    {
        private List<City> _cities = null;
        private int turn;
        private int cityCnt;
        public Battle(List<City> cities)
        {
            turn = 1;
            cityCnt = 0;
            _cities = new List<City>();
            foreach(var c in cities)
            {
                _cities.Add(c);
            }
            _cities = cityRandomReplace(_cities);
        }

        public void NextTurn()
        {
            var info = Singleton.GetGameProcessInfomation();
            info.ShowText(_cities[cityCnt].GetPosition(), string.Format("{0} turn {1} / {2} {3}", turn, cityCnt + 1, _cities.Count, _cities[cityCnt].Name));

            cityCnt++;
            if(cityCnt >= _cities.Count)
            {
                _cities = cityRandomReplace(_cities);
                cityCnt = 0;
                turn++;
            }
        }

        private List<City> cityRandomReplace(List<City> beforeCities)
        {
            var r = Singleton.GetRandom();
            List<City> afterCities = new List<City>();
            int max = beforeCities.Count;
            for (int i = 0; i < max; i++)
            {
                int index = r.Next(0, beforeCities.Count - 1);
                afterCities.Add(beforeCities[index]);
                beforeCities.RemoveAt(index);
            }

            return afterCities;
        }
    }
}
