using asd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HokkaidoWar.Model
{
    class NumberDialog
    {
        public enum Result{
            OK,
            Cancel,
            None
        }

        private asd.GeometryObject2D _dialog = null;
        private asd.TextureObject2D _okButton = null;
        private asd.TextureObject2D _cancelButton = null;
        private GeometryObject2D[] _geometryObj = new GeometryObject2D[4];
        private LineShape[] _line = new LineShape[4];

        private const int buttonWidth = 267;
        private const int buttonHeight = 63;
        private const int dialogX = 300;
        private const int dialogY = 300;
        private const int dialogWidth = 600;
        private const int dialogHeight = 400;

        public NumberDialog()
        {
            _dialog = new GeometryObject2D();
            _dialog.Color = new Color(255, 255, 255);
            _dialog.DrawingPriority = 15;

            var dialogRect = new RectangleShape();
            dialogRect.DrawingArea = new RectF(dialogX, dialogY, dialogWidth, dialogHeight);
            _dialog.Shape = dialogRect;

            _okButton = new TextureObject2D();
            _okButton.Texture = Singleton.ImageOK;
            _okButton.Position = new Vector2DF(310, 600);
            _okButton.DrawingPriority = 15;

            _cancelButton = new TextureObject2D();
            _cancelButton.Texture = Singleton.ImageCancel;
            _cancelButton.Position = new Vector2DF(620, 600);
            _cancelButton.DrawingPriority = 15;

            for (int i = 0; i < 4; i++)
            {
                _geometryObj[i] = new GeometryObject2D();
                _geometryObj[i].DrawingPriority = 15;
                _geometryObj[i].Color = new Color(0, 0, 0, 255);
                _line[i] = new LineShape();
                _line[i].Thickness = 5;
                _geometryObj[i].Shape = _line[i];
            }
            _line[0].StartingPosition = new Vector2DF(dialogX, dialogY);
            _line[0].EndingPosition = new Vector2DF(dialogX + dialogWidth, dialogY);
            _line[1].StartingPosition = new Vector2DF(dialogX + dialogWidth, dialogY);
            _line[1].EndingPosition = new Vector2DF(dialogX + dialogWidth, dialogY + dialogHeight);
            _line[2].StartingPosition = new Vector2DF(dialogX + dialogWidth, dialogY + dialogHeight);
            _line[2].EndingPosition = new Vector2DF(dialogX, dialogY + dialogHeight);
            _line[3].StartingPosition = new Vector2DF(dialogX, dialogY + dialogHeight);
            _line[3].EndingPosition = new Vector2DF(dialogX, dialogY);

        }

        public void ShowDialog(Layer2D layer)
        {
            layer.AddObject(_dialog);
            for (int i = 0; i < 4; i++)
            {
                layer.AddObject(_geometryObj[i]);
            }
            layer.AddObject(_okButton);
            layer.AddObject(_cancelButton);
        }

        public void CloseDialog(asd.Layer2D layer)
        {
            layer.RemoveObject(_dialog);
            for (int i = 0; i < 4; i++)
            {
                layer.RemoveObject(_geometryObj[i]);
            }
            layer.RemoveObject(_okButton);
            layer.RemoveObject(_cancelButton);
        }

        public void OnMouse(asd.Vector2DF pos)
        {
            if(isClickOK(pos))
            {
                _okButton.Texture = Singleton.ImageOK2;
            }
            else
            {
                _okButton.Texture = Singleton.ImageOK;
            }

            if (isClickCancel(pos))
            {
                _cancelButton.Texture = Singleton.ImageCancel2;
            }
            else
            {
                _cancelButton.Texture = Singleton.ImageCancel;
            }
        }

        public Result OnClick(asd.Vector2DF pos)
        {
            if (isClickOK(pos))
            {
                return Result.OK;
            }
            else if (isClickCancel(pos))
            {
                return Result.Cancel;
            }
            else
            {
                return Result.None;
            }
        }

        private bool isClickOK(asd.Vector2DF pos)
        {
            if (pos.X > _okButton.Position.X && pos.X < _okButton.Position.X + buttonWidth
                && pos.Y > _okButton.Position.Y && pos.Y < _okButton.Position.Y + buttonHeight)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool isClickCancel(asd.Vector2DF pos)
        {
            if (pos.X > _cancelButton.Position.X && pos.X < _cancelButton.Position.X + buttonWidth
                && pos.Y > _cancelButton.Position.Y && pos.Y < _cancelButton.Position.Y + buttonHeight)
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
