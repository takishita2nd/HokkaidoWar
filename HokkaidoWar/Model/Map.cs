﻿using asd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HokkaidoWar.Model
{
    class Map
    {
        private int _id;
        private int _x;
        private int _y;
        private int[] _link;
        private City _city;
        private Color _color;
        private GeometryObject2D _geometryObj;

        private readonly int width = 12;
        private readonly int height = 12;
        private readonly int offsetx = 50;
        private readonly int offsety = 50;
        private readonly int centerOffset = 6;

        public int Id { get { return _id; } }
        public int X { get { return _x; } }
        public int Y { get { return _y; } }
        public int CenterX { get { return _x + centerOffset; } }
        public int CenterY { get { return _y + centerOffset; } }

        public Map(int id, int x, int y, asd.Color color, int[] link)
        {   
            _id = id;
            _x = x;
            _y = y;
            _link = link;
            _color = color;
        }

        public void AddLayer(Layer2D layer)
        {
            _geometryObj = new GeometryObject2D();
            _geometryObj.DrawingPriority = 10;
            _geometryObj.Color = _color;
            var rect = new RectangleShape();
            rect.DrawingArea = new RectF(_x, _y, width, height);
            _geometryObj.Shape = rect;

            layer.AddObject(_geometryObj);
        }

        public void SetColor(asd.Color color)
        {
            _geometryObj.Color = color;
        }

        public void SetCity(City city)
        {
            _city = city;
            _color = city.GetColor();
        }

        public City GetCity()
        {
            return _city;
        }

        public List<Map> GetLinkdMap()
        {
            List<Map> maps = new List<Map>();
            foreach(var i in _link)
            {
                maps.Add(Singleton.FieldMap.GetMap(i));
            }
            return maps;
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
