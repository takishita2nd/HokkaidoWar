using asd;
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
        private Layer2D layer = null;
        private TextObject2D _turnText;
        private TextObject2D _playCity;

        public MainScene()
        {
            gameData = Singleton.GameData;
        }

        protected override void OnRegistered()
        {
            layer = new asd.Layer2D();
            AddLayer(layer);

            // 下地
            var background = new asd.GeometryObject2D();
            layer.AddObject(background);
            var bgRect = new asd.RectangleShape();
            bgRect.DrawingArea = new asd.RectF(0, 0, 1900, 1000);
            background.Shape = bgRect;
            var hokkaido = new asd.TextureObject2D();
            hokkaido.Texture = asd.Engine.Graphics.CreateTexture2D("101.png");
            hokkaido.Scale = new asd.Vector2DF(1.5f, 1.5f);
            layer.AddObject(hokkaido);

            // マップの配置
            foreach (var c in gameData.Battle.GetAliveCityList())
            {
                var maps = c.GetMaps();
                foreach (var m in maps)
                {
                    m.AddLayer(layer);
                }
            }

            // リンクの描画
            for (int i = 1; i <= gameData.MapData.citydata.Length; i++)
            {
                var m = Singleton.FieldMap.GetMap(i);
                foreach (var linkedMap in m.GetLinkdMap())
                {
                    if(m.Id < linkedMap.Id)
                    {
                        var geometryObject = new asd.GeometryObject2D();
                        geometryObject.Color = new asd.Color(0, 0, 255);
                        geometryObject.DrawingPriority = 5;
                        var linkLine = new asd.LineShape();
                        linkLine.StartingPosition = new asd.Vector2DF(m.CenterX, m.CenterY);
                        linkLine.EndingPosition = new asd.Vector2DF(linkedMap.CenterX, linkedMap.CenterY);
                        linkLine.Thickness = 2;
                        geometryObject.Shape = linkLine;
                        layer.AddObject(geometryObject);
                    }
                }
            }

            _turnText = new TextObject2D();
            _turnText.Font = Singleton.LargeFont;
            _turnText.DrawingPriority = 20;
            _turnText.Position = new Vector2DF(1000, 50);
            layer.AddObject(_turnText);

            _playCity = new TextObject2D();
            _playCity.Font = Singleton.LargeFont;
            _playCity.DrawingPriority = 20;
            _playCity.Position = new Vector2DF(1000, 150);
            layer.AddObject(_playCity);

            //var info = Singleton.InfomationWindow;
            //info.AddLayer(layer);
            //var info2 = Singleton.GameProcessInfomation;
            //info2.AddLayer(layer);
        }

        protected override void OnUpdated()
        {
            asd.Vector2DF pos = asd.Engine.Mouse.Position;

            var maps = Singleton.FieldMap.GetAllMaps();
            foreach(var map in maps)
            {
                if(map == null)
                {
                    continue;
                }

                if(map.IsOnMouse(pos))
                {
                    map.GetCity().PaintAttackColor();
                    foreach(var linkedMap in map.GetLinkdMap())
                    {
                        linkedMap.GetCity().PaintDeffenceColor();
                    }
                    break;
                }
                else
                {
                    map.GetCity().ClearPaint();
                }
            }

            switch (gameData.gameStatus)
            {
                case GameData.GameStatus.ShowTurn:
                    cycleShowTurn(pos);
                    break;
                case GameData.GameStatus.ActionEnemy:
                    cycleProcessActionEnemy(pos);
                    break;
                case GameData.GameStatus.ActionPlayer:
                    cycleProcessActionPlayer(pos);
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
                    case GameData.GameStatus.ShowTurn:
                        break;
                    case GameData.GameStatus.ActionEnemy:
                        break;
                    case GameData.GameStatus.ActionPlayer:
                        onClickMouseActionPlayer(pos);
                        break;
                    case GameData.GameStatus.GameEnd:
                        break;
                    case GameData.GameStatus.GameOver:
                        break;
                }
            }
        }

        private void onMouse(Vector2DF pos)
        {
            gameData.OnMouseCity(pos);
        }

        private void cycleShowTurn(Vector2DF pos)
        {
            _turnText.Text = "ターン" + gameData.TurnNumber;
            Thread.Sleep(1000);
            gameData.gameStatus = GameData.GameStatus.ActionEnemy;
        }

        private void cycleProcessActionEnemy(Vector2DF pos)
        {
            if (gameData.IsPlayerTrun())
            {
                gameData.gameStatus = GameData.GameStatus.ActionPlayer;
            }
            else
            {
                _playCity.Text = gameData.GetActionCity() + "の行動";
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
        }

        private void cycleProcessGameEnd()
        {
            var gameinfo = Singleton.GameProcessInfomation;
            gameinfo.ShowText(gameData.GetPlayCityPosition(), string.Empty);
            var info = Singleton.InfomationWindow;
            info.ShowText(gameData.AliveCities[0].GetPosition(), "ゲームが終了しました\r\n");
            info.ShowText(gameData.AliveCities[0].GetPosition(), gameData.AliveCities[0].Name + "の勝利です\r\n");
        }

        private void cycleProcessGameOver(asd.Vector2DF pos)
        {
            var gameinfo = Singleton.GameProcessInfomation;
            gameinfo.ShowText(gameData.GetPlayCityPosition(), string.Empty);
            var info = Singleton.InfomationWindow;
            info.ShowText(pos, "敗北しました\r\n");
        }

        private City _target = null;
        private void onClickMouseActionPlayer(asd.Vector2DF pos)
        {
            var info = Singleton.InfomationWindow;
            info.ShowText(pos, String.Empty);
            var linkedCities = gameData.GetPlayerLinkedCities();
            foreach (var city in linkedCities)
            {
                if (city.IsOnMouse(pos))
                {
                    _target = city;
                    gameData.PlayerAttackCity(_target);
                }
            }
        }
    }
}
