using asd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HokkaidoWar.Model
{
    class NumberDialogButton
    {
        public enum UpDown
        {
            Up,
            Down
        }

        private TextObject2D _button = null;
        private const int width = 40;
        private const int height = 50;

        public NumberDialogButton(UpDown upDown)
        {
            _button = new TextObject2D();
            _button.Font = Singleton.LargeFont;
            if(upDown == UpDown.Up)
            {
                _button.Text = "▲";
            }
            else
            {
                _button.Text = "▼";
            }
            _button.DrawingPriority = 15;
        }

        public void SetPosition(Vector2DF pos)
        {
            _button.Position = pos;
        }

        public void Show(Layer2D layer)
        {
            layer.AddObject(_button);
        }

        public void Hide(Layer2D layer)
        {
            layer.RemoveObject(_button);
        }

        public bool IsClick(Vector2DF pos)
        {
            if (pos.X > _button.Position.X && pos.X < _button.Position.X + width
                && pos.Y > _button.Position.Y && pos.Y < _button.Position.Y + height)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
