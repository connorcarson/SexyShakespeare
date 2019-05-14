using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using DG.Tweening.Core;

public class QAManager : MonoBehaviour
{
    public static QAManager instance;
    
    private string _questionJson;
    private string _answerJson;
    private string _endingJson;
    private string _currentAnswer1;
    private string _currentAnswer2;
    private string _currentAnswer3;

    public List<string> questionPool;
    public List<string> answerPool;

    public Text question1Text;
    public Text question2Text;
    public Text question3Text;
    
    public Text answerText;

    public GameObject question1Button;
    public GameObject question2Button;
    public GameObject question3Button;
    public GameObject answerDialogue;
    public GameObject nextButton;
    public GameObject textTail1;
    public GameObject textTail2;
    public GameObject textTail3;

    public int answerIndex;
    public int question1Index;
    public int question2Index;
    public int question3Index;
    public int contestantTurn = 1;
    public int charIndex;

    public QuestionData questionData;
    public CharacterData characterData;
    public EndingData endingData;

    //info for Yorick and transitional text
    public GameObject Yorick;
    public GameObject YorickDialogueBox;
    public GameObject YorickPanel;
    //public Image Yorick;
    public Text transitionalWriting;
    public Image textBox;
    public Image textBoxTail;
    public List<string> postQuestionRemark;
    public List<string> postResponseRemark;
    public List<string> preSelectionRemark;

    private int YorickResponseIndex;
    
    private void Awake()
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

        #region populate all arrays/lists using our .json files

        _questionJson = Application.dataPath + "/Text/AllQuestions.json"; //find location of AllQuestions.json
        string allQuestionText = File.ReadAllText(_questionJson); //read all the text in that .json file
        questionData = QuestionData.CreatFromJson(allQuestionText); //convert text in that .json file into something that Unity can read
        //(in this case, an object containing a List of strings)

        questionPool = questionData.questionPool.questions; //initialize our List variable as the List of strings in our .json file

        _answerJson = Application.dataPath + "/Text/AllAnswers.json"; //find location of AllAnswers.json
        string allAnswerText = File.ReadAllText(_answerJson); //read all the text in that .json file
        characterData = CharacterData.CreateFromJson(allAnswerText); //convert text in that .json file into something that Unity can read
        //(in this case, an array of objects, each containing a string name, and a List of string answers)

        _endingJson = Application.dataPath + "/Text/AllEndings.json"; //find location of AllEndings.json
        string allEndingText = File.ReadAllText(_endingJson); //read all the text in that .json file
        endingData = EndingData.CreateFromJson(allEndingText); //convert text in that .json file into something that Unity can read
        //(in this case, an array of objects, each containing a string name, and an array of string parings)

        #endregion
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Shuffle(); //shuffle the questions
        UpdateUI(); //display selected questions
        
        //WriteNewJson(fileLocation); //use to creat new Json file, if file does not already exist.
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

    void Shuffle() //shuffles index numbers used to randomly select questions
    {
        question1Index = Random.Range(0, questionPool.Count); //randomly select an index number for question 1
        question2Index = Random.Range(0, questionPool.Count); //randomly select an index number for question 2
        question3Index = Random.Range(0, questionPool.Count); //randomly select an index number for question 3

        if (question1Index == question2Index || question2Index == question3Index || question3Index == question1Index)
        //if the index numbers match (there are repeats)
        {
            Shuffle(); //shuffle again
        }
        else //exit this function
        {
            return;
        }
    }
    
    void UpdateUI() //displays randomly selected questions on our UI
    {
        string currentQuestion1 = questionPool[question1Index]; //initialize question 1 string using randomly selected index
        string currentQuestion2 = questionPool[question2Index]; //repeat for question 2
        string currentQuestion3 = questionPool[question3Index]; //repeat for question 3
        
        question1Text.text = currentQuestion1; //set question 1 text to question 1 string
        question2Text.text = currentQuestion2; //repeat for question 2 text
        question3Text.text = currentQuestion3; //repeat for question 3 text

        questionPool.Remove(currentQuestion1); //remove question 1 string from List of strings
        questionPool.Remove(currentQuestion2); //repeat for question 2
        questionPool.Remove(currentQuestion3); //repeat for question 2
    }
    
    public void ChooseQuestion(int questionNum) //player selects the question they wish to ask
    {
        Debug.Log("Hello I am working");
        question1Button.GetComponent<Button>().interactable = false; //make question 1 button nonactive
        question2Button.GetComponent<Button>().interactable = false; //repeat for question 2 button
        question3Button.GetComponent<Button>().interactable = false; //repeat for question 3 button
        
        charIndex = GameManager.instance.pos1.GetComponentInChildren<GameManager.AssignIndex>().fixedCharIndex;
        //initialize charIndex as char index variable assigned to the contestant parented to position 1
        answerPool = characterData.characters[charIndex].answers;
        //initialize answer pool as the list of answers belonging to the character in position 1
        
        switch (questionNum)
        {
            case 1: //if question 1 selected          
                answerText.text = answerPool[question1Index]; //set the answer text to the answer corresponding to question 1
                answerIndex = question1Index; //set answer index to question 1 index
                break;
            case 2: //if question 2 selected             
                answerText.text = answerPool[question2Index]; //set the answer text to the answer corresponding to question 2
                answerIndex = question2Index; //set answer index to question 2 index
                break;
            case 3: //if question 3 selected
                answerText.text = answerPool[question3Index]; //set the answer text to the answer corresponding to question 3
                answerIndex = question3Index; //set answer index to question 3 index
                break;
            default:
                break;
        }
        
        YorickCommentPicker(postQuestionRemark);
        
        _currentAnswer1 = answerPool[question1Index]; //initialize string as the answer corresponding to question 1
        _currentAnswer2 = answerPool[question2Index]; //initialize string as the answer corresponding to question 2
        _currentAnswer3 = answerPool[question3Index]; //initialize string as the answer corresponding to question 3

        //print(_currentAnswer1);
        answerPool.Remove(_currentAnswer1); //remove answer corresponding to question 1 from list of first contestant's answers        
        //print(_currentAnswer2);
        answerPool.Remove(_currentAnswer2); //remove answer corresponding to question 2 from list of first contestant's answers        
        //print(_currentAnswer3);
        answerPool.Remove(_currentAnswer3); //remove answer corresponding to question 3 from list of first contestant's answers 
        
        answerDialogue.SetActive(true); //set answer UI to active
        nextButton.SetActive(true); //etc.
        textTail1.SetActive(true); //etc.
    }

