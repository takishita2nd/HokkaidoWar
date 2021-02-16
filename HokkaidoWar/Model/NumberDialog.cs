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

        private GeometryObject2D _dialog = null;
        private TextureObject2D _okButton = null;
        private TextureObject2D _cancelButton = null;
        private GeometryObject2D[] _geometryObj = new GeometryObject2D[4];
        private LineShape[] _line = new LineShape[4];

        private const int buttonWidth = 267;
        private const int buttonHeight = 63;
        private const int dialogX = 300;
        private const int dialogY = 300;
        private const int dialogWidth = 600;
        private const int dialogHeight = 400;

        private TextObject2D[] _number = new TextObject2D[7];
        private NumberDialogButton[] _up = new NumberDialogButton[7];
        private NumberDialogButton[] _down = new NumberDialogButton[7];
        private TextObject2D _money = new TextObject2D();
        private TextObject2D _max = new TextObject2D();

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

            for(int i = 0; i<7; i++)
            {
                _number[i] = new TextObject2D();
                _number[i].Font = Singleton.LargeFont;
                _number[i].Text = "0";
                _number[i].Position = new Vector2DF(450 + 40 * i, 450);
                _number[i].DrawingPriority = 15;

                _up[i] = new NumberDialogButton(NumberDialogButton.UpDown.Up);
                _up[i].SetPosition(new Vector2DF(440 + 40 * i, 400));

                _down[i] = new NumberDialogButton(NumberDialogButton.UpDown.Down);
                _down[i].SetPosition(new Vector2DF(440 + 40 * i, 500));
            }

            _max = new TextObject2D();
            _max.Font = Singleton.LargeFont;
            _max.Text = "MAX";
            _max.Position = new Vector2DF(740, 450);
            _max.DrawingPriority = 15;

            _money = new TextObject2D();
            _money.Font = Singleton.LargeFont;
            _money.Text = "金";
            _money.Position = new Vector2DF(370, 320);
            _money.DrawingPriority = 15;

        }

        public void ShowDialog(Layer2D layer)
        {
            layer.AddObject(_dialog);
            for (int i = 0; i < 4; i++)
            {
                layer.AddObject(_geometryObj[i]);
            }
            for (int i = 0; i < 7; i++)
            {
                layer.AddObject(_number[i]);
                _up[i].Show(layer);
                _down[i].Show(layer);
            }
            layer.AddObject(_okButton);
            layer.AddObject(_cancelButton);
            layer.AddObject(_max);
            layer.AddObject(_money);
        }

        public void CloseDialog(asd.Layer2D layer)
        {
            layer.RemoveObject(_dialog);
            for (int i = 0; i < 4; i++)
            {
                layer.RemoveObject(_geometryObj[i]);
            }
            for (int i = 0; i < 7; i++)
            {
                layer.RemoveObject(_number[i]);
                _up[i].Hide(layer);
                _down[i].Hide(layer);
            }
            layer.RemoveObject(_okButton);
            layer.RemoveObject(_cancelButton);
            layer.RemoveObject(_max);
            layer.RemoveObject(_money);
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
