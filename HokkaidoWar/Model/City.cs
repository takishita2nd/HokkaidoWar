using asd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HokkaidoWar.Model
{
    class City
    {
        private string _name = string.Empty;
        private int _population = 0;
        private int _money = 0;
        private bool _isAlive;
        private List<Map> _maps = null;
        private asd.Color _color;

        public string Name { get { return _name; } }
        public int Population { get { return _population; } }
        public bool IsAlive { get { return _isAlive; } }

        public City(Citydata citydata)
        {
            _name = citydata.name;
            _population = citydata.population;
            _money = citydata.money;
            _isAlive = true;
            _maps = new List<Map>();
            var r = Singleton.Random;
            _color = new Color((byte)r.Next(0, 255), (byte)r.Next(0, 255), (byte)r.Next(0, 255));

            var fieldMap = Singleton.FieldMap;

            Map m = new Map(citydata.id, citydata.point.x, citydata.point.y, _color, citydata.link);
            m.SetCity(this);
            _maps.Add(m);
            fieldMap.SetMap(m);
        }

        public List<Map> GetMaps()
        {
            return _maps;
        }

        public void CombinationCity(City lose)
        {
            addMaps(lose.GetMaps());
            _population += lose.Population;
        }

        public void Lose()
        {
            _isAlive = false;
        }

        private void addMaps(List<Map> maps)
        {
            foreach(var m in maps)
            {
                m.SetCity(this);
            }
            _maps.AddRange(maps);
        }

        public void OnMouse(Vector2DF pos)
        {
            foreach (var m in _maps)
            {
                if(m.IsOnMouse(pos))
                {
                    var info = Singleton.InfomationWindow;
                    info.AppendText(pos, _name + "\r\n人口" + _population.ToString() + "\r\n面積" + _money.ToString());
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

        public List<City> GetLinkedCities()
        {
            List<City> cities = new List<City>();
            foreach (var m in _maps)
            {
                foreach(var linkedMap in m.GetLinkdMap())
                {
                    var c = linkedMap.GetCity();
                    if (cities.Contains(c) == false && c != this)
                    {
                        cities.Add(c);
                    }
                }
            }
            return cities;
        }

        public void PaintAttackColor()
        {
            var color = new asd.Color(200, 0, 0);
            foreach(var m in _maps)
            {
                m.SetColor(color);
            }
        }

        public void PaintDeffenceColor()
        {
            var color = new asd.Color(0, 0, 200);
            foreach (var m in _maps)
            {
                m.SetColor(color);
            }
        }

        public void ClearPaint()
        {
            foreach (var m in _maps)
            {
                m.SetColor(_color);
            }
        }

        public asd.Color GetColor()
        {
            return _color;
        }
    }
}
