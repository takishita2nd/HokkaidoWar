using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HokkaidoWar
{
    class HokkaidoWar
    {
        MapData mapData = null;
        public HokkaidoWar()
        {
            mapData = FileAccess.Load();
        }

        public void Run()
        {
            asd.Engine.Initialize("北海道大戦", 1800, 1000, new asd.EngineOption());

            // 下地
            var background = new asd.GeometryObject2D();
            asd.Engine.AddObject2D(background);
            var bgRect = new asd.RectangleShape();
            bgRect.DrawingArea = new asd.RectF(0, 0, 1800, 1000);
            background.Shape = bgRect;

            var r = new Random();
            foreach (var map in mapData.list)
            {
                var color = new asd.Color((byte)r.Next(0, 255), (byte)r.Next(0, 255), (byte)r.Next(0, 255));
                foreach (var point in map.point)
                {
                    var geometryObj = new asd.GeometryObject2D();
                    geometryObj.Color = color;
                    asd.Engine.AddObject2D(geometryObj);
                    var rect = new asd.RectangleShape();
                    rect.DrawingArea = new asd.RectF(24 * point.x + 50, 24 * point.y + 50, 24, 24);
                    geometryObj.Shape = rect;
                }
            }

            while (asd.Engine.DoEvents())
            {
                asd.Engine.Update();
            }
            asd.Engine.Terminate();
        }
    }
}
