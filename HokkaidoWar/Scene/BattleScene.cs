using HokkaidoWar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private BattleIcon _image_gu_attack = null;
        private BattleIcon _image_choki_attack = null;
        private BattleIcon _image_par_attack = null;
        private BattleIcon _image_gu_deffence = null;
        private BattleIcon _image_choki_deffence = null;
        private BattleIcon _image_par_deffence = null;
        private asd.TextObject2D _attackParam = null;
        private asd.TextObject2D _deffenceParam = null;

        private City _attack = null;
        private City _deffence = null;
        private int _attackPower = 0;
        private int _deffencePower = 0;
        private Player _player;
        private GameStatus _status;

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
            _status = GameStatus.SelectDeffenceAction;
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

            var label = new asd.TextObject2D();
            label.Font = Singleton.LargeFont;
            label.Text = "VS";
            label.Position = new asd.Vector2DF(470, 400);
            layer.AddObject(label);

            var attackCityLabel = new asd.TextObject2D();
            attackCityLabel.Font = Singleton.LargeFont;
            attackCityLabel.Text = _attack.Name;
            attackCityLabel.Position = new asd.Vector2DF(450, 150);
            layer.AddObject(attackCityLabel);

            var deffenceCityLabel = new asd.TextObject2D();
            deffenceCityLabel.Font = Singleton.LargeFont;
            deffenceCityLabel.Text = _deffence.Name;
            deffenceCityLabel.Position = new asd.Vector2DF(450, 650);
            layer.AddObject(deffenceCityLabel);

            _attackParam = new asd.TextObject2D();
            _attackParam.Font = Singleton.LargeFont;
            _attackParam.Text = "戦闘力：" + _attack.Population;
            _attackParam.Position = new asd.Vector2DF(700, 650);
            layer.AddObject(_attackParam);

            _deffenceParam = new asd.TextObject2D();
            _deffenceParam.Font = Singleton.LargeFont;
            _deffenceParam.Text = "戦闘力：" + _deffence.Population;
            _deffenceParam.Position = new asd.Vector2DF(700, 150);
            layer.AddObject(_deffenceParam);

            _image_gu_attack = new BattleIcon(BattleIcon.Icon.Gu, BattleIcon.Position.Attack);
            _image_gu_attack.AddLayer(layer);
            _image_choki_attack = new BattleIcon(BattleIcon.Icon.Choki, BattleIcon.Position.Attack);
            _image_choki_attack.AddLayer(layer);
            _image_par_attack = new BattleIcon(BattleIcon.Icon.Par, BattleIcon.Position.Attack);
            _image_par_attack.AddLayer(layer);
            _image_gu_deffence = new BattleIcon(BattleIcon.Icon.Gu, BattleIcon.Position.Deffence);
            _image_gu_deffence.AddLayer(layer);
            _image_choki_deffence = new BattleIcon(BattleIcon.Icon.Choki, BattleIcon.Position.Deffence);
            _image_choki_deffence.AddLayer(layer);
            _image_par_deffence = new BattleIcon(BattleIcon.Icon.Par, BattleIcon.Position.Deffence);
            _image_par_deffence.AddLayer(layer);
        }

        protected override void OnUpdated()
        {
            asd.Vector2DF pos = asd.Engine.Mouse.Position;

            switch (_status)
            {
                case GameStatus.SelectDeffenceAction:
                    cycleProcessSelectDeffenceAction(pos);
                    break;
                case GameStatus.SelectAttackAction:
                    break;
                case GameStatus.ShowActionResult:
                    break;
            }
            if (asd.Engine.Mouse.LeftButton.ButtonState == asd.ButtonState.Push)
            {
                switch (_status)
                {
                    case GameStatus.SelectDeffenceAction:
                        break;
                    case GameStatus.SelectAttackAction:
                        break;
                    case GameStatus.ShowActionResult:
                        break;
                }
            }
        }

        private void cycleProcessSelectDeffenceAction(asd.Vector2DF pos)
        {
            _image_gu_deffence.OnMouse(pos);
            _image_choki_deffence.OnMouse(pos);
            _image_par_deffence.OnMouse(pos);
        }
    }
}
