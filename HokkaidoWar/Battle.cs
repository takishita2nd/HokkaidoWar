using HokkaidoWar.Model;
using HokkaidoWar.Scene;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HokkaidoWar.GameData;

namespace HokkaidoWar
{
    class Battle
    {
        private const int maxRate = 50;
        private const int minRate = 1;

        private List<City> _cities = null;
        private List<City> aliveCities = null;
        private int cityCnt;

        private City lastAttack = null;
        private City lastDeffece = null;

        public Battle(List<City> cities)
        {
            cityCnt = 0;
            _cities = new List<City>();
            foreach(var c in cities)
            {
                _cities.Add(c);
            }
            _cities = cityRandomReplace(_cities);
            aliveCities = copyCity(_cities);
        }

        public List<City> GetCityList()
        {
            return _cities;
        }

        public List<City> GetAliveCityList()
        {
            return aliveCities;
        }

        public City GetActionCity()
        {
            return _cities[cityCnt];
        }

        public void NextTurn(City player)
        {
            //色を元に戻す
            //if (lastDeffece != null)
            //{
            //    lastDeffece.ClearPaint();
            //}
            if (lastAttack != null)
            {
                lastAttack.ClearPaint();
            }

            //if (!_cities[cityCnt].IsAlive)
            //{
            //    cityCnt++;
            //    if (cityCnt >= _cities.Count)
            //    {
            //        _cities = cityRandomReplace(aliveCities);
            //        aliveCities = copyCity(_cities);
            //        cityCnt = 0;
            //        turn++;
            //    }
            //    return;
            //}

            //var targets = _cities[cityCnt].GetLinkedCities();
            //var r = Singleton.Random;
            //int targetIdx = r.Next(0, targets.Count + 1);
            lastAttack = _cities[cityCnt];
            lastAttack.PaintAttackColor();

            //var info = Singleton.GameProcessInfomation;
            //if(targetIdx >= targets.Count)
            //{
            //    info.ShowText(lastAttack.GetPosition(), string.Format("{0} turn {1} / {2} {3}",
            //        turn, cityCnt + 1, _cities.Count, lastAttack.Name));
            //}
            //else
            //{
            //    lastDeffece = targets[targetIdx];
            //    lastDeffece.PaintDeffenceColor();
            //    if(lastDeffece.Equals(player))
            //    {
            //        var scene = new BattleScene(lastAttack, player, BattleScene.Player.Deffence);
            //        asd.Engine.ChangeScene(scene);
            //    }
            //    else
            //    {
            //        double attack = lastAttack.Population * (double)(r.Next(minRate, maxRate) / 10.0);
            //        double deffence = lastDeffece.Population * (double)(r.Next(minRate, maxRate) / 10.0);
            //        if (attack > deffence)
            //        {
            //            info.ShowText(lastAttack.GetPosition(), string.Format("{0} turn {1} / {2} {3}\r\ntarget {4} \r\n{5} vs {6}\r\nwin",
            //                turn, cityCnt + 1, _cities.Count, lastAttack.Name, lastDeffece.Name, (int)attack, (int)deffence));
            //            lastAttack.CombinationCity(lastDeffece);
            //            lastDeffece.Lose();
            //            aliveCities.Remove(lastDeffece);
            //            lastDeffece = null;
            //        }
            //        else
            //        {
            //            info.ShowText(lastAttack.GetPosition(), string.Format("{0} turn {1} / {2} {3}\r\ntarget {4} \r\n{5} vs {6}\r\nlose",
            //                turn, cityCnt + 1, _cities.Count, lastAttack.Name, lastDeffece.Name, (int)attack, (int)deffence));
            //        }
            //    }
            //}

            cityCnt++;
            if(cityCnt >= _cities.Count)
            {
                _cities = cityRandomReplace(aliveCities);
                aliveCities = copyCity(_cities);
                cityCnt = 0;
                Singleton.GameData.TurnNumber++;
                Singleton.GameData.gameStatus = GameStatus.ShowTurn;
            }
        }

        public void EnemyTurnEnd(BattleResult result)
        {
            if(result == BattleResult.win)
            {
                //lastAttack.CombinationCity(lastDeffece);
                lastDeffece.Lose();
                aliveCities.Remove(lastDeffece);
                lastDeffece = null;
            }
        }

        public void MyTurn(City player)
        {
            if (lastDeffece != null)
            {
                lastDeffece.ClearPaint();
            }
            if (lastAttack != null)
            {
                lastAttack.ClearPaint();
            }
            //var info = Singleton.GameProcessInfomation;
            //info.ShowText(player.GetPosition(), string.Format("{0} turn {1} / {2} {3}", turn, cityCnt + 1, _cities.Count, player.Name));
        }

        public void MyTrunAttack(City player, City target)
        {
            lastAttack = player;
            lastDeffece = target;
            //var scene = new BattleScene(player, target, BattleScene.Player.Attack);
            //asd.Engine.ChangeScene(scene);
        }

        public void MyTurnEnd(BattleResult result)
        {
            if (result == BattleResult.win)
            {
                //lastAttack.CombinationCity(lastDeffece);
                lastDeffece.Lose();
                aliveCities.Remove(lastDeffece);
                lastDeffece.ClearPaint();
                lastDeffece = null;
            }

            lastAttack.ClearPaint();
            lastAttack = null;

            cityCnt++;
            if (cityCnt >= _cities.Count)
            {
                _cities = cityRandomReplace(aliveCities);
                aliveCities = copyCity(_cities);
                cityCnt = 0;
                Singleton.GameData.TurnNumber++;
                Singleton.GameData.gameStatus = GameStatus.ShowTurn;
            }
        }

        public void MyTurnEnd()
        {
            lastAttack.ClearPaint();
            lastAttack = null;

            cityCnt++;
            if (cityCnt >= _cities.Count)
            {
                _cities = cityRandomReplace(aliveCities);
                aliveCities = copyCity(_cities);
                cityCnt = 0;
                Singleton.GameData.TurnNumber++;
                Singleton.GameData.gameStatus = GameStatus.ShowTurn;
            }
        }

        private List<City> copyCity(List<City> cities)
        {
            List<City> ret = new List<City>();
            foreach(var c in cities)
            {
                ret.Add(c);
            }
            return ret;
        }

        private List<City> cityRandomReplace(List<City> beforeCities)
        {
            var r = Singleton.Random;
            List<City> afterCities = new List<City>();
            int max = beforeCities.Count;
            for (int i = 0; i < max; i++)
            {
                int index = r.Next(0, beforeCities.Count - 1);
                afterCities.Add(beforeCities[index]);
                beforeCities.RemoveAt(index);
            }

            return afterCities;
        }
    }
}
