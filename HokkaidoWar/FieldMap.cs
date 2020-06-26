using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HokkaidoWar
{
    class FieldMap
    {
        public int MaxX { get { return 44; } }
        public int MaxY { get { return 35; } }

        private Map[,] _map;
        public FieldMap()
        {
            _map = new Map[44, 35];
        }

        public Map GetMap(int x, int y)
        {
            return _map[x, y];
        }

        public void SetMap(Map map)
        {
            _map[map.X, map.Y] = map;
        }

        // Test
        public void onMouse(Map map)
        {
            for(int x = 0; x < MaxX; x++)
            {
                for(int y = 0; y < MaxY; y++)
                {
                    if(_map[x,y] != null)
                    {
                        _map[x, y].unlinkedMap();
                    }
                }
            }
            if(map.X > 0)
            {
                if (_map[map.X + 1, map.Y] != null)
                {
                    _map[map.X + 1, map.Y].linkedMap();
                }
            }
            if (map.X < MaxX)
            {
                if (_map[map.X - 1, map.Y] != null)
                {
                    _map[map.X - 1, map.Y].linkedMap();
                }
            }
            if (map.Y > 0)
            {
                if (_map[map.X, map.Y - 1] != null)
                {
                    _map[map.X, map.Y - 1].linkedMap();
                }
            }
            if (map.Y < MaxY)
            {
                if (_map[map.X, map.Y + 1] != null)
                {
                    _map[map.X, map.Y + 1].linkedMap();
                }
            }
        }
    }
}
