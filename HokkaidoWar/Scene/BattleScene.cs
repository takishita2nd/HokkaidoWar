using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HokkaidoWar.Scene
{
    class BattleScene : asd.Scene
    {
        private asd.TextObject2D _label = null;
        private asd.TextObject2D _attackCity = null;
        private asd.TextObject2D _deffenceCity = null;
        private asd.TextureObject2D _image_gu_attack = null;
        private asd.TextureObject2D _image_choki_attack = null;
        private asd.TextureObject2D _image_par_attack = null;
        private asd.TextureObject2D _image_gu_deffence = null;
        private asd.TextureObject2D _image_choki_deffence = null;
        private asd.TextureObject2D _image_par_deffence = null;
        private asd.TextObject2D _attackParam = null;
        private asd.TextObject2D _deffenceParam = null;

        public BattleScene()
        {

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

            _label = new asd.TextObject2D();
            _label.Font = Singleton.GetLargeFont();
            _label.Text = "VS";
            _label.Position = new asd.Vector2DF(470, 400);
            layer.AddObject(_label);

            _attackCity = new asd.TextObject2D();
            _attackCity.Font = Singleton.GetLargeFont();
            _attackCity.Text = "札幌";
            _attackCity.Position = new asd.Vector2DF(450, 150);
            layer.AddObject(_attackCity);

            _deffenceCity = new asd.TextObject2D();
            _deffenceCity.Font = Singleton.GetLargeFont();
            _deffenceCity.Text = "小樽";
            _deffenceCity.Position = new asd.Vector2DF(450, 650);
            layer.AddObject(_deffenceCity);

            _attackParam = new asd.TextObject2D();
            _attackParam.Font = Singleton.GetLargeFont();
            _attackParam.Text = "戦闘力：10000";
            _attackParam.Position = new asd.Vector2DF(700, 650);
            layer.AddObject(_attackParam);

            _deffenceParam = new asd.TextObject2D();
            _deffenceParam.Font = Singleton.GetLargeFont();
            _deffenceParam.Text = "戦闘力：8000";
            _deffenceParam.Position = new asd.Vector2DF(700, 150);
            layer.AddObject(_deffenceParam);

            _image_gu_attack = new asd.TextureObject2D();
            _image_gu_attack.Texture = Singleton.GetImageGu();
            _image_gu_attack.Position = new asd.Vector2DF(300, 500);
            layer.AddObject(_image_gu_attack);

            _image_choki_attack = new asd.TextureObject2D();
            _image_choki_attack.Texture = Singleton.GetImageChoki();
            _image_choki_attack.Position = new asd.Vector2DF(450, 500);
            layer.AddObject(_image_choki_attack);

            _image_par_attack = new asd.TextureObject2D();
            _image_par_attack.Texture = Singleton.GetImagePar();
            _image_par_attack.Position = new asd.Vector2DF(600, 500);
            layer.AddObject(_image_par_attack);

            _image_gu_deffence = new asd.TextureObject2D();
            _image_gu_deffence.Texture = Singleton.GetImageGu();
            _image_gu_deffence.Position = new asd.Vector2DF(300, 250);
            layer.AddObject(_image_gu_deffence);

            _image_choki_deffence = new asd.TextureObject2D();
            _image_choki_deffence.Texture = Singleton.GetImageChoki();
            _image_choki_deffence.Position = new asd.Vector2DF(450, 250);
            layer.AddObject(_image_choki_deffence);

            _image_par_deffence = new asd.TextureObject2D();
            _image_par_deffence.Texture = Singleton.GetImagePar();
            _image_par_deffence.Position = new asd.Vector2DF(600, 250);
            layer.AddObject(_image_par_deffence);
        }

        protected override void OnUpdated()
        {

        }
    }
}
