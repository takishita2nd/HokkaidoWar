﻿using HokkaidoWar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HokkaidoWar.GameData;

namespace HokkaidoWar.Scene
{
    class BattleScene : asd.Scene
    {
        enum GameStatus {
            SelectCommand,
            ShowActionResult
        }

        public enum Player
        {
            Attack,
            Deffence
        }

        private BattleIcon _image_gu_attack = null;
        private BattleIcon _image_choki_attack = null;
        private BattleIcon _image_par_attack = null;
        private BattleIcon _image_gu_deffence = null;
        private BattleIcon _image_choki_deffence = null;
        private BattleIcon _image_par_deffence = null;
        private BattleIcon _attackResult = null;
        private BattleIcon _deffenceResult = null;
        private asd.TextObject2D _attackParam = null;
        private asd.TextObject2D _deffenceParam = null;
        private asd.TextureObject2D _commandCharge = null;
        private asd.TextureObject2D _commandSiege = null;
        private asd.TextureObject2D _commandShoot = null;
        private asd.TextureObject2D _commandDeffence = null;

        private City _attack = null;
        private City _deffence = null;
        private int _attackPower = 0;
        private int _deffencePower = 0;
        private Player _player;
        private GameStatus _status;

        private BattleIcon.Icon selectedAttack;
        private BattleIcon.Icon selectedDeffece;

        private const int buttonWidth = 330;
        private const int buttonHeight = 80;

        /**
         * コンストラクタ
         */
        public BattleScene(City attack, City deffence, Player player)
        {
            _attack = attack;
            _attackPower = attack.Population;
            _deffence = deffence;
            _deffencePower = deffence.Population;
            _player = player;
            _status = GameStatus.SelectCommand;
        }

        protected override void OnRegistered()
        {
            var layer = new asd.Layer2D();
            AddLayer(layer);

            // 下地
            var background = new asd.GeometryObject2D();
            layer.AddObject(background);
            var bgRect = new asd.RectangleShape();
            bgRect.DrawingArea = new asd.RectF(0, 0, 1900, 1000);
            background.Shape = bgRect;

            var label = new asd.TextObject2D();
            label.Font = Singleton.LargeFont;
            label.Text = "VS";
            label.Position = new asd.Vector2DF(470, 400);
            layer.AddObject(label);

            var attackCityLabel = new asd.TextObject2D();
            attackCityLabel.Font = Singleton.LargeFont;
            attackCityLabel.Text = _attack.Name;
            attackCityLabel.Position = new asd.Vector2DF(450, 650);
            layer.AddObject(attackCityLabel);

            var deffenceCityLabel = new asd.TextObject2D();
            deffenceCityLabel.Font = Singleton.LargeFont;
            deffenceCityLabel.Text = _deffence.Name;
            deffenceCityLabel.Position = new asd.Vector2DF(450, 150);
            layer.AddObject(deffenceCityLabel);

            _attackParam = new asd.TextObject2D();
            _attackParam.Font = Singleton.LargeFont;
            _attackParam.Text = "戦闘力：" + _attackPower;
            _attackParam.Position = new asd.Vector2DF(700, 650);
            layer.AddObject(_attackParam);

            _deffenceParam = new asd.TextObject2D();
            _deffenceParam.Font = Singleton.LargeFont;
            _deffenceParam.Text = "戦闘力：" + _deffencePower;
            _deffenceParam.Position = new asd.Vector2DF(700, 150);
            layer.AddObject(_deffenceParam);

            var decision = new asd.TextureObject2D();
            decision.Texture = asd.Engine.Graphics.CreateTexture2D("decision.png");
            decision.Position = new asd.Vector2DF(50, 50);
            decision.Scale = new asd.Vector2DF(0.5f, 0.5f);
            layer.AddObject(decision);

            _commandCharge = new asd.TextureObject2D();
            _commandCharge.Texture = Singleton.ImageCharge;
            _commandCharge.Position = new asd.Vector2DF(1100, 300);
            layer.AddObject(_commandCharge);

            _commandSiege = new asd.TextureObject2D();
            _commandSiege.Texture = Singleton.ImageSiege;
            _commandSiege.Position = new asd.Vector2DF(1100, 400);
            layer.AddObject(_commandSiege);

            _commandShoot = new asd.TextureObject2D();
            _commandShoot.Texture = Singleton.ImageShoot;
            _commandShoot.Position = new asd.Vector2DF(1100, 500);
            layer.AddObject(_commandShoot);

            _commandDeffence = new asd.TextureObject2D();
            _commandDeffence.Texture = Singleton.ImageDeffence;
            _commandDeffence.Position = new asd.Vector2DF(1100, 600);
            layer.AddObject(_commandDeffence);

            _image_gu_attack = new BattleIcon(BattleIcon.Icon.Gu, BattleIcon.Position.Attack);
            _image_gu_attack.AddLayer(layer);
            _image_gu_attack.Hide();
            _image_choki_attack = new BattleIcon(BattleIcon.Icon.Choki, BattleIcon.Position.Attack);
            _image_choki_attack.AddLayer(layer);
            _image_choki_attack.Hide();
            _image_par_attack = new BattleIcon(BattleIcon.Icon.Par, BattleIcon.Position.Attack);
            _image_par_attack.AddLayer(layer);
            _image_par_attack.Hide();
            _image_gu_deffence = new BattleIcon(BattleIcon.Icon.Gu, BattleIcon.Position.Deffence);
            _image_gu_deffence.AddLayer(layer);
            _image_gu_deffence.Hide();
            _image_choki_deffence = new BattleIcon(BattleIcon.Icon.Choki, BattleIcon.Position.Deffence);
            _image_choki_deffence.AddLayer(layer);
            _image_choki_deffence.Hide();
            _image_par_deffence = new BattleIcon(BattleIcon.Icon.Par, BattleIcon.Position.Deffence);
            _image_par_deffence.AddLayer(layer);
            _image_par_deffence.Hide();
            _attackResult = new BattleIcon(BattleIcon.Icon.Choki, BattleIcon.Position.Attack);
            _attackResult.AddLayer(layer);
            _attackResult.Hide();
            _deffenceResult = new BattleIcon(BattleIcon.Icon.Choki, BattleIcon.Position.Deffence);
            _deffenceResult.AddLayer(layer);
            _deffenceResult.Hide();
        }

