using System;
using System.Collections.Generic;
using Enemies;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Dices
{
    public class DiceManager : MonoBehaviour
    {
        public List<Dice> dices = new List<Dice>();
        public List<Dice> dicesPool = new List<Dice>();
        public int[] dicesIDs;
        private int _iDsLenght = 1;
        
        public Dice currentDice;
        public Dice previousDice;
        public static DiceManager instanceDm;
        private GameManager _gameManager;
        [SerializeField] private Sprite[] reloadSprites;
        [SerializeField] private Transform diceLayout;
        private Player.Player _player;



        public void Update()
        {
            if (Input.GetKeyDown("1") && dices.Count >= 1 && Time.timeScale != 0)
            {
                dices[0].OnClick();
            }
            if (Input.GetKeyDown("2") && dices.Count >= 2 && Time.timeScale != 0)
            {
                dices[1].OnClick();
            }
            if (Input.GetKeyDown("3") && dices.Count >= 3 && Time.timeScale != 0)
            {
                dices[2].OnClick();
            }
            if (Input.GetKeyDown("4") && dices.Count >= 4 && Time.timeScale != 0)
            {
                dices[3].OnClick();
            }
        }

        DiceManager()
        {
            instanceDm = this;
        }

        private void Awake()
        {
            _gameManager = GameManager.instanceGm;
            _player = Player.Player.instancePlayer;
        }


        public void LoadDices(int[] ids)
        {
            dicesIDs = ids;
            for (int i = 0; i < ids.Length; i++)
            {
                dices.Add(dicesPool[ids[i]]);
            }
            for (int i = 0; i < dices.Count; i++)
            {
                GameObject newDice = Instantiate(dices[i].gameObject,diceLayout.position,Quaternion.identity,diceLayout);
                dices[i] = newDice.GetComponent<Dice>();
                

                switch (dices[i].diceReloadingType)
                {
                    case ReloadingType.OnKill:
                        dices[i].SetReloadIcon(reloadSprites[Convert.ToInt32(ReloadingType.OnKill)]);
                        break;
                    case ReloadingType.OnTime:
                        dices[i].SetReloadIcon(reloadSprites[Convert.ToInt32(ReloadingType.OnTime)]);
                        break;
                    case ReloadingType.OnGettingHit:
                        dices[i].SetReloadIcon(reloadSprites[Convert.ToInt32(ReloadingType.OnGettingHit)]);
                        break;
                    case ReloadingType.SacrificeHealth:
                        dices[i].SetReloadIcon(reloadSprites[Convert.ToInt32(ReloadingType.SacrificeHealth)]);
                        break;
                    case ReloadingType.OneUse:
                        dices[i].SetReloadIcon(reloadSprites[Convert.ToInt32(ReloadingType.OneUse)]);
                        break;
                }
            }
        }

        public void ReloadDices()
        {
            int [] charges = new int[dices.Count];
            for (int i = 0; i < dices.Count; i++)
            {
                charges[i] = dices[i].reloadingPoints;
            }
            foreach (var elem in dices)
            {
                Destroy(elem.gameObject);
            }
            dices.Clear();
            for (int i = 0; i < dicesIDs.Length; i++)
            {
                dices.Add(dicesPool[dicesIDs[i]]);
            }
            for (int i = 0; i < dices.Count; i++)
            {
                GameObject newDice = Instantiate(dices[i].gameObject,diceLayout.position,Quaternion.identity,diceLayout);
                dices[i] = newDice.GetComponent<Dice>();

                switch (dices[i].diceReloadingType)
                {
                    case ReloadingType.OnKill:
                        dices[i].SetReloadIcon(reloadSprites[Convert.ToInt32(ReloadingType.OnKill)]);
                        break;
                    case ReloadingType.OnTime:
                        dices[i].SetReloadIcon(reloadSprites[Convert.ToInt32(ReloadingType.OnTime)]);
                        break;
                    case ReloadingType.OnGettingHit:
                        dices[i].SetReloadIcon(reloadSprites[Convert.ToInt32(ReloadingType.OnGettingHit)]);
                        break;
                    case ReloadingType.SacrificeHealth:
                        dices[i].SetReloadIcon(reloadSprites[Convert.ToInt32(ReloadingType.SacrificeHealth)]);
                        break;
                    case ReloadingType.OneUse:
                        dices[i].SetReloadIcon(reloadSprites[Convert.ToInt32(ReloadingType.OneUse)]);
                        break;
                }
            }
            for (int i = 0; i < dices.Count; i++)
            {
                dices[i].reloadingPoints = charges[i];
                dices[i].UpdateReloadingStats();
            }
        }

        public void ReplaceDice(int index, int newId)
        {
            CheckDecreaseHealth(dices[index]);
            dicesIDs[index] = newId;
            CheckIncreaseHealth(dices[index]);
            _gameManager.SavePlayerData();
        }

        public void SwitchDices(int index ,int id,int index2 , int id2)
        {
            int tmp = dices[index].reloadingPoints;
            if (!_gameManager.fightEnded)
            {
                dices[index].reloadingPoints = dices[index2].reloadingPoints;
                dices[index2].reloadingPoints = tmp;
            }
            dicesIDs[index] = id2;
            dicesIDs[index2] = id;
            _gameManager.SavePlayerData();
        }
        
        
        
        public void DeleteDice(int index)
        {
            if (index < 4)
            {
                Destroy(dices[index].gameObject);
                CheckDecreaseHealth(dices[index]);
                dices.Remove(dices[index]);
                _iDsLenght = dicesIDs.Length;
                int[] tmpIDs = new int[_iDsLenght - 1];
                for (int i = 0; i < _iDsLenght; i++)
                {
                    if (i < index)
                    {
                        tmpIDs[i] = dicesIDs[i];
                    }
                    else if (i > index)
                    {
                        tmpIDs[i - 1] = dicesIDs[i];
                    }
                }
                dicesIDs = new int[--_iDsLenght];
                for (int i = 0; i < dicesIDs.Length; i++)
                {
                    dicesIDs[i] = tmpIDs[i];
                }
                _gameManager.SavePlayerData();
            }
            
        }

        public void AddDice(int diceId)
        {
            if (dices.Count < 4 && diceId < dicesPool.Count)
            {
                dices.Add(dicesPool[diceId]);
                _iDsLenght = dicesIDs.Length;
                int[] tmpDicesIDs = dicesIDs;
                dicesIDs = new int[++_iDsLenght];
                for (int i = 0; i < _iDsLenght-1; i++)
                {
                    dicesIDs[i] = tmpDicesIDs[i];
                }
                dicesIDs[_iDsLenght - 1] = diceId;
                GameObject newDice = Instantiate(dices[dices.Count-1].gameObject,diceLayout.position,Quaternion.identity,diceLayout);
                dices[dices.Count - 1] = newDice.GetComponent<Dice>();
                CheckDiceIcon(dices[dices.Count - 1]);
                CheckIncreaseHealth(dices[dices.Count - 1]);
                _gameManager.SavePlayerData();
            }
        }

        public Dice GetRandomDice()
        {
            List<Dice> possibleDices = new List<Dice>(dicesPool);

            List<Dice> tmpDices = new List<Dice>(dices);

            int count = 0;

            for (int id = 0; id < dicesPool.Count; id++)
            {
                for (int number = 0; number < dices.Count; number++)
                {
                    if (dicesPool[id].id == tmpDices[number].id)
                    {
                        possibleDices.Remove(possibleDices[id - count]);
                        count++;
                        break;
                    }
                }
                if (!dicesPool[id].isOpened)
                {
                    possibleDices.Remove(possibleDices[id - count]);
                    count++;
                }
            }

            List<int> finalDicesIds = new List<int>();

            for (int i = 0; i < possibleDices.Count; ++i)
            {
                for (int j = 0; j < Convert.ToInt32(possibleDices[i].diceRarity); j++)
                {
                    finalDicesIds.Add(possibleDices[i].id);
                }
            }
            int newDiceId = Random.Range(0, finalDicesIds.Count);
            return finalDicesIds.Count == 0 ? null : dicesPool[finalDicesIds[newDiceId]];
        }

        public void OpenDice(int diceId)
        {
            int count = dicesPool.Count;
            bool[] newDicesId = new bool[count];
            for (int i = 0; i < count; i++)
            {
                newDicesId[i] = dicesPool[i].isOpened;
            }
            newDicesId[diceId] = true;
            dicesPool[diceId].isOpened = true;
            //Debug.Log("Dice " + diceId + " was opened");
            SaveSystem.SaveSystem.SaveDiceData(newDicesId);
        }

        public void DeactivateDices()
        {
            EnemiesSpawner.instanceES.HighlightEnemies(false);
            EnemiesSpawner.instanceES.EnemiesClickable(false);
            currentDice = null;
            DeactivatePreviousDice();
            _player.TakingDice(null, false);
        }
        public void DeactivatePreviousDice()
        {
            foreach (var elem in dices)
            {
                if(elem == currentDice && elem.isActive) return;
                elem.DeactivateDice();
            }
        }
        
        public void DeactivateDicesButtons()
        {
            foreach (var elem in dices)
            {
                elem.diceButton.interactable = false;
            }
        }
        
        public void UpdateOnKillReloadingPoints()
        {
            foreach (var elem in dices)
            {
                if(elem.IsValidType(ReloadingType.OnKill)) elem.AddingReloadingPoints();
            }
        }
        public void UpdateOnGettingHitReloadingPoints(int damage)
        {
            foreach (var elem in dices)
            {
                if(elem.IsValidType(ReloadingType.OnGettingHit)) elem.AddingReloadingPoints(damage);
            }
        }

        public void UpdateSacrificeHealthReloadingPoints(int cost, Dice dice)
        {
            Player.Player.instancePlayer.TakingDamage(cost);
            dice.AddingReloadingPoints(cost);
        }

        public void UpdateOnTimeReloadingPoints()
        {
            foreach (var elem in dices)
            {
                if(elem.IsValidType(ReloadingType.OnTime)) elem.AddingReloadingPoints();
            }
        }
        
        private void CheckDiceIcon(Dice dice)
        {
            switch (dice.diceReloadingType)
            {
                case ReloadingType.OnKill:
                    dice.SetReloadIcon(reloadSprites[Convert.ToInt32(ReloadingType.OnKill)]);
                    break;
                case ReloadingType.OnTime:
                    dice.SetReloadIcon(reloadSprites[Convert.ToInt32(ReloadingType.OnTime)]);
                    break;
                case ReloadingType.OnGettingHit:
                    dice.SetReloadIcon(reloadSprites[Convert.ToInt32(ReloadingType.OnGettingHit)]);
                    break;
                case ReloadingType.SacrificeHealth:
                    dice.SetReloadIcon(reloadSprites[Convert.ToInt32(ReloadingType.SacrificeHealth)]);
                    break;
                case ReloadingType.OneUse:
                    dice.SetReloadIcon(reloadSprites[Convert.ToInt32(ReloadingType.OneUse)]);
                    break;
            }
        }

        private void CheckIncreaseHealth(Dice dice)
        {
            switch (dice.onPlayerHealthEffect)
            {
                case OnPlayerHealthEffect.None:
                    break;
                case OnPlayerHealthEffect.HealthUp:
                    _player.SetMaxPlayerHealth(_player.maxHealth + Convert.ToInt32(OnPlayerHealthEffect.HealthUp));
                    break;
                case OnPlayerHealthEffect.HealthDown:
                    if (_player.maxHealth + Convert.ToInt32(OnPlayerHealthEffect.HealthDown) <= 0)
                    {
                        _player.maxHealth = 1;
                        _player.SetMaxPlayerHealth(_player.maxHealth);
                    }
                    else
                    {
                        _player.SetMaxPlayerHealth(_player.maxHealth + Convert.ToInt32(OnPlayerHealthEffect.HealthDown));
                    }
                    break;
                case OnPlayerHealthEffect.Glass:
                    _player.SetMaxPlayerHealth(Convert.ToInt32(OnPlayerHealthEffect.Glass));
                    break;
            }
        }
        
        private void CheckDecreaseHealth(Dice dice)
        {
            switch (dice.onPlayerHealthEffect)
            {
                case OnPlayerHealthEffect.None:
                    break;
                case OnPlayerHealthEffect.HealthUp:
                    _player.SetMaxPlayerHealth(_player.maxHealth - Convert.ToInt32(OnPlayerHealthEffect.HealthUp));
                    break;
                case OnPlayerHealthEffect.HealthDown:
                    if (_player.maxHealth + Convert.ToInt32(OnPlayerHealthEffect.HealthDown) <= 0)
                        _player.maxHealth = 1;
                    _player.SetMaxPlayerHealth(_player.maxHealth - Convert.ToInt32(OnPlayerHealthEffect.HealthDown));
                    break;
                case OnPlayerHealthEffect.Glass:
                    _player.SetMaxPlayerHealth(_player.defaultMaxHealth);
                    break;
            }
        }
    }
}
