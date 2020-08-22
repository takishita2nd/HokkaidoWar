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
        private bool _isAlive;
        private List<Map> _maps = null;
        private asd.Color _color;

        public string Name { get { return _name; } }
        public int Population { get { return _population; } }
        public bool IsAlive { get { return _isAlive; } }

        public City(string name, Point[] points, int population)
        {
            _name = name;
            _population = population;
            _isAlive = true;
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

        public void OnMouse(asd.Vector2DF pos)
        {
            foreach (var m in _maps)
            {
                if(m.IsOnMouse(pos))
                {
                    var info = Singleton.GetInfomationWindow();
                    info.AppendText(pos, _name + "\r\n" + _population.ToString());
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
    }
}
