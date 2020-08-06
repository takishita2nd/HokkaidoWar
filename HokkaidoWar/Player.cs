using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HokkaidoWar
{
    class Player
    {
        private City _city;

        public City City { get { return _city; } }

        public Player(City city)
        {
            _city = city;
        }
    }
}
