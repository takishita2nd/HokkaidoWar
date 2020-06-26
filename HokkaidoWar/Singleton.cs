using asd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HokkaidoWar
{
    class Singleton
    {
        private static InfomationWindow _info = null;
        private static Random random = null;
        private static asd.Font _font = null;
        private static FieldMap _map = null;

        public static Random GetRandom()
        {
            if(random == null)
            {
                random = new Random();
            }
            return random;
        }

        public static asd.Font GetFont()
        {
            if(_font == null)
            {
                _font = asd.Engine.Graphics.CreateFont("FontText.aff");
            }
            return _font;
        }

        public static InfomationWindow GetInfomationWindow()
        {
            if (_info == null)
            {
                _info = new InfomationWindow();
            }
            return _info;
        }

        public static FieldMap GetFieldMap()
        {
            if (_map == null)
            {
                _map = new FieldMap();
            }
            return _map;
        }
    }
}
