using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HokkaidoWar.Model
{
    class Dialog
    {
        private asd.GeometryObject2D _dialog = null;
        private asd.TextureObject2D _okButton = null;
        private asd.TextureObject2D _cancelButton = null;
        private asd.TextObject2D _valueText = null;

        public Dialog()
        {
            _dialog = new asd.GeometryObject2D();
            _dialog.Color = new asd.Color(255, 255, 255);

            var dialogRect = new asd.RectangleShape();
            dialogRect.DrawingArea = new asd.RectF(300, 400, 600, 200);
            _dialog.Shape = dialogRect;

            _valueText = new asd.TextObject2D();
            _valueText.Position = new asd.Vector2DF(310, 410);
            _valueText.Font = Singleton.Font;

            _okButton = new asd.TextureObject2D();
            _okButton.Texture = Singleton.ImageOK;
            _okButton.Position = new asd.Vector2DF(310, 500);

            _cancelButton = new asd.TextureObject2D();
            _cancelButton.Texture = Singleton.ImageCancel;
            _cancelButton.Position = new asd.Vector2DF(620, 500);
        }

        public void ShowDialog(asd.Layer2D layer, string cityName)
        {
            layer.AddObject(_dialog);
            _valueText.Text = cityName + "でよろしいですか？";
            layer.AddObject(_valueText);
            layer.AddObject(_okButton);
            layer.AddObject(_cancelButton);
        }

        public void CloseDialog(asd.Layer2D layer)
        {
            layer.RemoveObject(_dialog);
            layer.RemoveObject(_valueText);
            layer.RemoveObject(_okButton);
            layer.RemoveObject(_cancelButton);
        }
    }
}
