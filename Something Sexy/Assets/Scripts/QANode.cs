using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QANode
{    
    public string question1Text;
    public string question2Text;
    public string question3Text;
    public string answer1Text;
    public string answer2Text;
    public string answer3Text;

    public int roundNumber;
    
    public QANode()
    {
        roundNumber = 0;
        
        question1Text = "This is default question text.";
        question2Text = "This is default question text.";
        question3Text = "This is default question text.";
        
        answer1Text = "This is default answer text.";
        answer2Text = "This is default answer text.";
        answer3Text = "This is default answer text.";
    }

    public QANode(string question1Text, string question2Text, string question3Text)
    {
        this.question1Text = question1Text;
        this.question2Text = question2Text;
        this.question3Text = question3Text;
    }
    
    public QANode(int roundNumber, string question1Text, string question2Text, string question3Text,
        string answer1Text, string answer2Text, string answer3Text)
    {
        this.roundNumber = roundNumber;
        
        this.question1Text = question1Text;
        this.question2Text = question2Text;
        this.question3Text = question3Text;
        
        this.answer1Text = answer1Text;
        this.answer2Text = answer2Text;
        this.answer3Text = answer3Text;
    }
}
