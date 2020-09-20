using HokkaidoWar.Model;
using HokkaidoWar.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HokkaidoWar
{
    class HokkaidoWar
    {

        public HokkaidoWar()
        {
        }

        public void Run()
        {
            asd.Engine.Initialize("北海道大戦", 1200, 1000, new asd.EngineOption());

            // シーンの登録
            var scene = new MainScene();
            asd.Engine.ChangeScene(scene);

            while (asd.Engine.DoEvents())
            {
                asd.Engine.Update();
            }
            asd.Engine.Terminate();
        }

    }
}
