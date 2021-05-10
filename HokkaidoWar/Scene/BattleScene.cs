using HokkaidoWar.Model;
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
            SelectDeffenceAction,
            SelectAttackAction,
            ShowActionResult
        }

        public enum Player
        {
            Attack,
            Deffence
        }

        enum Action
        {
            Charge,
            Siege,
            Shoot,
            Deffence
        }

        private asd.TextureObject2D _attackResult = null;
        private asd.TextureObject2D _deffenceResult = null;
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

        private Action selectedAttack;
        private Action selectedDeffece;

        private const int buttonWidth = 330;
        private const int buttonHeight = 80;

        /**
         * コンストラクタ
         */
        public BattleScene(City attack, City deffence, Player player)
        {
            _attack = attack;
            _attackPower = (int)(attack.Power * attack.Bonus);
            _deffence = deffence;
            _deffencePower = (int)(deffence.Power * deffence.Bonus);
            _player = player;
            _status = GameStatus.SelectDeffenceAction; ;
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
            _commandCharge.Position = new asd.Vector2DF(1100, 300);
            layer.AddObject(_commandCharge);

            _commandSiege = new asd.TextureObject2D();
            _commandSiege.Position = new asd.Vector2DF(1100, 400);
            layer.AddObject(_commandSiege);

            _commandShoot = new asd.TextureObject2D();
            _commandShoot.Position = new asd.Vector2DF(1100, 500);
            layer.AddObject(_commandShoot);

            if(_player == Player.Deffence)
            {
                _commandDeffence = new asd.TextureObject2D();
                _commandDeffence.Position = new asd.Vector2DF(1100, 600);
                layer.AddObject(_commandDeffence);
            }

            _attackResult = new asd.TextureObject2D();
            _attackResult.Position = new asd.Vector2DF(450, 500);
            layer.AddObject(_attackResult);

            _deffenceResult = new asd.TextureObject2D();
            _deffenceResult.Position = new asd.Vector2DF(450, 250);
            layer.AddObject(_deffenceResult);
        }

        protected override void OnUpdated()
        {
            asd.Vector2DF pos = asd.Engine.Mouse.Position;

            switch (_status)
            {
                case GameStatus.SelectAttackAction:
                    cycleProcessSelectAttackAction(pos);
                    break;
                case GameStatus.SelectDeffenceAction:
                    cycleProcessSelectDeffenceAction(pos);
                    break;
                case GameStatus.ShowActionResult:
                    cycleProcessShowActionResult(pos);
                    break;
            }
            if (asd.Engine.Mouse.LeftButton.ButtonState == asd.ButtonState.Push)
            {
                switch (_status)
                {
                    case GameStatus.SelectAttackAction:
                        onClickMouseSelectAttackAction(pos);
                        break;
                    case GameStatus.SelectDeffenceAction:
                        onClickMouseSelectDeffenceAction(pos);
                        break;
                    case GameStatus.ShowActionResult:
                        onClickMouseShowActionResult(pos);
                        break;
                }
            }
        }

        private void cycleProcessSelectAttackAction(asd.Vector2DF pos)
        {
            if (_player == Player.Attack)
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
            }
            else
            {
                var r = Singleton.Random;
                switch (r.Next(0, 3))
                {
                    case 0:
                        selectedAttack = Action.Charge;
                        break;
                    case 1:
                        selectedAttack = Action.Siege;
                        break;
                    case 2:
                        selectedAttack = Action.Shoot;
                        break;
                }
                showCommand();
                _status = GameStatus.ShowActionResult;
            }

        }

        private void cycleProcessSelectDeffenceAction(asd.Vector2DF pos)
        {
            if (_player == Player.Deffence)
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
            else
            {
                var r = Singleton.Random;
                switch (r.Next(0, 3))
                {
                    case 0:
                        selectedDeffece = Action.Charge;
                        break;
                    case 1:
                        selectedDeffece = Action.Siege;
                        break;
                    case 2:
                        selectedDeffece = Action.Shoot;
                        break;
                }
                showCommand();
                _status = GameStatus.SelectAttackAction;
            }
        }

        private void cycleProcessShowActionResult(asd.Vector2DF pos)
        {
            switch (selectedAttack)
            {
                case Action.Charge:
                    _attackResult.Texture = Singleton.ImageCharge;
                    break;
                case Action.Siege:
                    _attackResult.Texture = Singleton.ImageSiege;
                    break;
                case Action.Shoot:
                    _attackResult.Texture = Singleton.ImageShoot;
                    break;
                default:
                    _attackResult.Texture = Singleton.ImageDeffence;
                    break;
            }
            switch(selectedDeffece)
            {
                case Action.Charge:
                    _deffenceResult.Texture = Singleton.ImageCharge;
                    break;
                case Action.Siege:
                    _deffenceResult.Texture = Singleton.ImageSiege;
                    break;
                case Action.Shoot:
                    _deffenceResult.Texture = Singleton.ImageShoot;
                    break;
                default:
                    _deffenceResult.Texture = Singleton.ImageDeffence;
                    break;
            }
        }

        private void onClickMouseSelectDeffenceAction(asd.Vector2DF pos)
        {
            if (_player == Player.Deffence)
            {
                if (isOnMouse(pos, _commandCharge))
                {
                    selectedDeffece = Action.Charge;
                    hideCommand();
                    _status = GameStatus.SelectAttackAction;
                }
                if (isOnMouse(pos, _commandSiege))
                {
                    selectedDeffece = Action.Siege;
                    hideCommand();
                    _status = GameStatus.SelectAttackAction;
                }
                if (isOnMouse(pos, _commandShoot))
                {
                    selectedDeffece = Action.Shoot;
                    hideCommand();
                    _status = GameStatus.SelectAttackAction;
                }
                if (isOnMouse(pos, _commandDeffence))
                {
                    selectedDeffece = Action.Deffence;
                    hideCommand();
                    _status = GameStatus.SelectAttackAction;
                }
            }
        }

        private void onClickMouseSelectAttackAction(asd.Vector2DF pos)
        {
            if (_player == Player.Attack)
            {
                if (isOnMouse(pos, _commandCharge))
                {
                    selectedAttack = Action.Charge;
                    hideCommand();
                    _status = GameStatus.ShowActionResult;
                }
                if (isOnMouse(pos, _commandSiege))
                {
                    selectedAttack = Action.Siege;
                    hideCommand();
                    _status = GameStatus.ShowActionResult;
                }
                if (isOnMouse(pos, _commandShoot))
                {
                    selectedAttack = Action.Shoot;
                    hideCommand();
                    _status = GameStatus.ShowActionResult;
                }
            }
        }

        private void onClickMouseShowActionResult(asd.Vector2DF pos)
        {
            var result = judge(selectedAttack, selectedDeffece);
            if (result == BattleResult.win)
            {
                _deffencePower -= (int)Math.Floor(_attackPower * (Singleton.Random.NextDouble() + 0.1));
                if (_deffencePower <= 0)
                {
                    _attack.UpdatePower(_attackPower);
                    Singleton.GameData.BattleResultUpdate(BattleResult.win);
                    asd.Engine.ChangeScene(new MainScene());
                    _deffenceParam.Text = "戦闘力：0";
                }
                else
                {
                    _deffenceParam.Text = "戦闘力：" + _deffencePower;
                }
            }
            else if (result == BattleResult.lose)
            {

                _attackPower -= (int)Math.Floor(_deffencePower * (Singleton.Random.NextDouble() + 0.1));
                if (_attackPower <= 0)
                {
                    _deffence.UpdatePower(_deffencePower);
                    Singleton.GameData.BattleResultUpdate(BattleResult.lose);
                    asd.Engine.ChangeScene(new MainScene());
                    _attackParam.Text = "戦闘力：0";
                }
                else
                {
                    _attackParam.Text = "戦闘力：" + _attackPower;
                }
            }
            else if (result == BattleResult.guard)
            {
                var attackDamage = _deffencePower / 10;
                var deffenceDamage = _attackPower / 10;
                _deffencePower -= deffenceDamage;
                _attackPower -= attackDamage;
                if (_deffencePower <= 0)
                {
                    _attack.UpdatePower(_attackPower);
                    Singleton.GameData.BattleResultUpdate(BattleResult.win);
                    asd.Engine.ChangeScene(new MainScene());
                    _deffenceParam.Text = "戦闘力：0";
                    _attackParam.Text = "戦闘力：" + _attackPower;
                }
                else if (_attackPower <= 0)
                {
                    _deffence.UpdatePower(_deffencePower);
                    Singleton.GameData.BattleResultUpdate(BattleResult.lose);
                    asd.Engine.ChangeScene(new MainScene());
                    _deffenceParam.Text = "戦闘力：" + _deffencePower;
                    _attackParam.Text = "戦闘力：0";
                }
                else
                {
                    _attackParam.Text = "戦闘力：" + _attackPower;
                    _deffenceParam.Text = "戦闘力：" + _deffencePower;
                }
            }
            _attackResult.Texture = null;
            _deffenceResult.Texture = null;
            _status = GameStatus.SelectDeffenceAction;
        }

        private BattleResult judge(Action attack, Action deffence)
        {
            switch (attack)
            {
                case Action.Charge:
                    switch (deffence)
                    {
                        case Action.Charge:
                            return BattleResult.draw;
                        case Action.Siege:
                            return BattleResult.win;
                        case Action.Shoot:
                            return BattleResult.lose;
                        default:
                            return BattleResult.guard;
                    }
                case Action.Siege:
                    switch (deffence)
                    {
                        case Action.Charge:
                            return BattleResult.lose;
                        case Action.Siege:
                            return BattleResult.draw;
                        case Action.Shoot:
                            return BattleResult.win;
                        default:
                            return BattleResult.guard;
                    }
                case Action.Shoot:
                    switch (deffence)
                    {
                        case Action.Charge:
                            return BattleResult.win;
                        case Action.Siege:
                            return BattleResult.lose;
                        case Action.Shoot:
                            return BattleResult.draw;
                        default:
                            return BattleResult.guard;
                    }
                default:
                    return BattleResult.guard;
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

        private void showCommand()
        {
            _commandCharge.Texture = Singleton.ImageCharge;
            _commandSiege.Texture = Singleton.ImageSiege;
            _commandShoot.Texture = Singleton.ImageShoot;
            if (_player == Player.Deffence)
            {
                _commandDeffence.Texture = Singleton.ImageDeffence;
            }
        }
        private void hideCommand()
        {
            _commandCharge.Texture = null;
            _commandSiege.Texture = null;
            _commandShoot.Texture = null;
            if (_player == Player.Deffence)
            {
                _commandDeffence.Texture = null;
            }
        }
    }
}
