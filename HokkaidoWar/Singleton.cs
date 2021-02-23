using HokkaidoWar.Model;
using HokkaidoWar.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HokkaidoWar
{
    class Singleton
    {
        private static GameData _gameData = null;
        private static InfomationWindow _info = null;
        private static InfomationWindow _gameInfo = null;
        private static Random random = null;
        private static asd.Font _font = null;
        private static asd.Font _LargeFont = null;
        private static FieldMap _map = null;
        private static asd.Texture2D _texture_gu = null;
        private static asd.Texture2D _texture_choki = null;
        private static asd.Texture2D _texture_par = null;
        private static asd.Texture2D _texture_gu2 = null;
        private static asd.Texture2D _texture_choki2 = null;
        private static asd.Texture2D _texture_par2 = null;
        private static asd.Texture2D _texture_ok = null;
        private static asd.Texture2D _texture_ok2 = null;
        private static asd.Texture2D _texture_cancel = null;
        private static asd.Texture2D _texture_cancel2 = null;
        private static asd.Texture2D _texture_attack = null;
        private static asd.Texture2D _texture_attack2 = null;
        private static asd.Texture2D _texture_powerup = null;
        private static asd.Texture2D _texture_powerup2 = null;
        private static asd.Texture2D _texture_charge = null;
        private static asd.Texture2D _texture_charge2 = null;
        private static asd.Texture2D _texture_siege = null;
        private static asd.Texture2D _texture_siege2 = null;
        private static asd.Texture2D _texture_shoot = null;
        private static asd.Texture2D _texture_shoot2 = null;
        private static asd.Texture2D _texture_deffence = null;
        private static asd.Texture2D _texture_deffence2 = null;
        private static asd.Texture2D _texture_max = null;

        public static GameData GameData
        {
            get
            {
                if (_gameData == null)
                {
                    _gameData = new GameData();
                }
                return _gameData;
            }
        }

        public static Random Random
        {
            get
            {
                if (random == null)
                {
                    random = new Random();
                }
                return random;
            }
        }

        public static asd.Font Font
        {
            get
            {
                if (_font == null)
                {
                    _font = asd.Engine.Graphics.CreateFont("FontText.aff");
                }
                return _font;
            }
        }

        public static asd.Font LargeFont
        {
            get
            {
                if (_LargeFont == null)
                {
                    _LargeFont = asd.Engine.Graphics.CreateFont("FontTextLarge.aff");
                }
                return _LargeFont;
            }
        }

        public static InfomationWindow InfomationWindow
        {
            get
            {
                if (_info == null)
                {
                    _info = new InfomationWindow();
                }
                return _info;
            }
        }

        public static InfomationWindow GameProcessInfomation
        {
            get
            {
                if (_gameInfo == null)
                {
                    _gameInfo = new InfomationWindow();
                }
                return _gameInfo;
            }
        }

        public static FieldMap FieldMap
        {
            get
            {
                if (_map == null)
                {
                    _map = new FieldMap();
                }
                return _map;
            }
        }

        public static asd.Texture2D ImageGu
        {
            get
            {
                if (_texture_gu == null)
                {
                    _texture_gu = asd.Engine.Graphics.CreateTexture2D("image_gu.png");
                }
                return _texture_gu;
            }
        }

        public static asd.Texture2D ImageChoki
        {
            get
            {
                if (_texture_choki == null)
                {
                    _texture_choki = asd.Engine.Graphics.CreateTexture2D("image_choki.png");
                }
                return _texture_choki;
            }
        }

        public static asd.Texture2D ImagePar
        {
            get
            {
                if (_texture_par == null)
                {
                    _texture_par = asd.Engine.Graphics.CreateTexture2D("image_par.png");
                }
                return _texture_par;
            }
        }

        public static asd.Texture2D ImageGu2
        {
            get
            {
                if (_texture_gu2 == null)
                {
                    _texture_gu2 = asd.Engine.Graphics.CreateTexture2D("image_gu2.png");
                }
                return _texture_gu2;
            }
        }

        public static asd.Texture2D ImageChoki2
        {
            get
            {
                if (_texture_choki2 == null)
                {
                    _texture_choki2 = asd.Engine.Graphics.CreateTexture2D("image_choki2.png");
                }
                return _texture_choki2;
            }
        }

        public static asd.Texture2D ImagePar2
        {
            get
            {
                if (_texture_par2 == null)
                {
                    _texture_par2 = asd.Engine.Graphics.CreateTexture2D("image_par2.png");
                }
                return _texture_par2;
            }
        }

        public static asd.Texture2D ImageOK
        {
            get
            {
                if (_texture_ok == null)
                {
                    _texture_ok = asd.Engine.Graphics.CreateTexture2D("image_OK.png");
                }
                return _texture_ok;
            }
        }
        public static asd.Texture2D ImageOK2
        {
            get
            {
                if (_texture_ok2 == null)
                {
                    _texture_ok2 = asd.Engine.Graphics.CreateTexture2D("image_OK2.png");
                }
                return _texture_ok2;
            }
        }
        public static asd.Texture2D ImageCancel
        {
            get
            {
                if (_texture_cancel == null)
                {
                    _texture_cancel = asd.Engine.Graphics.CreateTexture2D("image_Cancel.png");
                }
                return _texture_cancel;
            }
        }
        public static asd.Texture2D ImageCancel2
        {
            get
            {
                if (_texture_cancel2 == null)
                {
                    _texture_cancel2 = asd.Engine.Graphics.CreateTexture2D("image_Cancel2.png");
                }
                return _texture_cancel2;
            }
        }
        public static asd.Texture2D ImageAttack
        {
            get
            {
                if (_texture_attack == null)
                {
                    _texture_attack = asd.Engine.Graphics.CreateTexture2D("attack1.png");
                }
                return _texture_attack;
            }
        }
        public static asd.Texture2D ImageAttack2
        {
            get
            {
                if (_texture_attack2 == null)
                {
                    _texture_attack2 = asd.Engine.Graphics.CreateTexture2D("attack2.png");
                }
                return _texture_attack2;
            }
        }
        public static asd.Texture2D ImagePowerup
        {
            get
            {
                if (_texture_powerup == null)
                {
                    _texture_powerup = asd.Engine.Graphics.CreateTexture2D("powerup1.png");
                }
                return _texture_powerup;
            }
        }
        public static asd.Texture2D ImagePowerup2
        {
            get
            {
                if (_texture_powerup2 == null)
                {
                    _texture_powerup2 = asd.Engine.Graphics.CreateTexture2D("powerup2.png");
                }
                return _texture_powerup2;
            }
        }
        public static asd.Texture2D ImageCharge
        {
            get
            {
                if (_texture_charge == null)
                {
                    _texture_charge = asd.Engine.Graphics.CreateTexture2D("Command_Charge.png");
                }
                return _texture_charge;
            }
        }
        public static asd.Texture2D ImageCharge2
        {
            get
            {
                if (_texture_charge2 == null)
                {
                    _texture_charge2 = asd.Engine.Graphics.CreateTexture2D("Command_Charge2.png");
                }
                return _texture_charge2;
            }
        }
        public static asd.Texture2D ImageSiege
        {
            get
            {
                if (_texture_siege == null)
                {
                    _texture_siege = asd.Engine.Graphics.CreateTexture2D("Command_Siege.png");
                }
                return _texture_siege;
            }
        }
        public static asd.Texture2D ImageSiege2
        {
            get
            {
                if (_texture_siege2 == null)
                {
                    _texture_siege2 = asd.Engine.Graphics.CreateTexture2D("Command_Siege2.png");
                }
                return _texture_siege2;
            }
        }
        public static asd.Texture2D ImageShoot
        {
            get
            {
                if (_texture_shoot == null)
                {
                    _texture_shoot = asd.Engine.Graphics.CreateTexture2D("Command_Shoot.png");
                }
                return _texture_shoot;
            }
        }
        public static asd.Texture2D ImageShoot2
        {
            get
            {
                if (_texture_shoot2 == null)
                {
                    _texture_shoot2 = asd.Engine.Graphics.CreateTexture2D("Command_Shoot2.png");
                }
                return _texture_shoot2;
            }
        }
        public static asd.Texture2D ImageDeffence
        {
            get
            {
                if (_texture_deffence == null)
                {
                    _texture_deffence = asd.Engine.Graphics.CreateTexture2D("Command_Deffence.png");
                }
                return _texture_deffence;
            }
        }
        public static asd.Texture2D ImageDeffence2
        {
            get
            {
                if (_texture_deffence2 == null)
                {
                    _texture_deffence2 = asd.Engine.Graphics.CreateTexture2D("Command_Deffence2.png");
                }
                return _texture_deffence2;
            }
        }

        public static asd.Texture2D ImageMAX
        {
            get
            {
                if (_texture_max == null)
                {
                    _texture_max = asd.Engine.Graphics.CreateTexture2D("max.png");
                }
                return _texture_max;
            }
        }
    }
}
