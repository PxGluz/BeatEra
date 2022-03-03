using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData
{
    public int[] characterIndex = new int[29];
    public int selectedCharacterIndex;

    public CharacterData (int[] v, int selection)
    {
        for (int i = 0; i <= 28; i++)
            characterIndex[i] = v[i];
        selectedCharacterIndex = selection;
    }
}
