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
        private TextObject2D _actionText;
        private TextureObject2D _attackButton;
        private TextureObject2D _powerupButton;
        private TextureObject2D _cancelButton;
        private NumberDialog _numberDialog = new NumberDialog();
        private List<Map> linkedMaps;

        private const int buttonWidth = 330;
        private const int buttonHeight = 80;

        private Dialog _dialog = new Dialog();

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
            _playCity.Position = new Vector2DF(1000, 100);
            layer.AddObject(_playCity);

            _actionText = new TextObject2D();
            _actionText.Font = Singleton.LargeFont;
            _actionText.DrawingPriority = 20;
            _actionText.Position = new Vector2DF(1000, 150);
            layer.AddObject(_actionText);


            _attackButton = new TextureObject2D();
            _attackButton.Position = new Vector2DF(1000, 300);
            layer.AddObject(_attackButton);

            _powerupButton = new TextureObject2D();
            _powerupButton.Position = new Vector2DF(1000, 400);
            layer.AddObject(_powerupButton);

            _cancelButton = new TextureObject2D();
            _cancelButton.Position = new Vector2DF(1000, 500);
            layer.AddObject(_cancelButton);
        }

        protected override void OnUpdated()
        {
            _actionText.Text = string.Empty;

            asd.Vector2DF pos = asd.Engine.Mouse.Position;

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
                case GameData.GameStatus.SelectTargetCity:
                    cycleSelectTargetCity(pos);
                    break;
                case GameData.GameStatus.InputPowerUpPoint:
                    cycleInputPowerUpPoint(pos);
                    break;
                case GameData.GameStatus.VerificateInputPowerUp:
                    cycleVerificateInputPowerUp(pos);
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
                    case GameData.GameStatus.SelectTargetCity:
                        onClickSelectTargetCity(pos);
                        break;
                    case GameData.GameStatus.InputPowerUpPoint:
                        onClickInputPowerUpPoint(pos);
                        break;
                    case GameData.GameStatus.VerificateInputPowerUp:
                        onClickVerificateInputPowerUp(pos);
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
            gameData.GetMoney();
            Thread.Sleep(1000);
            gameData.gameStatus = GameData.GameStatus.ActionEnemy;
        }

        private void cycleProcessActionEnemy(Vector2DF pos)
        {
            _turnText.Text = "ターン" + gameData.TurnNumber;
            _playCity.Text = gameData.GetActionCity() + "の行動";

            if (gameData.IsPlayerTrun())
            {
                gameData.PlayPlayer();
                var info = Singleton.InfomationWindow;
                info.Show(layer);
                gameData.gameStatus = GameData.GameStatus.ActionPlayer;
            }
            else
            {
                Thread.Sleep(200);
                _actionText.Text = gameData.PlayNextCity();
                if (gameData.IsPlayerAlive() == false)
                {
                    gameData.gameStatus = GameData.GameStatus.GameOver;
                }
            }
        }

        private void cycleProcessActionPlayer(Vector2DF pos)
        {
            _turnText.Text = "ターン" + gameData.TurnNumber;
            var info = Singleton.InfomationWindow;
            if(pos.X <= 1000)
            {
                if (!info.IsShow())
                {
                    info.Show(layer);
                }
                info.ShowText(pos, string.Empty);
                onMouse(pos);
            }
            else
            {
                if (info.IsShow())
                {
                    info.Hide(layer);
                }
            }

            if(isOnMouse(pos, _attackButton))
            {
                _attackButton.Texture = Singleton.ImageAttack2;
            }
            else
            {
                _attackButton.Texture = Singleton.ImageAttack;
            }
            if (isOnMouse(pos, _powerupButton))
            {
                _powerupButton.Texture = Singleton.ImagePowerup2;
            }
            else
            {
                _powerupButton.Texture = Singleton.ImagePowerup;
            }
        }

        private void cycleSelectTargetCity(Vector2DF pos)
        {
            var info = Singleton.InfomationWindow;
            if (pos.X <= 1000)
            {
                if (!info.IsShow())
                {
                    info.Show(layer);
                }
                info.ShowText(pos, "都市を選択してください\r\n");
                onMouse(pos);
            }
            else
            {
                if (info.IsShow())
                {
                    info.Hide(layer);
                }
            }

            if (isOnMouse(pos, _cancelButton))
            {
                _cancelButton.Texture = Singleton.ImageCancel2;
            }
            else
            {
                _cancelButton.Texture = Singleton.ImageCancel;
            }

        }

        private void cycleInputPowerUpPoint(Vector2DF pos)
        {
            _numberDialog.OnMouse(pos);
        }

        private void cycleVerificateInputPowerUp(Vector2DF pos)
        {
            _dialog.OnMouse(pos);
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

        private void onClickMouseActionPlayer(asd.Vector2DF pos)
        {
            if(isOnMouse(pos, _attackButton))
            {
                linkedMaps = gameData.GetPlayerLinkedMaps();
                foreach (var map in linkedMaps)
                {
                    map.PaintAttackColor();
                }
                _attackButton.Texture = null;
                _powerupButton.Texture = null;
                _cancelButton.Texture = Singleton.ImageCancel;
                gameData.gameStatus = GameData.GameStatus.SelectTargetCity;
            }
            if(isOnMouse(pos, _powerupButton))
            {
                _numberDialog.ShowDialog(layer, gameData.Player.City.Money);
                gameData.gameStatus = GameData.GameStatus.InputPowerUpPoint;
            }
        }

        private void onClickSelectTargetCity(Vector2DF pos)
        {
            if (isOnMouse(pos, _cancelButton))
            {
                foreach (var map in linkedMaps)
                {
                    map.SetColor(map.GetCity().GetColor());
                }
                _attackButton.Texture = Singleton.ImageAttack;
                _powerupButton.Texture = Singleton.ImagePowerup;
                _cancelButton.Texture = null;
                gameData.gameStatus = GameData.GameStatus.ActionPlayer;
            }
            else
            {
                foreach(var map in linkedMaps)
                {
                    if(map.IsOnMouse(pos))
                    {
                        foreach (var m in linkedMaps)
                        {
                            m.SetColor(m.GetCity().GetColor());
                        }

                        var info = Singleton.InfomationWindow;
                        info.Hide(layer);
                        _cancelButton.Texture = null;
                        gameData.PlayerAttackCity(map.GetCity());
                    }
                }
            }
        }

        private void onClickInputPowerUpPoint(Vector2DF pos)
        {
            var result = _numberDialog.OnClick(pos);
            switch(result)
            {
                case NumberDialog.Result.OK:
                    _dialog.ShowDialog(layer, "戦力" + _numberDialog.GetValue() + "を購入します。\r\nよろしいですか？");
                    gameData.gameStatus = GameData.GameStatus.VerificateInputPowerUp;
                    break;
                case NumberDialog.Result.Cancel:
                    _numberDialog.CloseDialog(layer);
                    gameData.gameStatus = GameData.GameStatus.ActionPlayer;
                    break;
                default:
                    break;
            }
        }

        private void onClickVerificateInputPowerUp(Vector2DF pos)
        {
            var result = _dialog.OnClick(pos);
            switch(result)
            {
                case Dialog.Result.OK:
                    var power = _numberDialog.GetValue();
                    gameData.Player.City.PayMoney(power);
                    gameData.Player.City.AddPower(power);
                    _numberDialog.CloseDialog(layer);
                    _dialog.CloseDialog(layer);
                    gameData.PlayerTurnEnd();
                    gameData.gameStatus = GameData.GameStatus.ActionEnemy;
                    break;
                case Dialog.Result.Cancel:
                    _dialog.CloseDialog(layer);
                    gameData.gameStatus = GameData.GameStatus.InputPowerUpPoint;
                    break;
                default:
                    break;
            }
        }

        private bool isOnMouse(Vector2DF pos, TextureObject2D button)
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
