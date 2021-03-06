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
            // マップデータの取得
            var gameData = Singleton.GameData;
            gameData.setMapData(FileAccess.Load());

            foreach (var citydata in gameData.MapData.citydata)
            {
                City city = new City(citydata);
                gameData.Cities.Add(city);
            }
            gameData.gameStatus = GameData.GameStatus.SelectCity;
            gameData.Battleinitialize();
        }

        public void Run()
        {
            asd.Engine.Initialize("北海道大戦", 1900, 1000, new asd.EngineOption());

            // asd.Engine.File.AddRootPackage("hokkaido.pack");

            // シーンの登録
            asd.Engine.ChangeScene(new TitleScene());

            while (asd.Engine.DoEvents())
            {
                asd.Engine.Update();
            }
            asd.Engine.Terminate();
        }

    }
}