        protected override void OnUpdated()
        {
            asd.Vector2DF pos = asd.Engine.Mouse.Position;

            switch (_status)
            {
                case GameStatus.SelectCommand:
                    cycleProcessSelectCommand(pos);
                    break;
                case GameStatus.ShowActionResult:
                    cycleProcessShowActionResult(pos);
                    break;
            }
            if (asd.Engine.Mouse.LeftButton.ButtonState == asd.ButtonState.Push)
            {
                switch (_status)
                {
                    case GameStatus.SelectCommand:
                        onClickMouseSelectDeffenceAction(pos);
                        break;
                    case GameStatus.ShowActionResult:
                        onClickMouseShowActionResult(pos);
                        break;
                }
            }
        }

        private void cycleProcessSelectCommand(asd.Vector2DF pos)
        {
            if (isOnMouse(pos, _commandCharge))
            {
                _commandCharge.Texture = Singleton.ImageCharge2;
            }
            else
            {
                _commandCharge.Texture = Singleton.ImageCharge;
            }
            if (isOnMouse(pos, _commandSiege))
            {
                _commandSiege.Texture = Singleton.ImageSiege2;
            }
            else
            {
                _commandSiege.Texture = Singleton.ImageSiege;
            }
            if (isOnMouse(pos, _commandShoot))
            {
                _commandShoot.Texture = Singleton.ImageShoot2;
            }
            else
            {
                _commandShoot.Texture = Singleton.ImageShoot;
            }
            if (isOnMouse(pos, _commandDeffence))
            {
                _commandDeffence.Texture = Singleton.ImageDeffence2;
            }
            else
            {
                _commandDeffence.Texture = Singleton.ImageDeffence;
            }
        }

        private void cycleProcessSelectAttackAction(asd.Vector2DF pos)
        {
            if (_player == Player.Attack)
            {
                _image_gu_attack.Show();
                _image_choki_attack.Show();
                _image_par_attack.Show();
                _image_gu_attack.OnMouse(pos);
                _image_choki_attack.OnMouse(pos);
                _image_par_attack.OnMouse(pos);
            }
            else
            {
                var r = Singleton.Random;
                switch (r.Next(0, 3))
                {
                    case 0:
                        selectedAttack = BattleIcon.Icon.Gu;
                        break;
                    case 1:
                        selectedAttack = BattleIcon.Icon.Choki;
                        break;
                    case 2:
                        selectedAttack = BattleIcon.Icon.Par;
                        break;
                }
                _status = GameStatus.ShowActionResult;
            }
        }

        private void cycleProcessShowActionResult(asd.Vector2DF pos)
        {
            _attackResult.SetIcon(selectedAttack);
            _attackResult.Show();
            _deffenceResult.SetIcon(selectedDeffece);
            _deffenceResult.Show();
        }

