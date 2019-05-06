using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EndingData
{
    public Pairings[] player;
   
    public static EndingData CreateFromJson(string json)
    {
        var toReturn = JsonUtility.FromJson<EndingData>(json);
        
        //Debug.Log(toReturn.player[5].pairing[2]);
        return JsonUtility.FromJson<EndingData>(json);
    }    
}

[Serializable]
public class Pairings
{
    public string name;
    public string[] pairing;
}
