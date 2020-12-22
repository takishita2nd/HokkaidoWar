using HokkaidoWar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HokkaidoWar
{
    class GameData
    {
        public enum GameStatus
        {
            None,
            SelectCity,
            VerificateCity,
            ActionEnemy,
            ActionPlayer,
            GameEnd,
            GameOver
        }

        public enum BattleResult
        {
            win,
            draw,
            lose
        }

        public GameStatus gameStatus = GameStatus.None;
        public List<City> Cities = null;
        public List<City> AliveCities = null;
        public Battle Battle = null;
        public Player Player = null;
        public MapData MapData = null;

        public GameData()
        {
            Cities = new List<City>();
        }

        public void setMapData(MapData mapData)
        {
            MapData = mapData;
        }

        public void Battleinitialize()
        {
            Battle = new Battle(Cities);
            AliveCities = Battle.GetAliveCityList();
        }

        public void OnMouseCity(asd.Vector2DF pos)
        {
            if (isOnMouseMap(pos))
            {
                foreach (var city in AliveCities)
                {
                    city.OnMouse(pos);
                }
            }
        }

        public City GetSelectedCity(asd.Vector2DF pos)
        {
            City retcity = null;
            if (isOnMouseMap(pos))
            {
                foreach (var city in AliveCities)
                {
                    if (city.IsOnMouse(pos))
                    {
                        retcity = city;
                        break;
                    }
                }
            }
            return retcity;
        }

        public void CreatePlayer(City selectedCity)
        {
            Player = new Player(selectedCity);
        }

        public bool IsPlayerTrun()
        {
            return Player.City.Equals(Battle.GetActionCity());
        }

        public bool IsPlayerAlive()
        {
            return AliveCities.Contains(Player.City);
        }

        public void PlayNextCity()
        {
            Battle.NextTurn(Player.City);
            AliveCities = Battle.GetAliveCityList();
        }

        public void PlayPlayer()
        {
            Battle.MyTurn(Player.City);
            Player.City.PaintAttackColor();

        }

        public bool IsOnMousePlayerCity(asd.Vector2DF pos)
        {
            return Player.City.IsOnMouse(pos);
        }

        public void OnMousePlayerCity(asd.Vector2DF pos)
        {
            Player.City.OnMouse(pos);
        }

        public List<City> GetPlayerLinkedCities()
        {
            return Player.City.GetLinkedCities();
        }

        public void PlayerAttackCity(City target)
        {
            Battle.MyTrunAttack(Player.City, target);
        }

        public asd.Vector2DF GetPlayCityPosition()
        {
            return Player.City.GetPosition();
        }

        public void BattleResultUpdate(BattleResult result)
        {
            if (gameStatus == GameStatus.ActionEnemy)
            {
                Battle.EnemyTurnEnd(result);
            }
            else if (gameStatus == GameStatus.ActionPlayer)
            {
                Battle.MyTurnEnd(result);
                var aliveCity = Battle.GetAliveCityList();
                if(aliveCity.Count == 1)
                {
                    gameStatus = GameStatus.GameEnd;
                }
                else
                {
                    gameStatus = GameStatus.ActionEnemy;
                }
            }
        }

        private bool isOnMouseMap(asd.Vector2DF pos)
        {
            bool ret = false;
            foreach (var city in AliveCities)
            {
                if (city.IsOnMouse(pos))
                {
                    ret = true;
                }
            }
            return ret;
        }
    }
}