        private void onClickMouseSelectDeffenceAction(asd.Vector2DF pos)
        {
            if (_player == Player.Deffence)
            {
                if (_image_gu_deffence.IsOnMouse(pos))
                {
                    selectedDeffece = BattleIcon.Icon.Gu;
                    _image_gu_deffence.Hide();
                    _image_choki_deffence.Hide();
                    _image_par_deffence.Hide();
                    //_status = GameStatus.SelectAttackAction;
                }
                else if (_image_choki_deffence.IsOnMouse(pos))
                {
                    selectedDeffece = BattleIcon.Icon.Choki;
                    _image_gu_deffence.Hide();
                    _image_choki_deffence.Hide();
                    _image_par_deffence.Hide();
                    //_status = GameStatus.SelectAttackAction;
                }
                else if (_image_par_deffence.IsOnMouse(pos))
                {
                    selectedDeffece = BattleIcon.Icon.Par;
                    _image_gu_deffence.Hide();
                    _image_choki_deffence.Hide();
                    _image_par_deffence.Hide();
                    //_status = GameStatus.SelectAttackAction;
                }
            }
        }

        private void onClickMouseSelectAttackAction(asd.Vector2DF pos)
        {
            if (_player == Player.Attack)
            {
                if (_image_gu_attack.IsOnMouse(pos))
                {
                    selectedAttack = BattleIcon.Icon.Gu;
                }
                else if (_image_choki_attack.IsOnMouse(pos))
                {
                    selectedAttack = BattleIcon.Icon.Choki;
                }
                else if (_image_par_attack.IsOnMouse(pos))
                {
                    selectedAttack = BattleIcon.Icon.Par;
                }
                else
                {
                    return;
                }
                _image_gu_attack.Hide();
                _image_choki_attack.Hide();
                _image_par_attack.Hide();
                _status = GameStatus.ShowActionResult;
            }
        }

        private void onClickMouseShowActionResult(asd.Vector2DF pos)
        {
            var result = janken(selectedAttack, selectedDeffece);
            if(result == BattleResult.win)
            {
                _deffencePower -= (int)Math.Floor(_attackPower * (Singleton.Random.NextDouble() + 0.1));
                if(_deffencePower <= 0)
                {
                    Singleton.GameData.BattleResultUpdate(BattleResult.win);
                    var scene = new MainScene();
                    asd.Engine.ChangeScene(scene);
                    _deffenceParam.Text = "戦闘力：0";
                }
                else
                {
                    _deffenceParam.Text = "戦闘力：" + _deffencePower;
                }
            }
            else if(result == BattleResult.lose)
            {
                
                _attackPower -= (int)Math.Floor(_deffencePower * (Singleton.Random.NextDouble() + 0.1));
                if (_attackPower <= 0)
                {
                    Singleton.GameData.BattleResultUpdate(BattleResult.lose);
                    var scene = new MainScene();
                    asd.Engine.ChangeScene(scene);
                    _attackParam.Text = "戦闘力：0";
                }
                else
                {
                    _attackParam.Text = "戦闘力：" + _attackPower;
                }
            }
            _attackResult.Hide();
            _deffenceResult.Hide();
            //_status = GameStatus.SelectDeffenceAction;
        }

        private BattleResult janken(BattleIcon.Icon attack, BattleIcon.Icon deffence)
        {
            switch (attack)
            {
                case BattleIcon.Icon.Gu:
                    switch (deffence)
                    {
                        case BattleIcon.Icon.Gu:
                            return BattleResult.draw;
                        case BattleIcon.Icon.Choki:
                            return BattleResult.win;
                        case BattleIcon.Icon.Par:
                            return BattleResult.lose;
                        default:
                            return BattleResult.draw;
                    }
                case BattleIcon.Icon.Choki:
                    switch (deffence)
                    {
                        case BattleIcon.Icon.Gu:
                            return BattleResult.lose;
                        case BattleIcon.Icon.Choki:
                            return BattleResult.draw;
                        case BattleIcon.Icon.Par:
                            return BattleResult.win;
                        default:
                            return BattleResult.draw;
                    }
                case BattleIcon.Icon.Par:
                    switch (deffence)
                    {
                        case BattleIcon.Icon.Gu:
                            return BattleResult.win;
                        case BattleIcon.Icon.Choki:
                            return BattleResult.lose;
                        case BattleIcon.Icon.Par:
                            return BattleResult.draw;
                        default:
                            return BattleResult.draw;
                    }
                default:
                    return BattleResult.draw;
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
