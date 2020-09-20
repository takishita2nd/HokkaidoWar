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
        private GameData gameData = null;
        public MainScene()
        {
            gameData = Singleton.GetGameData();

            if(gameData.gameStatus == GameData.GameStatus.None)
            {
                MapData mapData = FileAccess.Load();

                var r = new Random();
                foreach (var map in mapData.list)
                {
                    City city = new City(map.name, map.point, map.population);
                    gameData.Cities.Add(city);
                }

                gameData.Battleinitialize();
                gameData.gameStatus = GameData.GameStatus.SelectCity;
            }
        }

        protected override void OnRegistered()
        {
            var layer = new asd.Layer2D();
            AddLayer(layer);

            // 下地
            var background = new asd.GeometryObject2D();
            layer.AddObject(background);
            var bgRect = new asd.RectangleShape();
            bgRect.DrawingArea = new asd.RectF(0, 0, 1800, 1000);
            background.Shape = bgRect;

            foreach(var c in gameData.Battle.GetAliveCityList())
            {
                var maps = c.GetMaps();
                foreach(var m in maps)
                {
                    m.AddLayer(layer);
                }
            }

            var info = Singleton.GetInfomationWindow();
            info.AddLayer(layer);
            var info2 = Singleton.GetGameProcessInfomation();
            info2.AddLayer(layer);
        }

        protected override void OnUpdated()
        {
            asd.Vector2DF pos = asd.Engine.Mouse.Position;

            switch (gameData.gameStatus)
            {
                case GameData.GameStatus.SelectCity:
                    cycleProcessSelectCity(pos);
                    break;
                case GameData.GameStatus.ActionEnemy:
                    cycleProcessActionEnemy(pos);
                    break;
                case GameData.GameStatus.ActionPlayer:
                    cycleProcessActionPlayer(pos);
                    break;
                case GameData.GameStatus.ShowResult:
                    break;
                case GameData.GameStatus.GameEnd:
                    cycleProcessGameEnd();
                    break;
                case GameData.GameStatus.GameOver:
                    cycleProcessGameOver(pos);
                    break;
            }

            if (asd.Engine.Mouse.LeftButton.ButtonState == asd.ButtonState.Push)
            {
                switch (gameData.gameStatus)
                {
                    case GameData.GameStatus.SelectCity:
                        onClickMouseSelectCity(pos);
                        break;
                    case GameData.GameStatus.ActionEnemy:
                        break;
                    case GameData.GameStatus.ActionPlayer:
                        onClickMouseActionPlayer(pos);
                        break;
                    case GameData.GameStatus.ShowResult:
                        onClickMouseShowResult();
                        break;
                    case GameData.GameStatus.GameEnd:
                        break;
                    case GameData.GameStatus.GameOver:
                        break;
                }
            }
        }

        private void onMouse(asd.Vector2DF pos)
        {
            gameData.OnMaouseCity(pos);
        }

        private void cycleProcessSelectCity(asd.Vector2DF pos)
        {
            var info = Singleton.GetInfomationWindow();
            info.ShowText(pos, "都市を選択してください\r\n");
            onMouse(pos);
        }

        private void cycleProcessActionEnemy(asd.Vector2DF pos)
        {
            if (gameData.IsPlayerTrun())
            {
                gameData.gameStatus = GameData.GameStatus.ActionPlayer;
            }
            else
            {
                Thread.Sleep(200);
                gameData.PlayNextCity();
                if (gameData.IsPlayerAlive() == false)
                {
                    gameData.gameStatus = GameData.GameStatus.GameOver;
                }
            }
        }

        private void cycleProcessActionPlayer(asd.Vector2DF pos)
        {
            gameData.PlayPlayer();
            var info = Singleton.GetInfomationWindow();
            info.ShowText(pos, "都市を選択してください\r\n");
            if (gameData.IsOnMousePlayerCity(pos))
            {
                gameData.OnMousePlayerCity(pos);
            }
            else
            {
                var linkedCities = gameData.GetPlayerLinkedCities();
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
            gameinfo.ShowText(gameData.GetPlayCityPosition(), string.Empty);
            var info = Singleton.GetInfomationWindow();
            info.ShowText(gameData.AliveCities[0].GetPosition(), "ゲームが終了しました\r\n");
            info.ShowText(gameData.AliveCities[0].GetPosition(), gameData.AliveCities[0].Name + "の勝利です\r\n");
        }

        private void cycleProcessGameOver(asd.Vector2DF pos)
        {
            var gameinfo = Singleton.GetGameProcessInfomation();
            gameinfo.ShowText(gameData.GetPlayCityPosition(), string.Empty);
            var info = Singleton.GetInfomationWindow();
            info.ShowText(pos, "敗北しました\r\n");
        }

        private void onClickMouseSelectCity(asd.Vector2DF pos)
        {
            var selectcity = gameData.GetSelectedCity(pos);
            if (selectcity != null)
            {
                gameData.CreatePlayer(selectcity);
                gameData.gameStatus = GameData.GameStatus.ActionEnemy;
            }
        }

        private City _target = null;
        private void onClickMouseActionPlayer(asd.Vector2DF pos)
        {
            var info = Singleton.GetInfomationWindow();
            info.ShowText(pos, String.Empty);
            var linkedCities = gameData.GetPlayerLinkedCities();
            foreach (var city in linkedCities)
            {
                if (city.IsOnMouse(pos))
                {
                    _target = city;
                    gameData.PlayerAttackCity(_target);
                    gameData.gameStatus = GameData.GameStatus.ShowResult;
                }
            }
        }

        private void onClickMouseShowResult()
        {
            gameData.PlayerTurnEnd();
            var aliveCities = gameData.Battle.GetAliveCityList();
            if (aliveCities.Count <= 1)
            {
                gameData.gameStatus = GameData.GameStatus.GameEnd;
            }
            else
            {
                gameData.gameStatus = GameData.GameStatus.ActionEnemy;
            }
        }
    }
}
