using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Data_Management : MonoBehaviour
{
    public static Data_Management datamanagement;

    public bool[] Levels;

    void Awake()
    {
        if (datamanagement == null)
        {
            DontDestroyOnLoad(gameObject);
            datamanagement = this;
        }

        //if datamanagement already exists then destroy it
        else if (datamanagement != this)
        {
            Destroy(gameObject);
        }
    }

    public void SaveData()
    {
        BinaryFormatter BinForm = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gameInfo.dat");

        //creates container for data
        gameData data = new gameData();

        data.Levels = Levels;
        //serializes
        BinForm.Serialize(file, data);
        file.Close();
    }

    public void LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/gameInfo.dat"))
        {
            BinaryFormatter BinForm = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gameInfo.dat", FileMode.Open);

            //decrypts binary
            gameData data = (gameData)BinForm.Deserialize(file);
            file.Close();

            Levels = data.Levels;
        }
    }
}

[Serializable]
class gameData
{
    public bool[] Levels;
}
