using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class CharacterSystem
{
    public static void CharacterSave(int[] v, int selection)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/characters.be";
        FileStream stream = new FileStream(path, FileMode.Create);
        CharacterData data = new CharacterData(v, selection);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static CharacterData CharacterLoad()
    {
        string path = Application.persistentDataPath + "/characters.be";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            CharacterData data = formatter.Deserialize(stream) as CharacterData;
            stream.Close();
            return data;
        }
        else
            return null;
    }
}
