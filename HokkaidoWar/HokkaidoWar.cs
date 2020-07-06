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
        List<City> cities = null;
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

            cities = new List<City>();
            var r = new Random();
            foreach (var map in mapData.list)
            {
                City city = new City(map.name, map.point, map.population);
                cities.Add(city);
            }

            while (asd.Engine.DoEvents())
            {
                FieldMap fieldMap = Singleton.GetFieldMap();
                fieldMap.unlinkMap();
                asd.Vector2DF pos = asd.Engine.Mouse.Position;
                if (isOnMaouseMap(pos))
                {
                    foreach (var city in cities)
                    {
                        city.OnMouse(pos);
                    }
                }
                else
                {
                    var info = Singleton.GetInfomationWindow();
                    info.ShowText(pos, string.Empty);
                }

                asd.Engine.Update();
            }
            asd.Engine.Terminate();
        }

        private bool isOnMaouseMap(asd.Vector2DF pos)
        {
            bool ret = false;
            foreach (var city in cities)
            {
                if (city.IsOnMouse(pos))
                {
                    ret = true;
                }
            }
            return ret;
        }
    }
}
