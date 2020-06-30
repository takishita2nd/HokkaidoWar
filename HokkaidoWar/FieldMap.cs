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
            if(x < 0 || x >= MaxX || y < 0 || y >= MaxY)
            {
                return null;
            }
            else
            {
                return _map[x, y];
            }
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
            if(map.Up != null)
            {
                map.Up.linkedMap();
            }
            if (map.Down != null)
            {
                map.Down.linkedMap();
            }
            if (map.Left != null)
            {
                map.Left.linkedMap();
            }
            if (map.Right != null)
            {
                map.Right.linkedMap();
            }
        }
    }
}
