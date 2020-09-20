using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HokkaidoWar.Model
{
    class Player
    {
        public City City { get; }

        public Player(City city)
        {
            City = city;
        }
    }
}
