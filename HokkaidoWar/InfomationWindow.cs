using asd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HokkaidoWar
{
    class InfomationWindow
    {
        private asd.TextObject2D _valueText;

        public InfomationWindow()
        {
        }

        public void AddLayer(asd.Layer2D layer)
        {
            _valueText = new asd.TextObject2D();
            _valueText.Font = Singleton.GetFont();
            layer.AddObject(_valueText);
        }

        public void ShowText(asd.Vector2DF pos, string text)
        {
            _valueText.Text = text;
            _valueText.Position = new asd.Vector2DF(pos.X, pos.Y);
        }

        public void AppendText(asd.Vector2DF pos, string text)
        {
            _valueText.Text += text;
            _valueText.Position = new asd.Vector2DF(pos.X, pos.Y);
        }
    }
}
