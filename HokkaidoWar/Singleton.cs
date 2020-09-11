using HokkaidoWar.Model;
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
        private static InfomationWindow _gameInfo = null;
        private static Random random = null;
        private static asd.Font _font = null;
        private static asd.Font _LargeFont = null;
        private static FieldMap _map = null;
        private static asd.Layer2D _mainSceneLayer = null;
        private static asd.Texture2D _texture_gu = null;
        private static asd.Texture2D _texture_choki = null;
        private static asd.Texture2D _texture_par = null;

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

        public static asd.Font GetLargeFont()
        {
            if (_LargeFont == null)
            {
                _LargeFont = asd.Engine.Graphics.CreateFont("FontTextLarge.aff");
            }
            return _LargeFont;
        }

        public static InfomationWindow GetInfomationWindow()
        {
            if (_info == null)
            {
                _info = new InfomationWindow();
            }
            return _info;
        }

        public static InfomationWindow GetGameProcessInfomation()
        {
            if (_gameInfo == null)
            {
                _gameInfo = new InfomationWindow();
            }
            return _gameInfo;
        }

        public static FieldMap GetFieldMap()
        {
            if (_map == null)
            {
                _map = new FieldMap();
            }
            return _map;
        }

        public static asd.Layer2D GetMainSceneLayer()
        {
            if(_mainSceneLayer == null)
            {
                _mainSceneLayer = new asd.Layer2D();
            }
            return _mainSceneLayer;
        }

        public static asd.Texture2D GetImageGu()
        {
            if (_texture_gu == null)
            {
                _texture_gu = asd.Engine.Graphics.CreateTexture2D("image_gu.png");
            }
            return _texture_gu;
        }

        public static asd.Texture2D GetImageChoki()
        {
            if (_texture_choki == null)
            {
                _texture_choki = asd.Engine.Graphics.CreateTexture2D("image_choki.png");
            }
            return _texture_choki;
        }

        public static asd.Texture2D GetImagePar()
        {
            if (_texture_par == null)
            {
                _texture_par = asd.Engine.Graphics.CreateTexture2D("image_par.png");
            }
            return _texture_par;
        }
    }
}
