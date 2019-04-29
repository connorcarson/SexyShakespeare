using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AllEndings
{
    public Pairings[] player;
   
    public static AllEndings CreateFromJson(string json)
    {
        var toReturn = JsonUtility.FromJson<AllEndings>(json);
        
        //Debug.Log(toReturn.player[5].pairing[2]);
        return JsonUtility.FromJson<AllEndings>(json);
    }    
}

[Serializable]
public class Pairings
{
    public string name;
    public string[] pairing;
}
