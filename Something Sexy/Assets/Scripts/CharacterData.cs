using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

[Serializable]
public class AllResponses
{
    public CharacterResponse[] characters;
   
    public static AllResponses CreateFromJson(string json)
    {
        var toReturn = JsonUtility.FromJson<AllResponses>(json);
        
        //Debug.Log(toReturn.characters[5].answers[2]);
        return JsonUtility.FromJson<AllResponses>(json);
    }    
}

[Serializable]
public class CharacterResponse
{
    public string name;
    public string[] answers;
}