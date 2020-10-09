using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace HokkaidoWar.Model
{
    class BattleIcon
    {
        public enum Icon
        {
            Gu,
            Choki,
            Par
        }

        public enum Position
        {
            Attack,
            Deffence
        }

        private asd.TextureObject2D _image = null;
        private Icon _icon;
        private Position _position;
        private int _x;
        private int _y;
        private int width = 80;
        private int height = 80;

        public BattleIcon(Icon icon, Position pos)
        {
            _icon = icon;
            _position = pos;
            _image = new asd.TextureObject2D();
            if (pos == Position.Attack)
            {
                _y = 500;
                switch (icon)
                {
                    case Icon.Gu:
                        _x = 300;
                        _image.Texture = Singleton.ImageGu;
                        break;
                    case Icon.Choki:
                        _x = 450;
                        _image.Texture = Singleton.ImageChoki;
                        break;
                    case Icon.Par:
                        _x = 600;
                        _image.Texture = Singleton.ImagePar;
                        break;
                }
                _image.Position = new asd.Vector2DF(_x, _y);
            }
            else
            {
                _y = 250;
                switch (icon)
                {
                    case Icon.Gu:
                        _x = 300;
                        _image.Texture = Singleton.ImageGu;
                        break;
                    case Icon.Choki:
                        _x = 450;
                        _image.Texture = Singleton.ImageChoki;
                        break;
                    case Icon.Par:
                        _x = 600;
                        _image.Texture = Singleton.ImagePar;
                        break;
                }
                _image.Position = new asd.Vector2DF(_x, _y);
            }
        }

        public void AddLayer(asd.Layer2D layer)
        {
            layer.AddObject(_image);
        }

        public void Show()
        {
            switch (_icon)
            {
                case Icon.Gu:
                    _image.Texture = Singleton.ImageGu;
                    break;
                case Icon.Choki:
                    _image.Texture = Singleton.ImageChoki;
                    break;
                case Icon.Par:
                    _image.Texture = Singleton.ImagePar;
                    break;
            }
        }

        public void Hide()
        {
            _image.Texture = null;
        }

        public void OnMouse(asd.Vector2DF pos)
        {
            if (IsOnMouse(pos))
            {
                switch (_icon)
                {
                    case Icon.Gu:
                        _image.Texture = Singleton.ImageGu2;
                        break;
                    case Icon.Choki:
                        _image.Texture = Singleton.ImageChoki2;
                        break;
                    case Icon.Par:
                        _image.Texture = Singleton.ImagePar2;
                        break;
                }
            }
            else
            {
                switch (_icon)
                {
                    case Icon.Gu:
                        _image.Texture = Singleton.ImageGu;
                        break;
                    case Icon.Choki:
                        _image.Texture = Singleton.ImageChoki;
                        break;
                    case Icon.Par:
                        _image.Texture = Singleton.ImagePar;
                        break;
                }
            }
        }

        public bool IsOnMouse(asd.Vector2DF pos)
        {
            if (pos.X > _x && pos.X < width + _x
                && pos.Y > _y && pos.Y < height + _y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetIcon(Icon icon)
        {
            _icon = icon;
        }
    }
}
