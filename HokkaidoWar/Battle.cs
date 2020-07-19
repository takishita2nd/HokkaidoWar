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

        private City lastAttack = null;
        private City lastDeffece = null;

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

        public List<City> GetCityList()
        {
            return _cities;
        }

        public void NextTurn()
        {
            if (lastDeffece != null)
            {
                lastDeffece.ClearPaint();
            }
            if (lastAttack != null)
            {
                lastAttack.ClearPaint();
            }

            var targets = _cities[cityCnt].GetLinkedCities();
            var r = Singleton.GetRandom();
            int targetIdx = r.Next(0, targets.Count + 1);
            lastAttack = _cities[cityCnt];
            lastAttack.PaintAttackColor();

            var info = Singleton.GetGameProcessInfomation();
            if(targetIdx >= targets.Count)
            {
                info.ShowText(lastAttack.GetPosition(), string.Format("{0} turn {1} / {2} {3}",
                    turn, cityCnt + 1, _cities.Count, lastAttack.Name));
            }
            else
            {
                lastDeffece = targets[targetIdx];
                lastDeffece.PaintDeffenceColor();
                float attack = lastAttack.Population * (float)(r.Next(5, 30) / 10.0);
                float deffence = lastDeffece.Population * (float)(r.Next(5, 30) / 10.0);
                if(attack > deffence)
                {
                    info.ShowText(lastAttack.GetPosition(), string.Format("{0} turn {1} / {2} {3}\r\ntarget {4} \r\n{5} vs {6}\r\nwin",
                        turn, cityCnt + 1, _cities.Count, lastAttack.Name, lastDeffece.Name, (int)attack, (int)deffence));
                    lastAttack.CombinationCity(lastDeffece);
                    _cities.Remove(lastDeffece);
                    lastDeffece = null;
                }
                else
                {
                    info.ShowText(lastAttack.GetPosition(), string.Format("{0} turn {1} / {2} {3}\r\ntarget {4} \r\n{5} vs {6}\r\nlose",
                        turn, cityCnt + 1, _cities.Count, lastAttack.Name, lastDeffece.Name, (int)attack, (int)deffence));
                }
            }

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
