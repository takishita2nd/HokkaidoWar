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
        Battle _battle = null;

        Player _player = null;

        enum GameStatus
        {
            SelectCity,
            ActionEnemy,
            ActionPlayer,
            ShowResult,
            GameEnd
        }

        GameStatus gameStatus;

        public HokkaidoWar()
        {
            gameStatus = GameStatus.SelectCity;
            mapData = FileAccess.Load();
        }

        public void Run()
        {
            asd.Engine.Initialize("北海道大戦", 1200, 1000, new asd.EngineOption());

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

            _battle = new Battle(cities);

            while (asd.Engine.DoEvents())
            {
                asd.Vector2DF pos = asd.Engine.Mouse.Position;

                switch (gameStatus)
                {
                    case GameStatus.SelectCity:
                        var info = Singleton.GetInfomationWindow();
                        info.ShowText(pos, "都市を選択してください\r\n");
                        onMouse(pos);
                        break;
                }

                if (asd.Engine.Mouse.LeftButton.ButtonState == asd.ButtonState.Push)
                {
                    switch (gameStatus)
                    {
                        case GameStatus.SelectCity:
                            _player = new Player(getCity(pos));
                            gameStatus = GameStatus.ActionEnemy;
                            break;
                    }
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

        private void onMouse(asd.Vector2DF pos)
        {
            if (isOnMaouseMap(pos))
            {
                foreach (var city in cities)
                {
                    city.OnMouse(pos);
                }
            }
        }

        private City getCity(asd.Vector2DF pos)
        {
            City retcity = null;
            if (isOnMaouseMap(pos))
            {
                foreach(var city in cities)
                {
                    if (city.IsOnMouse(pos))
                    {
                        retcity = city;
                        break;
                    }
                }
            }
            return retcity;
        }
    }
}
