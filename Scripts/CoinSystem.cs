using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class CoinSystem
{
    public static void SaveCoins(int totalCoins)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/coins.be";
        FileStream stream = new FileStream(path, FileMode.Create);
        CoinData data = new CoinData(totalCoins);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static CoinData LoadCoins()
    {
        string path = Application.persistentDataPath + "/coins.be";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            CoinData data = formatter.Deserialize(stream) as CoinData;
            stream.Close();
            return data;
        }
        else
            return null;
    }
}
