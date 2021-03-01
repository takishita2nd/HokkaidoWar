using HokkaidoWar.Model;
using HokkaidoWar.Scene;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        private Map targetMap = null;

        public enum BattleAction
        {
            Charge,
            Siege,
            Shoot,
        }
        private BattleAction[] Action = { BattleAction.Charge, BattleAction.Shoot, BattleAction.Siege };

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

        public string NextTurn(City player)
        {
            string message = string.Empty;
            //色を元に戻す
            if (lastDeffece != null)
            {
                lastDeffece.ClearPaint();
            }
            if (lastAttack != null)
            {
                lastAttack.ClearPaint();
            }

            if (!_cities[cityCnt].IsAlive)
            {
                cityCnt++;
                if (cityCnt >= _cities.Count)
                {
                    _cities = cityRandomReplace(aliveCities);
                    aliveCities = copyCity(_cities);
                    cityCnt = 0;
                    Singleton.GameData.TurnNumber++;
                }
                return message;
            }

            var targets = _cities[cityCnt].GetLinkedMaps();
            var r = Singleton.Random;
            int targetIdx = r.Next(0, targets.Count + 2);
            
            lastAttack = _cities[cityCnt];
            lastAttack.PaintAttackColor();

            if(targetIdx < targets.Count)
            {
                targetMap = targets[targetIdx];
                lastDeffece = targetMap.GetCity();
                message = lastDeffece.Name + "に攻撃";
                lastDeffece.PaintDeffenceColor();
                if (lastDeffece.Equals(player))
                {
                    var scene = new BattleScene(lastAttack, player, BattleScene.Player.Deffence);
                    asd.Engine.ChangeScene(scene);
                }
                else
                {
                    var result = enemyBattle(lastAttack, lastDeffece);
                    if(result == BattleResult.win)
                    {
                        message += "\r\n勝利";
                        lastAttack.AddMap(targetMap);
                        lastDeffece.LostMap(targetMap);
                        lastAttack.ResetPower();
                    }
                    else
                    {
                        message += "\r\n敗北";
                        lastAttack.ResetPower();
                        lastDeffece.ResetPower();
                    }
                    targetMap = null;
                }
            }
            else if(targetIdx < targets.Count + 1)
            {
                int payMoney = r.Next(0, lastAttack.Money);
                lastAttack.PayMoney(payMoney);
                lastAttack.AddPower(payMoney);
            }

            cityCnt++;
            if(cityCnt >= _cities.Count)
            {
                _cities = cityRandomReplace(aliveCities);
                aliveCities = copyCity(_cities);
                cityCnt = 0;
                Singleton.GameData.TurnNumber++;
                Singleton.GameData.gameStatus = GameStatus.ShowTurn;
            }
            return message;
        }

        public void EnemyTurnEnd(BattleResult result)
        {
            if(result == BattleResult.win)
            {
                lastAttack.AddMap(targetMap);
                lastDeffece.LostMap(targetMap);
                if(!lastDeffece.IsAlive)
                {
                    aliveCities.Remove(lastDeffece);
                }
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
        }

        public void MyTrunAttack(City player, City target, Map map)
        {
            lastAttack = player;
            lastDeffece = target;
            targetMap = map;
            var scene = new BattleScene(player, target, BattleScene.Player.Attack);
            asd.Engine.ChangeScene(scene);
        }

        public void MyTurnEnd(BattleResult result)
        {
            if (result == BattleResult.win)
            {
                lastAttack.AddMap(targetMap);
                lastDeffece.LostMap(targetMap);
                if (!lastDeffece.IsAlive)
                {
                    aliveCities.Remove(lastDeffece);
                }
            }
            if (lastAttack.Power < lastAttack.Population)
            {
                lastAttack.ResetPower();
            }

            lastDeffece.ClearPaint();
            lastDeffece = null;
            lastAttack.ClearPaint();
            lastAttack = null;
            targetMap = null;

            cityCnt++;
            if (cityCnt >= _cities.Count)
            {
                _cities = cityRandomReplace(aliveCities);
                aliveCities = copyCity(_cities);
                cityCnt = 0;
                Singleton.GameData.TurnNumber++;
                Singleton.GameData.gameStatus = GameStatus.ShowTurn;
            }
            else
            {
                Singleton.GameData.gameStatus = GameStatus.ActionEnemy;
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

        private BattleResult enemyBattle(City attack, City deffence)
        {
            var r = Singleton.Random;

            BattleAction battleActionAttack = Action[r.Next(0, Action.Length - 1)];
            BattleAction battleActionDeffence = Action[r.Next(0, Action.Length - 1)];
            switch (battleActionAttack)
            {
                case BattleAction.Charge:
                    switch (battleActionDeffence)
                    {
                        case BattleAction.Charge:
                            if (attack.Power > deffence.Power)
                            {
                                return BattleResult.win;
                            }
                            else
                            {
                                return BattleResult.lose;
                            }
                        case BattleAction.Siege:
                            if (attack.Power * 1.5f > deffence.Power)
                            {
                                return BattleResult.win;
                            }
                            else
                            {
                                return BattleResult.lose;
                            }
                        case BattleAction.Shoot:
                            if (attack.Power > deffence.Power * 1.5f)
                            {
                                return BattleResult.win;
                            }
                            else
                            {
                                return BattleResult.lose;
                            }
                        default:
                            return BattleResult.win;
                    }
                case BattleAction.Siege:
                    switch (battleActionDeffence)
                    {
                        case BattleAction.Siege:
                            if (attack.Power > deffence.Power)
                            {
                                return BattleResult.win;
                            }
                            else
                            {
                                return BattleResult.lose;
                            }
                        case BattleAction.Shoot:
                            if (attack.Power * 1.5f > deffence.Power)
                            {
                                return BattleResult.win;
                            }
                            else
                            {
                                return BattleResult.lose;
                            }
                        case BattleAction.Charge:
                            if (attack.Power > deffence.Power * 1.5f)
                            {
                                return BattleResult.win;
                            }
                            else
                            {
                                return BattleResult.lose;
                            }

                        default:
                            return BattleResult.win;
                    }
                case BattleAction.Shoot:
                    switch (battleActionDeffence)
                    {
                        case BattleAction.Shoot:
                            if (attack.Power > deffence.Power)
                            {
                                return BattleResult.win;
                            }
                            else
                            {
                                return BattleResult.lose;
                            }
                        case BattleAction.Charge:
                            if (attack.Power * 1.5f > deffence.Power)
                            {
                                return BattleResult.win;
                            }
                            else
                            {
                                return BattleResult.lose;
                            }
                        case BattleAction.Siege:
                            if (attack.Power > deffence.Power * 1.5f)
                            {
                                return BattleResult.win;
                            }
                            else
                            {
                                return BattleResult.lose;
                            }
                        default:
                            return BattleResult.win;
                    }
                default:
                    return BattleResult.win;
            }

        }
    }
}
