using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuestionData
{
    public QuestionPool questionPool;

    public static QuestionData CreatFromJson(string json)
    {
        var toReturn = JsonUtility.FromJson<QuestionData>(json);
        
        Debug.Log(toReturn.questionPool.questions[4]);
        return JsonUtility.FromJson<QuestionData>(json);
    }
}

[Serializable]
public class QuestionPool
{
    public string[] questions;
}

