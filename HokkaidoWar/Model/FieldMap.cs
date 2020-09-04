using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HokkaidoWar.Model
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
    }
}
