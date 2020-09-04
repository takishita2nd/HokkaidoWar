using HokkaidoWar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HokkaidoWar.Scene
{
    class MainScene : asd.Scene
    {
        enum GameStatus
        {
            SelectCity,
            ActionEnemy,
            ActionPlayer,
            ShowResult,
            GameEnd,
            GameOver
        }
        GameStatus gameStatus;

        MapData mapData = null;
        List<City> cities = null;
        List<City> aliveCities = null;
        Battle _battle = null;

        Player _player = null;

        public MainScene()
        {
            gameStatus = GameStatus.SelectCity;
            mapData = FileAccess.Load();
        }

        protected override void OnRegistered()
        {
            var layer = Singleton.GetMainSceneLayer();
            AddLayer(layer);

            // 下地
            var background = new asd.GeometryObject2D();
            layer.AddObject(background);
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
            aliveCities = _battle.GetAliveCityList();
        }

        protected override void OnUpdated()
        {
            asd.Vector2DF pos = asd.Engine.Mouse.Position;

            switch (gameStatus)
            {
                case GameStatus.SelectCity:
                    cycleProcessSelectCity(pos);
                    break;
                case GameStatus.ActionEnemy:
                    cycleProcessActionEnemy(pos);
                    break;
                case GameStatus.ActionPlayer:
                    cycleProcessActionPlayer(pos);
                    break;
                case GameStatus.ShowResult:
                    break;
                case GameStatus.GameEnd:
                    cycleProcessGameEnd();
                    break;
                case GameStatus.GameOver:
                    cycleProcessGameOver(pos);
                    break;
            }

            if (asd.Engine.Mouse.LeftButton.ButtonState == asd.ButtonState.Push)
            {
                switch (gameStatus)
                {
                    case GameStatus.SelectCity:
                        onClickMouseSelectCity(pos);
                        break;
                    case GameStatus.ActionEnemy:
                        break;
                    case GameStatus.ActionPlayer:
                        onClickMouseActionPlayer(pos);
                        break;
                    case GameStatus.ShowResult:
                        onClickMouseShowResult();
                        break;
                    case GameStatus.GameEnd:
                        break;
                    case GameStatus.GameOver:
                        break;
                }
            }
        }

        private bool isOnMaouseMap(asd.Vector2DF pos)
        {
            bool ret = false;
            foreach (var city in aliveCities)
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
                foreach (var city in aliveCities)
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
                foreach (var city in aliveCities)
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

        private void cycleProcessSelectCity(asd.Vector2DF pos)
        {
            var info = Singleton.GetInfomationWindow();
            info.ShowText(pos, "都市を選択してください\r\n");
            onMouse(pos);
        }

        private void cycleProcessActionEnemy(asd.Vector2DF pos)
        {
            if (_player.City.Equals(_battle.GetActionCity()))
            {
                gameStatus = GameStatus.ActionPlayer;
            }
            else
            {
                Thread.Sleep(200);
                _battle.NextTurn();
                aliveCities = _battle.GetAliveCityList();
                if (aliveCities.Contains(_player.City) == false)
                {
                    gameStatus = GameStatus.GameOver;
                }
            }
        }

        private void cycleProcessActionPlayer(asd.Vector2DF pos)
        {
            _battle.MyTurn(_player.City);
            _player.City.PaintAttackColor();
            var info = Singleton.GetInfomationWindow();
            info.ShowText(pos, "都市を選択してください\r\n");
            if (_player.City.IsOnMouse(pos))
            {
                _player.City.OnMouse(pos);
            }
            else
            {
                var linkedCities = _player.City.GetLinkedCities();
                foreach (var city in linkedCities)
                {
                    if (city.IsOnMouse(pos))
                    {
                        city.OnMouse(pos);
                        city.PaintDeffenceColor();
                    }
                    else
                    {
                        city.ClearPaint();
                    }
                }
            }
        }

        private void cycleProcessGameEnd()
        {
            var gameinfo = Singleton.GetGameProcessInfomation();
            gameinfo.ShowText(_player.City.GetPosition(), string.Empty);
            var info = Singleton.GetInfomationWindow();
            info.ShowText(aliveCities[0].GetPosition(), "ゲームが終了しました\r\n");
            info.ShowText(aliveCities[0].GetPosition(), aliveCities[0].Name + "の勝利です\r\n");
        }

        private void cycleProcessGameOver(asd.Vector2DF pos)
        {
            var gameinfo = Singleton.GetGameProcessInfomation();
            gameinfo.ShowText(_player.City.GetPosition(), string.Empty);
            var info = Singleton.GetInfomationWindow();
            info.ShowText(pos, "敗北しました\r\n");
        }

        private void onClickMouseSelectCity(asd.Vector2DF pos)
        {
            var selectcity = getCity(pos);
            if (selectcity != null)
            {
                _player = new Player(selectcity);
                gameStatus = GameStatus.ActionEnemy;
            }
        }

        private City _target = null;
        private void onClickMouseActionPlayer(asd.Vector2DF pos)
        {
            var info = Singleton.GetInfomationWindow();
            info.ShowText(pos, String.Empty);
            var linkedCities = _player.City.GetLinkedCities();
            foreach (var city in linkedCities)
            {
                if (city.IsOnMouse(pos))
                {
                    _target = city;
                    _battle.MyTrunAttack(_player.City, _target);
                    gameStatus = GameStatus.ShowResult;
                }
            }
        }

        private void onClickMouseShowResult()
        {
            _battle.MyTurnEnd();
            aliveCities = _battle.GetAliveCityList();
            if (aliveCities.Count <= 1)
            {
                gameStatus = GameStatus.GameEnd;
            }
            else
            {
                gameStatus = GameStatus.ActionEnemy;
            }
        }
    }
}
