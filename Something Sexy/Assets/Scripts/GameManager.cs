using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Image = UnityEngine.UIElements.Image;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public GameObject[] allCharacters;
    private GameObject[] _allContestantPos;
    private GameObject[] _allContestantSelection;

    private GameObject _leftContestant;
    private GameObject _middleContestant;
    private GameObject _rightContestant;

    public GameObject contestant1Select;
    public GameObject contestant2Select;
    public GameObject contestant3Select;
    public GameObject pos1;
    public GameObject pos2;
    public GameObject pos3;
    public GameObject roundText;
    public GameObject winnerSelectText;
    public GameObject resultsButton;
    
    public GameObject endPanel;
    public GameObject endTextObject;
    public GameObject endButton;
    public GameObject restartButton;

    private TextMeshProUGUI _endText;

    public int pos1Index;
    public int pos2Index;
    public int pos3Index; 
    public int contestant1Index;
    public int contestant2Index;
    public int contestant3Index;
    public int winnerIndex;
    private int _endingNum = 1;

    private Vector3 _newPos;
    private Vector3 _newPos2;
    private Vector3 _newPos3;

    public Image Yorick;
    public Image YorickTextBox;
    public GameObject YorickCommentary;
/*
    public Sprite [] CharacterPortraits;
    public GameObject PortraitHolder;    
*/

    #region round property
    
    public int roundNum = 1;
    public int maxRounds = 6;

    public int Rounds
    {
        get { return roundNum; }
        set
        {
            roundNum = value;
            if (roundNum > maxRounds)
            {
                roundNum = maxRounds;
            }
        }
    }    

    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            //DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        #region Initialize all char prefabs

        GameObject antony = Resources.Load<GameObject>("Prefabs/Characters/Antony");
        GameObject beatrice = Resources.Load<GameObject>("Prefabs/Characters/Beatrice");
        GameObject benedick = Resources.Load<GameObject>("Prefabs/Characters/Benedick");
        GameObject cleopatra = Resources.Load<GameObject>("Prefabs/Characters/Cleopatra");
        GameObject hamlet = Resources.Load<GameObject>("Prefabs/Characters/Hamlet");
        GameObject juliet = Resources.Load<GameObject>("Prefabs/Characters/Juliet");
        GameObject kate = Resources.Load<GameObject>("Prefabs/Characters/Kate");
        GameObject ophelia = Resources.Load<GameObject>("Prefabs/Characters/Ophelia");
        GameObject petruchio = Resources.Load<GameObject>("Prefabs/Characters/Petruchio");
        GameObject romeo = Resources.Load<GameObject>("Prefabs/Characters/Romeo");
