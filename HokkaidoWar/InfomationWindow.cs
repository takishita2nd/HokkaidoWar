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
        private TextObject2D _valueText;
        private GeometryObject2D _windowBox;
        private GeometryObject2D[] _geometryObj = new GeometryObject2D[4];
        private RectangleShape _rect;
        private LineShape[] _line = new LineShape[4];
        private const int rectWidth = 250;
        private const int rectHeight = 70;
        private const int xPositionOffset = 10;
        private bool _isShow = false;

        public InfomationWindow()
        {
        }

        public void Show(Layer2D layer)
        {
            _windowBox = new GeometryObject2D();
            _windowBox.DrawingPriority = 10;
            _windowBox.Color = new Color(255, 255, 255, 255);
            _rect = new RectangleShape();
            layer.AddObject(_windowBox);

            for(int i = 0; i < 4; i++)
            {
                _geometryObj[i] = new GeometryObject2D();
                _geometryObj[i].DrawingPriority = 10;
                _geometryObj[i].Color = new Color(0, 0, 0, 255);
                _line[i] = new LineShape();
                _line[i].Thickness = 5;
                _geometryObj[i].Shape = _line[i];
                layer.AddObject(_geometryObj[i]);
            }

            _valueText = new TextObject2D();
            _valueText.Font = Singleton.Font;
            _valueText.DrawingPriority = 20;
            layer.AddObject(_valueText);
            _isShow = true;
        }

        public void Hide(Layer2D layer)
        {
            layer.RemoveObject(_windowBox);
            for (int i = 0; i < 4; i++)
            {
                layer.RemoveObject(_geometryObj[i]);
            }
            layer.RemoveObject(_valueText);
            _isShow = false;
        }

        public bool IsShow()
        {
            return _isShow;
        }

        public void ShowText(Vector2DF pos, string text)
        {
            _rect.DrawingArea = new RectF(pos.X, pos.Y, rectWidth, rectHeight);
            _windowBox.Shape = _rect;
            _line[0].StartingPosition = new Vector2DF(pos.X, pos.Y);
            _line[0].EndingPosition = new Vector2DF(pos.X + rectWidth, pos.Y);
            _line[1].StartingPosition = new Vector2DF(pos.X + rectWidth, pos.Y);
            _line[1].EndingPosition = new Vector2DF(pos.X + rectWidth, pos.Y + rectHeight);
            _line[2].StartingPosition = new Vector2DF(pos.X + rectWidth, pos.Y + rectHeight);
            _line[2].EndingPosition = new Vector2DF(pos.X, pos.Y + rectHeight);
            _line[3].StartingPosition = new Vector2DF(pos.X, pos.Y + rectHeight);
            _line[3].EndingPosition = new Vector2DF(pos.X, pos.Y);
            _valueText.Text = text;
            _valueText.Position = new Vector2DF(pos.X + xPositionOffset, pos.Y);
        }

        public void AppendText(Vector2DF pos, string text)
        {
            _valueText.Text += text;
            _valueText.Position = new Vector2DF(pos.X + xPositionOffset, pos.Y);
        }
    }
}
