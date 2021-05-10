using HokkaidoWar.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HokkaidoWar
{
    class FileAccess
    {
        private const string _filename = "hokkaido.json";
        public static MapData Load()
        {
            string json;
            using (var stream = new StreamReader(_filename, true))
            {
                json = stream.ReadToEnd();
            }
            //string str = string.Empty;
            //asd.StreamFile stream = asd.Engine.File.CreateStreamFile(_filename);
            //List<byte> buffer = new List<byte>();
            //stream.Read(buffer, stream.Size);
            //string json = Encoding.UTF8.GetString(buffer.ToArray());
            return JsonConvert.DeserializeObject<MapData>(json);
        }

        private const string _savefilename = "save.json";
        public static void SaveData(GameData gamedata)
        {
            SaveData saveData = new SaveData();
            saveData.Turn = gamedata.TurnNumber;
            saveData.PlayerId = gamedata.Player.City.Id;
            List<CityData> cityDatas = new List<CityData>();
            foreach(var city in gamedata.AliveCities)
            {
                if (city.IsAlive)
                {
                    CityData data = new CityData();
                    data.id = city.Id;
                    data.name = city.Name;
                    data.money = city.Money;
                    data.power = city.Power;
                    data.bonus = city.Bonus;
                    List<int> mapid = new List<int>();
                    foreach (var map in city.GetMaps())
                    {
                        mapid.Add(map.Id);
                    }
                    data.mapid = mapid.ToArray();
                    cityDatas.Add(data);
                }
            }
            saveData.Citydata = cityDatas.ToArray();
            string json = JsonConvert.SerializeObject(saveData);
            using (var stream = new StreamWriter(_savefilename, false))
            {
                stream.WriteLine(json);
            }
        }

        public static SaveData LoadData()
        {
            string json;
            using (var stream = new StreamReader(_savefilename, true))
            {
                json = stream.ReadToEnd();
            }
            return JsonConvert.DeserializeObject<SaveData>(json);
        }
    }
}