    public void Next() //player presses next button
    {
        contestantTurn++; //increase contestant turn number

        switch (contestantTurn)
        {
            case 2: //if contestant 2 is answering
                charIndex = GameManager.instance.pos2.GetComponentInChildren<GameManager.AssignIndex>().fixedCharIndex;
                //re-initialize charIndex as char index variable assigned to the contestant parented to position 2
                answerPool = characterData.characters[charIndex].answers;
                //re-initialize answer pool as the list of answers belonging to the character in position 2
                
                _currentAnswer1 = answerPool[question1Index]; //re-initialize current answer 1 using second contestant's answerPool
                _currentAnswer2 = answerPool[question2Index]; //re-initialize current answer 1 using second contestant's answerPool
                _currentAnswer3 = answerPool[question3Index]; //re-initialize current answer 1 using second contestant's answerPool
                
                answerText.text = answerPool[answerIndex]; //set the answer text to the answer corresponding to the selected question 
                
                answerPool.Remove(_currentAnswer1); //remove answer corresponding to question 3 from second contestant's answerPool
                answerPool.Remove(_currentAnswer2); //remove answer corresponding to question 3 from second contestant's answerPool
                answerPool.Remove(_currentAnswer3); //remove answer corresponding to question 3 from second contestant's answerPool
                
                textTail1.SetActive(false); //deactivate left tail of speech bubble
                textTail2.SetActive(true); //activate middle tail of speech bubble
                break;
            case 3: //if contestant 3 is answering
                charIndex = GameManager.instance.pos3.GetComponentInChildren<GameManager.AssignIndex>().fixedCharIndex;
                //re-initialize charIndex as char index variable assigned to the contestant parented to position 3
                answerPool = characterData.characters[charIndex].answers;
                //re-initialize answer pool as the list of answers belonging to the character in position 3
                
                _currentAnswer1 = answerPool[question1Index]; //re-initialize current answer 1 using third contestant's answerPool
                _currentAnswer2 = answerPool[question2Index]; //re-initialize current answer 2 using third contestant's answerPool
                _currentAnswer3 = answerPool[question3Index]; //re-initialize current answer 3 using third contestant's answerPool
                
                answerText.text = answerPool[answerIndex]; //set the answer text to the answer corresponding to the selected question 
                
                answerPool.Remove(_currentAnswer1); //remove answer corresponding to question 1 from third contestant's answerPool
                answerPool.Remove(_currentAnswer2); //remove answer corresponding to question 2 from third contestant's answerPool
                answerPool.Remove(_currentAnswer3); //remove answer corresponding to question 3 from third contestant's answerPool
                
                textTail2.SetActive(false); //deactivate middle tail of speech bubble
                textTail3.SetActive(true); //activate right tail of speech bubble
                
                break;
            case 4: //if turn count is more than 3 (all contestants have answered)            
                GameManager.instance.Rounds++; //increase round number

                if (GameManager.instance.roundNum < 6)
                {
                    YorickCommentPicker(postResponseRemark);
                }

                nextButton.SetActive(false); //deactivate answer UI 
                answerDialogue.SetActive(false); //etc.
                textTail3.SetActive(false); //etc.
                
                question1Button.GetComponent<Button>().interactable = true; //set question 1 button to be active
                question2Button.GetComponent<Button>().interactable = true; //repeat for question 2 button
                question3Button.GetComponent<Button>().interactable = true; //repeat for question 3 button
            
                Shuffle(); //shuffle remaining questions
                UpdateUI(); //display selected questions
            
                contestantTurn = 1; //reset contestant turn number
                break;
            default:
                break;
        }
    }

    public void YorickCommentPicker(List<string> CurrentResponsePool)
    {
        Yorick.SetActive(true);
        YorickDialogueBox.SetActive(true);
        YorickPanel.SetActive(true);
        
        // set the index number to a random number between 0 and the length of the array we will select
        YorickResponseIndex = Random.Range(0, CurrentResponsePool.Count);

        string currentResponse = CurrentResponsePool[YorickResponseIndex];
        transitionalWriting.text = currentResponse;
        CurrentResponsePool.Remove(currentResponse);
        
        textBox.DOColor(Color.white, .5f);
        textBoxTail.DOColor(Color.white, .5f);
        transitionalWriting.DOColor(Color.black, .5f);
    }


    public void EndYorick()
    {
        Yorick.SetActive(true);
        YorickDialogueBox.SetActive(true);
        YorickPanel.SetActive(true);

        transitionalWriting.text = "Come, come! Tis time to select the object of thy heart's desire! Who shall ye choose?";
    }
    
    public void DeactivateYorick()
    {
        Yorick.SetActive(false);
        YorickDialogueBox.SetActive(false);
        YorickPanel.SetActive(false);
    }
    
    /*public void FadeOut()
    {
        textBox.DOColor(Color.clear, .5f);
        textBoxTail.DOColor(Color.clear, .5f);
        transitionalWriting.DOColor(Color.clear, .5f);
    }*/
    
}
    
