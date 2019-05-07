using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EndingData
{
    public CharacterSet[] player;
   
    public static EndingData CreateFromJson(string json)
    {
        var toReturn = JsonUtility.FromJson<EndingData>(json);
        
        //Debug.Log(toReturn.player[5].pairing[2]);
        return JsonUtility.FromJson<EndingData>(json);
    }    
}

[Serializable]
public class CharacterSet
{
    public string name;
    public CoupleData[] pairing;
}

[Serializable]
public class CoupleData
{
    public string part1;
    public string part2;
    public string part3;
}