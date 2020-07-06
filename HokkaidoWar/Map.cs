using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HokkaidoWar
{
    class Map
    {
        private int _x;
        private int _y;
        private City _city;
        private asd.Color _color;
        private asd.GeometryObject2D _geometryObj;

        private readonly int width = 24;
        private readonly int height = 24;
        private readonly int offsetx = 50;
        private readonly int offsety = 50;

        public int X { get { return _x; } }
        public int Y { get { return _y; } }

        public Map Up { 
            get {
                var field = Singleton.GetFieldMap();
                if(_x == 18 && _y == 0)
                {
                    return field.GetMap(23, 0);
                }
                else if(_x == 23 && _y == 0)
                {
                    return field.GetMap(18, 0);
                }
                else if (_x == 20 && _y == 1)
                {
                    return field.GetMap(18, 0);
                }
                else
                {
                    return field.GetMap(_x, _y - 1);
                }
            }
        }

        public Map Down
        {
            get
            {
                var field = Singleton.GetFieldMap();
                if(_x == 18 && _y == 0)
                {
                    return field.GetMap(19, 1);
                }
                else
                {
                    return field.GetMap(_x, _y + 1);
                }
            }
        }

        public Map Left
        {
            get
            {
                var field = Singleton.GetFieldMap();
                if (_x == 2 && _y == 29)
                {
                    return field.GetMap(0, 29);
                }
                else if(_x == 19 && _y == 1)
                {
                    return field.GetMap(18, 0);
                }
                else if (_x == 23 && _y == 0)
                {
                    return field.GetMap(20, 1);
                }
                else
                {
                    return field.GetMap(_x - 1, _y);
                }
            }
        }

        public Map Right
        {
            get
            {
                var field = Singleton.GetFieldMap();
                if (_x == 0 && _y == 29)
                {
                    return field.GetMap(2, 29);
                }
                else if(_x == 18 && _y == 0)
                {
                    return field.GetMap(20, 1);
                }
                else if (_x == 20 && _y == 1)
                {
                    return field.GetMap(23, 0);
                }
                else
                {
                    return field.GetMap(_x + 1, _y);
                }
            }
        }

        public Map(int x, int y, asd.Color color)
        {
            _x = x;
            _y = y;
            _geometryObj = new asd.GeometryObject2D();
            _color = color;
            _geometryObj.Color = color;
            asd.Engine.AddObject2D(_geometryObj);
            var rect = new asd.RectangleShape();
            rect.DrawingArea = new asd.RectF(width * _x + offsetx, height * _y + offsety, width, height);
            _geometryObj.Shape = rect;
        }

        public void SetColor(asd.Color color)
        {
            _geometryObj.Color = color;
        }

        public void SetCity(City city)
        {
            _city = city;
        }

        public City GetCity()
        {
            return _city;
        }

        public bool IsOnMouse(asd.Vector2DF pos)
        {
            if (pos.X > width * _x + offsetx && pos.X < width * (_x + 1) + offsetx
                && pos.Y > height * _y + offsety && pos.Y < height * (_y + 1) + offsety)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Test
        public void linkedMap()
        {
            var changeColor = new asd.Color(200, 200, 200);
           _geometryObj.Color = changeColor;
        }

        public void unlinkedMap()
        {
            _geometryObj.Color = _color;
        }
    }
}
