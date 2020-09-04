﻿using asd;
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
        private static FieldMap _map = null;
        private static Layer2D _mainSceneLayer = null;

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

        public static Layer2D GetMainSceneLayer()
        {
            if(_mainSceneLayer == null)
            {
                _mainSceneLayer = new Layer2D();
            }
            return _mainSceneLayer;
        }
    }
}
