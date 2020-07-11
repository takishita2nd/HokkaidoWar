using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HokkaidoWar
{
    class City
    {
        private string _name = string.Empty;
        private int _population = 0;
        private List<Map> _maps = null;
        private asd.Color _color;

        public string Name { get { return _name; } }

        public City(string name, Point[] points, int population)
        {
            _name = name;
            _population = population;
            _maps = new List<Map>();
            var r = Singleton.GetRandom();
            _color = new asd.Color((byte)r.Next(0, 255), (byte)r.Next(0, 255), (byte)r.Next(0, 255));

            var fieldMap = Singleton.GetFieldMap();

            foreach (var p in points)
            {
                Map m = new Map(p.x, p.y, _color);
                m.SetCity(this);
                _maps.Add(m);
                fieldMap.SetMap(m);
            }
        }

        public void OnMouse(asd.Vector2DF pos)
        {
            foreach (var m in _maps)
            {
                if(m.IsOnMouse(pos))
                {
                    var info = Singleton.GetInfomationWindow();
                    info.ShowText(pos, _name + "\r\n" + _population.ToString());
                }
            }
        }

        public bool IsOnMouse(asd.Vector2DF pos)
        {
            bool ret = false;
            foreach (var m in _maps)
            {
                if (m.IsOnMouse(pos))
                {
                    ret = true;
                }
            }
            return ret;
        }

        public asd.Vector2DF GetPosition()
        {
            return new asd.Vector2DF(24 * _maps[0].X + 50, 24 * _maps[0].Y + 50);
        }

        private List<City> GetLinkedCities()
        {
            List<City> cities = new List<City>();
            foreach (var m in _maps)
            {
                if (m.Up != null)
                {
                    var c = m.Up.GetCity();
                    if (cities.Contains(c) == false && c != this)
                    {
                        cities.Add(c);
                    }
                }
                if (m.Down != null)
                {
                    var c = m.Down.GetCity();
                    if (cities.Contains(c) == false && c != this)
                    {
                        cities.Add(c);
                    }
                }
                if (m.Left != null)
                {
                    var c = m.Left.GetCity();
                    if (cities.Contains(c) == false && c != this)
                    {
                        cities.Add(c);
                    }
                }
                if (m.Right != null)
                {
                    var c = m.Right.GetCity();
                    if (cities.Contains(c) == false && c != this)
                    {
                        cities.Add(c);
                    }
                }
            }
            return cities;
        }
    }
}
