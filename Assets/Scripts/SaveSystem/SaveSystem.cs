using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace SaveSystem
{
    public static class SaveSystem
    {
        public static void SavePlayerData(int playerHP,int maxPlayerHP, int[] currentDicesIDs)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/playerData.rotd";
            FileStream stream = new FileStream(path, FileMode.Create);
        
            PlayerData data = new PlayerData(playerHP, maxPlayerHP, currentDicesIDs);
            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static PlayerData LoadPlayerData()
        {
            string path = Application.persistentDataPath + "/playerData.rotd";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                PlayerData data = formatter.Deserialize(stream) as PlayerData;
                stream.Close();
            
                return data;
            }
            else
            {
                Debug.Log("No data found!");
                return null;
            }
        }

        public static void DeleteAllData()
        {
            string playerPath = Application.persistentDataPath + "/playerData.rotd";
            string levelPath = Application.persistentDataPath + "/levelData.rotd";
            if (File.Exists(levelPath))
            {
                File.Delete(levelPath);
            }
            if (File.Exists(playerPath))
            {
                File.Delete(playerPath);
            }
        }
        
        public static void SaveLevelData( int[] newEnemiesIds,int newCurrentLevel, int newLevelEnemiesPoints)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/levelData.rotd";
            FileStream stream = new FileStream(path, FileMode.Create);
        
            LevelData data = new LevelData(newEnemiesIds,newCurrentLevel,newLevelEnemiesPoints);
            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static LevelData LoadLevelData()
        {
            string path = Application.persistentDataPath + "/levelData.rotd";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                LevelData data = formatter.Deserialize(stream) as LevelData;
                stream.Close();
            
                return data;
            }
            else
            {
                Debug.Log("No level data found!");
                return null;
            }
        }
        
        public static void SaveDiceData(bool[] newDiceInfo)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/dicesData.rotd";
            FileStream stream = new FileStream(path, FileMode.Create);
        
            DicesData data = new DicesData(newDiceInfo);
            
            formatter.Serialize(stream, data);
            stream.Close();
        }
        
        public static DicesData LoadDiceData()
        {
            string path = Application.persistentDataPath + "/dicesData.rotd";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                DicesData data = formatter.Deserialize(stream) as DicesData;
                stream.Close();
            
                return data;
            }
            else
            {
                Debug.Log("No dices data found!");
                return null;
            }
        }
    }
}
