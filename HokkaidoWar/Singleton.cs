using HokkaidoWar.Model;
using HokkaidoWar.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HokkaidoWar
{
    class Singleton
    {
        private static GameData _gameData = null;
        private static InfomationWindow _info = null;
        private static InfomationWindow _gameInfo = null;
        private static Random random = null;
        private static asd.Font _font = null;
        private static asd.Font _LargeFont = null;
        private static FieldMap _map = null;
        private static asd.Texture2D _texture_gu = null;
        private static asd.Texture2D _texture_choki = null;
        private static asd.Texture2D _texture_par = null;
        private static asd.Texture2D _texture_gu2 = null;
        private static asd.Texture2D _texture_choki2 = null;
        private static asd.Texture2D _texture_par2 = null;

        public static GameData GameData
        {
            get
            {
                if (_gameData == null)
                {
                    _gameData = new GameData();
                }
                return _gameData;
            }
        }

        public static Random Random
        {
            get
            {
                if (random == null)
                {
                    random = new Random();
                }
                return random;
            }
        }

        public static asd.Font Font
        {
            get
            {
                if (_font == null)
                {
                    _font = asd.Engine.Graphics.CreateFont("FontText.aff");
                }
                return _font;
            }
        }

        public static asd.Font LargeFont
        {
            get
            {
                if (_LargeFont == null)
                {
                    _LargeFont = asd.Engine.Graphics.CreateFont("FontTextLarge.aff");
                }
                return _LargeFont;
            }
        }

        public static InfomationWindow InfomationWindow
        {
            get
            {
                if (_info == null)
                {
                    _info = new InfomationWindow();
                }
                return _info;
            }
        }

        public static InfomationWindow GameProcessInfomation
        {
            get
            {
                if (_gameInfo == null)
                {
                    _gameInfo = new InfomationWindow();
                }
                return _gameInfo;
            }
        }

        public static FieldMap FieldMap
        {
            get
            {
                if (_map == null)
                {
                    _map = new FieldMap();
                }
                return _map;
            }
        }

        public static asd.Texture2D ImageGu
        {
            get
            {
                if (_texture_gu == null)
                {
                    _texture_gu = asd.Engine.Graphics.CreateTexture2D("image_gu.png");
                }
                return _texture_gu;
            }
        }

        public static asd.Texture2D ImageChoki
        {
            get
            {
                if (_texture_choki == null)
                {
                    _texture_choki = asd.Engine.Graphics.CreateTexture2D("image_choki.png");
                }
                return _texture_choki;
            }
        }

        public static asd.Texture2D ImagePar
        {
            get
            {
                if (_texture_par == null)
                {
                    _texture_par = asd.Engine.Graphics.CreateTexture2D("image_par.png");
                }
                return _texture_par;
            }
        }

        public static asd.Texture2D ImageGu2
        {
            get
            {
                if (_texture_gu2 == null)
                {
                    _texture_gu2 = asd.Engine.Graphics.CreateTexture2D("image_gu2.png");
                }
                return _texture_gu2;
            }
        }

        public static asd.Texture2D ImageChoki2
        {
            get
            {
                if (_texture_choki2 == null)
                {
                    _texture_choki2 = asd.Engine.Graphics.CreateTexture2D("image_choki2.png");
                }
                return _texture_choki2;
            }
        }

        public static asd.Texture2D ImagePar2
        {
            get
            {
                if (_texture_par2 == null)
                {
                    _texture_par2 = asd.Engine.Graphics.CreateTexture2D("image_par2.png");
                }
                return _texture_par2;
            }
        }
    }
}
