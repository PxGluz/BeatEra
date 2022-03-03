using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class ScoreSystem
{
    public static void ScoreSave (Manager manager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/score.be";
        FileStream stream = new FileStream(path, FileMode.Create);
        ScoreData data = new ScoreData(manager);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static ScoreData ScoreLoad ()
    {
        string path = Application.persistentDataPath + "/score.be";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            ScoreData data = formatter.Deserialize(stream) as ScoreData;
            stream.Close();
            return data;
        }
        else
            return null;
    }
}
