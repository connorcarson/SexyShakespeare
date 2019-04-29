﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class QAManager : MonoBehaviour
{
    public static QAManager instance;
    
    private string _questionJson;
    private string _answerJson;
    private string _endingJson;

    public string[] questionPool;
    public string[] answerPool;

    public Text question1Text;
    public Text question2Text;
    public Text question3Text;
    public Text answerText;
    //public Text roundText;

    public GameObject question1Button;
    public GameObject question2Button;
    public GameObject question3Button;

    public GameObject answerDialogue;
    public GameObject nextButton;
    public GameObject textTail1;
    public GameObject textTail2;
    public GameObject textTail3;

    //public int roundNum = 1;
    //public int maxRounds = 5;
    public int questionIndex;
    public int answerIndex;
    public int question1Index;
    public int question2Index;
    public int question3Index;
    public int contestantTurn = 1;
    public int charIndex;

    public QuestionData questions;
    public AllResponses characterData;
    public AllEndings endingData;
    
    // Start is called before the first frame update
    void Start()
    {   
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        _questionJson = Application.dataPath + "/Text/AllQuestions.json";
        string allQuestionText = File.ReadAllText(_questionJson);
        questions = QuestionData.CreatFromJson(allQuestionText);

        questionPool = questions.questionPool.questions;

        _answerJson = Application.dataPath + "/Text/AllAnswers.json";
        string allAnswerText = File.ReadAllText(_answerJson);
        characterData = AllResponses.CreateFromJson(allAnswerText);

        _endingJson = Application.dataPath + "/Text/Endings.json";
        string allEndingText = File.ReadAllText(_endingJson);
        endingData = AllEndings.CreateFromJson(allEndingText);
        
        UpdateUI();

        //WriteNewJson(fileLocation); //use to creat new Json file, if file does not already exist.
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*void WriteNewJson(string fileLocation)
    {
        currentNode = new QANode(
            1, 
            "this will be question 1",
            "this will be question 2",
            "this will be question 3",
            "this will be answer 1",
            "this will be answer 2",
            "this will be answer 3");

        string JsonStr = JsonUtility.ToJson(currentNode, true);
        
        File.WriteAllText(fileLocation, JsonStr);
    }*/


    void UpdateUI()
    {
        //roundText.text = "Round " + roundNum;
        question1Index = Random.Range(0, questionPool.Length);        
        question2Index = Random.Range(0, questionPool.Length);
        question3Index = Random.Range(0, questionPool.Length);
        
        if (question1Index == question2Index || question1Index == question3Index || question2Index == question3Index)
        {
            UpdateUI();
        }
        else
        {
            string currentQuestion1 = questionPool[question1Index];
            string currentQuestion2 = questionPool[question2Index];
            string currentQuestion3 = questionPool[question3Index];
        
            question1Text.text = currentQuestion1;
            question2Text.text = currentQuestion2;
            question3Text.text = currentQuestion3;
        }
    }
    
    public void ChooseQuestion(int questionNum)
    {
        question1Button.GetComponent<Button>().interactable = false;
        question2Button.GetComponent<Button>().interactable = false;
        question3Button.GetComponent<Button>().interactable = false;
        
        answerDialogue.SetActive(true);
        nextButton.SetActive(true);
        textTail1.SetActive(true);

        switch (questionNum)
        {
            case 1:
                questionIndex = question1Index;
                break;
            case 2:
                questionIndex = question2Index;
                break;
            case 3:
                questionIndex = question3Index;
                break;
            default:
                break;
        }

        answerIndex = questionIndex;
        charIndex = GameManager.instance.pos1.GetComponentInChildren<GameManager.AssignIndex>().fixedCharIndex;
        answerPool = characterData.characters[charIndex].answers;
        answerText.text = answerPool[answerIndex];
    }

    public void Next()
    {
        contestantTurn++;

        switch (contestantTurn)
        {
            case 2:
                charIndex = GameManager.instance.pos2.GetComponentInChildren<GameManager.AssignIndex>().fixedCharIndex;
                answerPool = characterData.characters[charIndex].answers;
                answerText.text = answerPool[answerIndex];
                textTail1.SetActive(false);
                textTail2.SetActive(true);
                break;
            case 3:
                charIndex = GameManager.instance.pos3.GetComponentInChildren<GameManager.AssignIndex>().fixedCharIndex;
                answerPool = characterData.characters[charIndex].answers;
                answerText.text = answerPool[answerIndex];
                textTail2.SetActive(false);
                textTail3.SetActive(true);
                break;
            case 4:
                question1Button.GetComponent<Button>().interactable = true;
                question2Button.GetComponent<Button>().interactable = true;
                question3Button.GetComponent<Button>().interactable = true;
            
                nextButton.SetActive(false);
                answerDialogue.SetActive(false);
                textTail3.SetActive(false);

                GameManager.instance.Rounds++;
            
                UpdateUI();
            
                contestantTurn = 1;
                break;
            default:
                break;
        }
        
        //Debug.Log(contestantTurn);
    }
}
