using HokkaidoWar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HokkaidoWar.Scene
{
    class SelectCityScene : asd.Scene
    {
        private GameData gameData = null;
        private asd.Layer2D layer = null;
        private Dialog dialog = new Dialog();
        private City selectcity = null;

        public SelectCityScene()
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
                    if (m.Id < linkedMap.Id)
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

            var info = Singleton.InfomationWindow;
            info.Show(layer);
        }

        protected override void OnUpdated()
        {
            asd.Vector2DF pos = asd.Engine.Mouse.Position;

            switch (gameData.gameStatus)
            {
                case GameData.GameStatus.SelectCity:
                    cycleProcessSelectCity(pos);
                    break;
                case GameData.GameStatus.VerificateCity:
                    cycleProcessVerificateCity(pos);
                    break;
            }

            if (asd.Engine.Mouse.LeftButton.ButtonState == asd.ButtonState.Push)
            {
                switch (gameData.gameStatus)
                {
                    case GameData.GameStatus.SelectCity:
                        onClickMouseSelectCity(pos);
                        break;
                    case GameData.GameStatus.VerificateCity:
                        onClickVerificateCity(pos);
                        break;

                }
            }
        }

        private void cycleProcessSelectCity(asd.Vector2DF pos)
        {
            // 情報ウィンドウの表示を更新
            var info = Singleton.InfomationWindow;
            info.ShowText(pos, "都市を選択してください\r\n");
            onMouse(pos);
        }

        private void cycleProcessVerificateCity(asd.Vector2DF pos)
        {
            //ダイアログ上にマウスカーソルがあれば処理する
            dialog.OnMouse(pos);
        }

        private void onClickMouseSelectCity(asd.Vector2DF pos)
        {
            selectcity = gameData.GetSelectedCity(pos);
            if (selectcity != null)
            {
                var info = Singleton.InfomationWindow;
                info.Hide(layer);
                dialog.ShowDialog(layer, selectcity.Name + "でよろしいですか？");
                gameData.gameStatus = GameData.GameStatus.VerificateCity;
            }
        }

        private void onClickVerificateCity(asd.Vector2DF pos)
        {
            var info = Singleton.InfomationWindow;
            switch (dialog.OnClick(pos))
            {
                case Dialog.Result.OK:
                    info.ShowText(pos, string.Empty);
                    dialog.CloseDialog(layer);
                    if (selectcity != null)
                    {
                        gameData.CreatePlayer(selectcity);
                        gameData.gameStatus = GameData.GameStatus.ShowTurn;
                        asd.Engine.ChangeScene(new MainScene());
                    }
                    break;
                case Dialog.Result.Cancel:
                    dialog.CloseDialog(layer);
                    info.Show(layer);
                    gameData.gameStatus = GameData.GameStatus.SelectCity;
                    break;
                case Dialog.Result.None:
                    break;
            }
        }

        private void onMouse(asd.Vector2DF pos)
        {
            gameData.OnMouseCity(pos);
        }
    }
}
