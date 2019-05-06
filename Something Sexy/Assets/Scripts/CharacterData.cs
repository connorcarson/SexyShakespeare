using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

[Serializable]
public class CharacterData
{
    public CharacterResponse[] characters;
   
    public static CharacterData CreateFromJson(string json)
    {
        var toReturn = JsonUtility.FromJson<CharacterData>(json);
        
        //Debug.Log(toReturn.characters[5].answers[2]);
        return JsonUtility.FromJson<CharacterData>(json);
    }    
}

[Serializable]
public class CharacterResponse
{
    public string name;
    public List<string> answers;
}