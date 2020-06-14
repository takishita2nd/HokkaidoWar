using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HokkaidoWar
{
    class Map
    {
        private int _x;
        private int _y;
        private asd.GeometryObject2D _geometryObj;

        private readonly int width = 24;
        private readonly int height = 24;
        private readonly int offsetx = 50;
        private readonly int offsety = 50;

        public Map(int x, int y, asd.Color color)
        {
            _x = x;
            _y = y;
            _geometryObj = new asd.GeometryObject2D();
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
    }
}
