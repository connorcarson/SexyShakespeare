using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public Object[] characters;
    public Vector3[] contestantPos;

    public int contestant1Index;
    public int contestant2Index;
    public int contestant3Index;

    //public Transform pos1;
    //public Transform pos2;
    //public Transform pos3;
    //public Transform[] contestantPositions;

    //public Vector3 contestantPos1;
    //public Vector3 contestantPos2;
    //public Vector3 contestantPos3;

    public int pos1Index;
    public int pos2Index;
    public int pos3Index;
    
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
        
        characters = Resources.LoadAll("Prefabs/Characters") as Object[];

        /*var antony = Resources.Load<GameObject>("Prefabs/Characters/Antony");
        var beatrice = Resources.Load<GameObject>("PrePrefabs/Characters/Beatrice");
        var benedick = Resources.Load<GameObject>("PrePrefabs/Characters/Benedick");
        var cleopatra = Resources.Load<GameObject>("PrePrefabs/Characters/Cleopatra");
        var hamlet = Resources.Load<GameObject>("PrePrefabs/Characters/Hamlet");
        var juliet = Resources.Load<GameObject>("PrePrefabs/Characters/Juliet");
        var kate = Resources.Load<GameObject>("PrePrefabs/Characters/Kate");
        var ophelia = Resources.Load<GameObject>("PrePrefabs/Characters/Ophelia");
        var petruchio = Resources.Load<GameObject>("Prefabs/Characters/Petruchio");
        var romeo = Resources.Load<GameObject>("PrePrefabs/Characters/Romeo");

        characters = new[] {antony, beatrice, benedick, cleopatra, hamlet, juliet, kate, ophelia, petruchio, romeo};*/

        //contestantPositions = new[] {pos2, pos2, pos3};
        
        FindPlayersPartner();
        FindContestants();
        ShuffleContestants();
        CreateContestants();
        
        print(characters[4].name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FindPlayersPartner()
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
    
    void FindContestants()
    {        
        contestant2Index = Random.Range(0, characters.Length);
        contestant3Index = Random.Range(0, characters.Length);

        if (contestant2Index == contestant1Index || 
            contestant3Index == contestant1Index || 
            contestant2Index == contestant3Index ||
            contestant2Index == CharacterSelection.instance.playerIndex ||
            contestant3Index == CharacterSelection.instance.playerIndex)
        {
            FindContestants();
        }
    }

    void ShuffleContestants()
    {
        pos1Index = Random.Range(0, contestantPos.Length);
        pos2Index = Random.Range(0, contestantPos.Length);
        pos3Index = Random.Range(0, contestantPos.Length);

        if (pos1Index == pos2Index || pos2Index == pos3Index || pos3Index == pos1Index)
        {
            ShuffleContestants();
        }
    }

    void CreateContestants()
    {
        Instantiate(characters[contestant1Index], contestantPos[pos1Index], Quaternion.identity);
        Instantiate(characters[contestant2Index], contestantPos[pos2Index], Quaternion.identity);
        Instantiate(characters[contestant3Index], contestantPos[pos3Index], Quaternion.identity);
    }
}
