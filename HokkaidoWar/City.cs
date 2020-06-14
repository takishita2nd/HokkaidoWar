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
        private List<Map> _maps = null;
        private asd.Color _color;

        public City(string name, Point[] points)
        {
            _name = name;
            _maps = new List<Map>();
            var r = Singleton.GetRandom();
            _color = new asd.Color((byte)r.Next(0, 255), (byte)r.Next(0, 255), (byte)r.Next(0, 255));

            foreach (var p in points)
            {
                Map m = new Map(p.x, p.y, _color);
                _maps.Add(m);
            }
        }

        public void OnMouse(asd.Vector2DF pos)
        {
            foreach(var m in _maps)
            {
                if(m.IsOnMouse(pos))
                {
                    var info = Singleton.GetInfomationWindow();
                    info.ShowText(pos, _name);
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
    }
}
