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
        private int maxId = 0;
        private Map[] _map;
        public FieldMap()
        {
            maxId = Singleton.GameData.MapData.citydata.Length + 1;
            _map = new Map[maxId];
        }

        public Map GetMap(int id)
        {
            if(id < 0 || id >= maxId)
            {
                return null;
            }
            else
            {
                return _map[id];
            }
        }

        public Map[] GetAllMaps()
        {
            return _map;
        }

        public void SetMap(Map map)
        {
            _map[map.Id] = map;
        }
    }
}
