using HokkaidoWar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HokkaidoWar.Scene
{
    class TitleScene : asd.Scene
    {
        private asd.Layer2D layer = null;
        private asd.TextureObject2D _newgame = null;
        private asd.TextureObject2D _load = null;

        private asd.Texture2D newgame1Image = asd.Engine.Graphics.CreateTexture2D("newgame1.png");
        private asd.Texture2D newgame2Image = asd.Engine.Graphics.CreateTexture2D("newgame2.png");
        private asd.Texture2D load1Image = asd.Engine.Graphics.CreateTexture2D("load1.png");
        private asd.Texture2D load2Image = asd.Engine.Graphics.CreateTexture2D("load2.png");

        private Dialog dialog = new Dialog();

        private const int buttonWidth = 330;
        private const int buttonHeight = 80;

        public TitleScene()
        {
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

            // 北海道の背景
            var hokkaido = new asd.TextureObject2D();
            hokkaido.Texture = asd.Engine.Graphics.CreateTexture2D("101.png");
            hokkaido.Scale = new asd.Vector2DF(1.5f, 1.5f);
            layer.AddObject(hokkaido);

            // タイトル
            var title = new asd.TextureObject2D();
            title.Texture = asd.Engine.Graphics.CreateTexture2D("title.png");
            title.Position = new asd.Vector2DF(250, 200);
            layer.AddObject(title);

            // 新規ゲームボタン
            _newgame = new asd.TextureObject2D();
            _newgame.Texture = newgame1Image;
            _newgame.Position = new asd.Vector2DF(150, 450);
            layer.AddObject(_newgame);

            // ロードボタン
            _load = new asd.TextureObject2D();
            _load.Texture = load1Image;
            _load.Position = new asd.Vector2DF(500, 450);
            layer.AddObject(_load);
        }

        protected override void OnUpdated()
        {
            asd.Vector2DF pos = asd.Engine.Mouse.Position;

            if(dialog.IsShow)
            {
                dialog.OnMouse(pos);
            }
            else
            {
                if (isOnMouse(pos, _newgame))
                {
                    _newgame.Texture = newgame2Image;
                }
                else
                {
                    _newgame.Texture = newgame1Image;
                }

                if (isOnMouse(pos, _load))
                {
                    _load.Texture = load2Image;
                }
                else
                {
                    _load.Texture = load1Image;
                }
            }

            if (asd.Engine.Mouse.LeftButton.ButtonState == asd.ButtonState.Push)
            {
                if (dialog.IsShow)
                {
                    var result = dialog.OnClick(pos);
                    if(result == Dialog.Result.OK)
                    {
                        var gamedata = Singleton.GameData;
                        gamedata.Cities = new List<City>();
                        var data = FileAccess.LoadData();
                        foreach (var city in data.Citydata)
                        {
                            gamedata.Cities.Add(new City(city));
                        }
                        City player = null;
                        foreach (var city in gamedata.Cities)
                        {
                            if(data.PlayerId == city.Id)
                            {
                                player = city;
                            }
                        }
                        gamedata.TurnNumber = data.Turn;

                        gamedata.Battleinitialize();
                        gamedata.CreatePlayer(player);
                        gamedata.gameStatus = GameData.GameStatus.ShowTurn;
                        asd.Engine.ChangeScene(new MainScene());
                    }
                    if (result == Dialog.Result.Cancel)
                    {
                        dialog.CloseDialog(layer);
                    }
                }
                else
                {
                    if (isOnMouse(pos, _newgame))
                    {
                        var scene = new SelectCityScene();
                        asd.Engine.ChangeSceneWithTransition(scene, new asd.TransitionFade(1.5f, 1.5f));
                    }
                    if (isOnMouse(pos, _load))
                    {
                        var data = FileAccess.LoadData();
                        Citydata player = null;
                        foreach (var city in Singleton.GameData.MapData.citydata)
                        {
                            if (data.PlayerId == city.id)
                            {
                                player = city;
                            }
                        }
                        dialog.ShowDialog(layer,
                             player.name + "\r\n" +
                            "ターン" + data.Turn + "\r\n" +
                            "再開しますか？");
                    }
                }
            }
        }

        private bool isOnMouse(asd.Vector2DF pos, asd.TextureObject2D button)
        {
            if (pos.X > button.Position.X && pos.X < button.Position.X + buttonWidth
                && pos.Y > button.Position.Y && pos.Y < button.Position.Y + buttonHeight)
            {
                return true;
            }
            return false;
        }
    }
}
