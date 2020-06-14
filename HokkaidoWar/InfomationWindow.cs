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
            _valueText = new asd.TextObject2D();
            _valueText.Font = Singleton.GetFont();
            asd.Engine.AddObject2D(_valueText);
        }

        public void ShowText(asd.Vector2DF pos, string text)
        {
            _valueText.Text = text;
            _valueText.Position = new asd.Vector2DF(pos.X, pos.Y);
        }
    }
}