/*
        Sprite antonyPortrait = Resources.Load<Sprite>("Prefabs/Icons/antony icon");
        Sprite beatricePortrait = Resources.Load<Sprite>("Prefabs/Icons/beatrice icon");
        Sprite benedickPortrait = Resources.Load<Sprite>("Prefabs/Icons/benedick icon");
        Sprite cleopatraPortrait = Resources.Load<Sprite>("Prefabs/Icons/cleo icon");
        Sprite hamletPortrait = Resources.Load<Sprite>("Prefabs/Icons/hamlet icon");
        Sprite julietPortrait = Resources.Load<Sprite>("Prefabs/Icons/juliet icon");
        Sprite katePortrait = Resources.Load<Sprite>("Prefabs/Icons/kate icon");
        Sprite opheliaPortrait = Resources.Load<Sprite>("Prefabs/Icons/ophelia icon");
        Sprite petruchioPortrait = Resources.Load<Sprite>("Prefabs/Icons/petruchio icon");
        Sprite romeoPortrait = Resources.Load<Sprite>("Prefabs/Icons/romeo icon");
*/

        #endregion

        #region populate our arrays
        
        //plug all our character game objects into an array
        allCharacters = new[] {antony, beatrice, benedick, cleopatra, hamlet, juliet, kate, ophelia, petruchio, romeo};

        //CharacterPortraits = new[] {antonyPortrait, beatricePortrait, cleopatraPortrait, hamletPortrait, julietPortrait, katePortrait, opheliaPortrait, petruchioPortrait, romeoPortrait};

        //plug all of our empty game objects into an array
        _allContestantPos = new[] {pos1, pos2, pos3};
        
        //plug all of our char selection buttons into an array
        _allContestantSelection = new []{contestant1Select, contestant2Select, contestant3Select};
        
        #endregion

        #region position contestant selection UI
        
        _newPos = Camera.main.WorldToScreenPoint(pos1.transform.position); //convert world position of char game objects to screen position
        _newPos2 = Camera.main.WorldToScreenPoint(pos2.transform.position); //repeat
        _newPos3 = Camera.main.WorldToScreenPoint(pos3.transform.position); //repeat
        
        contestant1Select.transform.position = _newPos; //move character selection button to new screen position 
        contestant2Select.transform.position = _newPos2; //repeat
        contestant3Select.transform.position = _newPos3; //repeat
        
        #endregion
        
        FindPlayersPartner(); //Find the canonical match of the player's selected character
        FindContestants(); //Find two other random contestants
        ShuffleContestants(); //Mix up the order in which they appear

        _leftContestant = instance.pos1.transform.GetChild(0).gameObject;
        _middleContestant = instance.pos2.transform.GetChild(0).gameObject;
        _rightContestant = instance.pos3.transform.GetChild(0).gameObject;

        //PortraitHolder.GetComponent<SpriteRenderer>().sprite = CharacterPortraits[CharacterSelection.instance.playerIndex];
       

    }

    void Update()
    {
        UpdateRounds();
    }

    void FindPlayersPartner() //find contestant 1's index numbers
    {
        switch (CharacterSelection.instance.playerIndex)
        {
            case 0: //Antony
                contestant1Index = 3; //partner is Cleopatra
                break;
            case 1: //Beatrice
                contestant1Index = 2; //partner is Benedick
                break;
            case 2: //Benedick
                contestant1Index = 1; //partner is Beatrice
                break;
            case 3: //Cleopatra
                contestant1Index = 0; //partner is Antony
                break;
            case 4: //Hamlet
                contestant1Index = 7; //partner is Ophelia
                break;
            case 5: //Juliet
                contestant1Index = 9; //partner is Romeo
                break;
            case 6: //Kate
                contestant1Index = 8; //partner is Petruchio
                break;
            case 7: //Ophelia
                contestant1Index = 4; //partner is Hamlet
                break;
            case 8: //Petruchio
                contestant1Index = 6; //partner is Kate
                break;
            case 9: //Romeo
                contestant1Index = 5; //partner is Juliet
                break;
            default:
                break;
        }
    }
    
    void FindContestants() //finds contestant 2 and 3's index numbers
    {        
        contestant2Index = Random.Range(0, allCharacters.Length); //contestant 2's index number is a random number between 0 and 9 A.K.A. our characters index numbers in the char array
        contestant3Index = Random.Range(0, allCharacters.Length); //repeat for contestant 3

        if (contestant2Index == contestant1Index || //if the index of contestant 2 is equal to contestant 1's number
            contestant3Index == contestant1Index || //or if the index of contestant 3 is equal to contestant 1's number
            contestant2Index == contestant3Index || //or if the index of contest 2 or 3 are equal to each other
            contestant2Index == CharacterSelection.instance.playerIndex || //or if the index of 2 is equal to the player char index
            contestant3Index == CharacterSelection.instance.playerIndex) //or if the index of 3 is equal to the player char index
        {
            FindContestants(); //start this function over A.K.A. re-randomize our character index numbers until there are no repeats
        }
    }

    void ShuffleContestants() //instantiates the contestants and shuffle them
    {
        pos1Index = Random.Range(0, _allContestantPos.Length); //position 1's index number is a random number between 0 and 2 A.K.A. the length of our positions array
        pos2Index = Random.Range(0, _allContestantPos.Length); //repeat for position 2
        pos3Index = Random.Range(0, _allContestantPos.Length); //repeat for position 3

        if (pos1Index == pos2Index || pos2Index == pos3Index || pos3Index == pos1Index) //if any position index numbers are equal to each other
        {
            ShuffleContestants(); //start this function over
        }
        else //otherwise
        {
            GameObject contestant1 = Instantiate(allCharacters[contestant1Index], //make an object from the array of prefabs based on contestant 1's index
                _allContestantPos[pos1Index].transform.position, Quaternion.identity); //position the object at one of the positions in the position array, according to its random pos index
            GameObject contestant2 = Instantiate(allCharacters[contestant2Index], //repeat for contestant2
                _allContestantPos[pos2Index].transform.position, Quaternion.identity);
            GameObject contestant3 = Instantiate(allCharacters[contestant3Index], //repeat for contestant3
                _allContestantPos[pos3Index].transform.position, Quaternion.identity);

            contestant1.transform.SetParent(_allContestantPos[pos1Index].transform); //parent contestant 1 to the empty transform where it's located
            contestant2.transform.SetParent(_allContestantPos[pos2Index].transform); //repeat for contestant 2
            contestant3.transform.SetParent(_allContestantPos[pos3Index].transform); //repeat for contestant 3

            contestant1.AddComponent<AssignIndex>().fixedCharIndex = contestant1Index; //assign character index as a public variable to the contestant1 game object
            contestant2.AddComponent<AssignIndex>().fixedCharIndex = contestant2Index; //repeat for contestant 2
            contestant3.AddComponent<AssignIndex>().fixedCharIndex = contestant3Index; //repeat for contestant 3

            contestant1.GetComponent<SpriteRenderer>().enabled = false;
            contestant2.GetComponent<SpriteRenderer>().enabled = false;
            contestant3.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public class AssignIndex : MonoBehaviour //this a class just for assigning the char index as a public variable to our contestant game objects
    {
        public int fixedCharIndex;
    }

    void UpdateRounds() //update the round number
    {
        roundText.GetComponent<Text>().text = "Round: " + Rounds; //set round text
        
        if (roundNum == maxRounds) //if played rounds is equal to maximum number of rounds allowed
        {
            roundText.SetActive(false); //deactivate the round text
            winnerSelectText.SetActive(true); //activate the winner selection text
            
            QAManager.instance.question1Button.SetActive(false); //deactivate the question button
            QAManager.instance.question2Button.SetActive(false); //repeat
            QAManager.instance.question3Button.SetActive(false); //repeat

            foreach (var selectionButton in _allContestantSelection) //for every selectionButton in our array of selection buttons
            {
                selectionButton.GetComponent<Button>().interactable = true; //set them to be interactable
            }
        }   
    } 
    
    public void WinnerSelect(int posIndex) //player selects the winning contestant
    {
        switch (posIndex)
        {
            case 1: //if the player selected the contestant parented to position 1
                winnerIndex = _leftContestant.GetComponent<GameManager.AssignIndex>().fixedCharIndex; //set winner index equal to the character index of the character in position 1
                _leftContestant.GetComponent<SpriteRenderer>().enabled = true; //activate the sprite of the contestant in position 1
                break;
            case 2:
                winnerIndex = _middleContestant.GetComponent<GameManager.AssignIndex>().fixedCharIndex; //set winner index equal to the character index of the character in position 2
                _middleContestant.GetComponent<SpriteRenderer>().enabled = true; //activate the sprite of the contestant in position 2
                break;
            case 3:
                winnerIndex = _rightContestant.GetComponent<GameManager.AssignIndex>().fixedCharIndex; //set winner index equal to the character index of the character in position 3
                _rightContestant.GetComponent<SpriteRenderer>().enabled = true; //activate the sprite of the contestant in position 3
                break;
            default:
                break;
        }
        
        contestant1Select.SetActive(false); //set contestant 2 selection button to be inactive
        contestant2Select.SetActive(false); //repeat
        contestant3Select.SetActive(false); //repeat
        winnerSelectText.GetComponent<Text>().enabled = false; //set winner select text to be inactive
        resultsButton.SetActive(true); //set results button to be active

        //Debug.Log("contestant index: " + winnerIndex);

    }

    public void ShowResults() //activates all ending UI and first part of ending text
    {
        _endText = endTextObject.GetComponent<TextMeshProUGUI>(); //getting reference to TextMeshProUGUI component
        
        endTextObject.SetActive(true); //set ending UI game object to be active
        endPanel.SetActive(true); //etc.
        endButton.SetActive(true); //etc,
        _endText.DOColor(Color.black, 1f); //etc.
        
        _endText.text = QAManager.instance.endingData.player[CharacterSelection.instance.playerIndex].pairing[winnerIndex].part1;
        //set ending text according to the index of the player character, the index of the selected winning contestant and the first part of the text
        
        //Debug.Log(QAManager.instance.endingData.player[CharacterSelection.instance.playerIndex].pairing[winnerIndex].part1);
        //Debug.Log(QAManager.instance.endingData.player[CharacterSelection.instance.playerIndex].pairing[winnerIndex].part2);
        //Debug.Log(QAManager.instance.endingData.player[CharacterSelection.instance.playerIndex].pairing[winnerIndex].part3);
    }

    public void CallNextPart() //calls the second and third parts of ending when the player clicks the arrow button
    {
        _endingNum++; //increase ending number

        switch (_endingNum)
        {
            case 2: //if ending number is 2, show part 2 of the ending
                _endText.text = QAManager.instance.endingData.player[CharacterSelection.instance.playerIndex].pairing[winnerIndex].part2;
                break;
            case 3: //if ending number is 3, show part 3 of the ending
                _endText.text = QAManager.instance.endingData.player[CharacterSelection.instance.playerIndex].pairing[winnerIndex].part3;
                endTextObject.transform.GetChild(0).gameObject.SetActive(false);
                restartButton.SetActive(true);
                break;
            default: //else
                _endingNum = 1; //reset ending number to 1
                break;
        }
    }

    public void Restart()
    {
        Destroy(GameManager.instance);
        Destroy(QAManager.instance);
        Destroy(CharacterSelection.instance);
        
        SceneManager.LoadScene(2);
    }
}
