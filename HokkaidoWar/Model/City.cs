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
        private bool _isAlive;
        private List<Map> _maps = null;
        private Color _color;
        private int _money;
        private int _power;
        private float _bonus;

        public string Name { get { return _name; } }
        public int Population
        {
            get
            {
                var population = 0;
                foreach(var m in _maps)
                {
                    population += m.Population;
                }
                return population;
            }
        }
        public int Area
        {
            get
            {
                var area = 0;
                foreach (var m in _maps)
                {
                    area += m.Area;
                }
                return area;
            }
        }
        public int Money {  get { return _money; } }
        public int Power { get { return (int)(_power * _bonus); } }
        public bool IsAlive { get { return _isAlive; } }

        public City(Citydata citydata)
        {
            _money = 0;
            _power = citydata.population;
            _bonus = 1.0f;
            _name = citydata.name;
            _isAlive = true;
            _maps = new List<Map>();
            var r = Singleton.Random;
            _color = new Color((byte)r.Next(0, 255), (byte)r.Next(0, 255), (byte)r.Next(0, 255));

            var fieldMap = Singleton.FieldMap;

            Map m = new Map(citydata.id, citydata.point.x, citydata.point.y, citydata.population, citydata.money, _color, citydata.link);
            m.SetCity(this);
            _maps.Add(m);
            fieldMap.SetMap(m);
        }

        public List<Map> GetMaps()
        {
            return _maps;
        }

        public void AddMap(Map map)
        {
            _maps.Add(map);
            map.SetCity(this);
            map.SetColor(_color);
        }

        public void LostMap(Map map)
        {
            if(_maps.Contains(map))
            {
                _maps.Remove(map);
                if(_maps.Count == 0)
                {
                    Lose();
                }
            }
        }

        public void Lose()
        {
            _isAlive = false;
        }

        public void AddMoney()
        {
            _money += Area;
        }

        public void PayMoney(int money)
        {
            _money -= money;
        }

        public void AddPower(int power)
        {
            _power += power;
        }

        public void ResetPower()
        {
            _power = Population;
        }

        public void UpdatePower(int power)
        {
            if(power > Population)
            {
                _power = power;
            }
            else
            {
                _power = Population;
            }
        }

        private void addMaps(List<Map> maps)
        {
            foreach(var m in maps)
            {
                m.SetCity(this);
            }
            _maps.AddRange(maps);
        }


        public void OnMouse(Vector2DF pos, bool allInfo)
        {
            foreach (var m in _maps)
            {
                if(m.IsOnMouse(pos))
                {
                    if(allInfo)
                    {
                        var info = Singleton.InfomationWindow;
                        info.AppendText(pos, _name +
                            "\r\n  金" + Money.ToString() +
                            "\r\n  戦力" + Power.ToString() +
                            "\r\n  人口" + Population.ToString() +
                            "\r\n  面積" + Area.ToString());
                    }
                    else
                    {
                        var info = Singleton.InfomationWindow;
                        info.AppendText(pos, _name +
                            "\r\n  人口" + Population.ToString() +
                            "\r\n  面積" + Area.ToString());
                    }
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

        public List<Map> GetLinkedMaps()
        {
            List<Map> maps = new List<Map>();
            foreach (var m in _maps)
            {
                foreach (var linkedMap in m.GetLinkdMap())
                {
                    if(maps.Contains(linkedMap) == false && linkedMap.GetCity() != this)
                    {
                        maps.Add(linkedMap);
                    }
                }
            }
            return maps;
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
