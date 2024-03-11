using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


    namespace Enemies
    {
        public enum EnemiesID
        {
            SimpleFly = 0,
            GreenFly = 1,
            BabyFly = 2,
            FlyingFly = 3,
            PoopFly = 4,
            PoopTinyFly = 5,
            PoopHeadFly = 6,
            BombFly = 7,
            ElectroFly = 8,
            GasMaskFly = 9,
            FakeFly = 10
        }
        public class EnemiesSpawner : MonoBehaviour
        {
            public List<Enemy> enemiesPool;
        
            public List<Enemy> enemiesAvailable;
        
            public List<Enemy> comingEnemies;

            public List<Enemy> enemiesOnScene;
        
            public Transform[] spawnPoint; //Spawn points
            public Transform[] targetPoint; //Target points

            public int levelEnemiesPoints;

            public int spawnInterval; //In turns

            public int spawnStart;
        
            private bool _enemiesAreHighlighted = false;

            //private bool _spawnedEnemy = false;

            public static EnemiesSpawner instanceES;

            private GameManager _gameManager;
        
            EnemiesSpawner()
            {
                instanceES = this;
            }

            private void Awake()
            {
                _gameManager = GameManager.instanceGm;
            }


            public void AddEnemies(int maxId)
            {
                if (maxId >= enemiesPool.Count) maxId = enemiesPool.Count - 1;
                for (int i = 0; i < maxId; i++)
                {
                    enemiesAvailable.Add(enemiesPool[i]);
                }
                GenerateLevelEnemiesList();
            }

            public void SpawningEnemies(int currentTurn)
            {
                if (currentTurn < spawnStart) return;
                if(comingEnemies.Count <= 0) return;
                if (currentTurn % spawnInterval == 0)
                {
                    int newEnemiesAmount = Random.Range(1, 4);
                    //_spawnedEnemy = false;
                    if (newEnemiesAmount > comingEnemies.Count) newEnemiesAmount = comingEnemies.Count;
                    for (int i = 0; i < newEnemiesAmount; ++i)
                    {
                        int spawnCellNumber = Random.Range(0, spawnPoint.Length);
                    
                        int enemyPrefabNumber = Random.Range(0, comingEnemies.Count);

                        Enemy enemy = Instantiate(comingEnemies[enemyPrefabNumber],
                            spawnPoint[spawnCellNumber].position, Quaternion.identity);
                        
                        enemiesOnScene.Add(enemy);
                        enemy.targetTransform = targetPoint[spawnCellNumber].transform;
                    
                        comingEnemies.Remove(comingEnemies[enemyPrefabNumber]);
                        //_spawnedEnemy = true;
                    }
                }
            }

            public void SpawnEnemy(Transform spawnTransform, int enemyPrefabNumber, int targetCell)
            {
                Enemy enemy = Instantiate(enemiesPool[enemyPrefabNumber],
                    spawnTransform.position, Quaternion.identity);
                        
                enemiesOnScene.Add(enemy);
                enemy.targetTransform = targetPoint[targetCell].transform;
            }
            
        
            private void GenerateLevelEnemiesList()
            {
                int min = GetMinimumEnemyCost();
                while (levelEnemiesPoints > 0 && levelEnemiesPoints - min >= 0)
                { 
                    int randomEnemyId = Random.Range(0, enemiesAvailable.Count);
                    int randomEnemyCost = enemiesAvailable[randomEnemyId].enemyCost;

                    if (levelEnemiesPoints - randomEnemyCost >= 0)
                    {
                        comingEnemies.Add(enemiesAvailable[randomEnemyId]);
                        levelEnemiesPoints -= randomEnemyCost;
                    } else if (levelEnemiesPoints <= 0)
                    {
                        break;
                    }
                }
            }

            private int GetMinimumEnemyCost()
            {
                int min = 0;
                foreach (var elem in enemiesAvailable)
                {
                    if (min == 0) min = elem.enemyCost;
                    if(elem.enemyCost < min) min = elem.enemyCost;
                }
                return min;
            }

            public void HighlightEnemies(bool isActive)
            {
                if(isActive && _enemiesAreHighlighted) return;
                foreach (var elem in enemiesOnScene)
                {
                    elem.enemyHighlight.SetActive(isActive);
                }
                _enemiesAreHighlighted = isActive;
            }

            public void RemoveEnemy(Enemy enemy)
            {
                enemiesOnScene.Remove(enemy);
                if (comingEnemies.Count == 0 && enemiesOnScene.Count == 0)
                {
                    _gameManager.Winning();
                }
            }
        
        
            public void EnemiesClickable(bool isClickable)
            {
                if (enemiesOnScene != null)
                { 
                    foreach (var enemy in enemiesOnScene) 
                    {
                        enemy.isClickable = isClickable; 
                    }
                }
        
            }

        }
    }

