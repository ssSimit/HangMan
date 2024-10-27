using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject blankPrefab;
    public Transform blankParent;

    public TextMeshProUGUI hintTxt;
    public TextMeshProUGUI countTxt;

    public string currentWord;
    int letterCount;
    int correctCount;
    private int mistakeNO=0;
    public bool playingGame=false;
    private GameObject currentButtonGO;
    private List<GameObject> pressedButtonsGO=new List<GameObject>();
    private List<char> pressedLetters=new List<char>();

    public WordManager wm;
    public UIManager um;

    public void getWord()
    {
        string[] values =  wm.GetRandomWord();
        string word;
        string category;
        if (values.Length != 2 || string.IsNullOrEmpty(values[0]) || string.IsNullOrEmpty(values[1]))
        {
            Debug.Log("not valid values");
            return;
        }
        word = values[0];
        category = values[1];
        hintTxt.text = "("+category+")";
        currentWord = word;
        Debug.Log(word);

        int childCount = 0;

        foreach(Transform child in blankParent)
        {
            if (childCount < 2)
            {
                childCount++;
                continue;
            }
            Destroy(child.gameObject);
        }
        letterCount = 0;
        correctCount = 0;
        foreach(char letters in word)
        {
            GameObject blanks=Instantiate(blankPrefab, blankParent);
            letterCount++;
            if(letters ==' ')
            {
                blanks.GetComponent<TMP_Text>().text = " ";
                letterCount--;
            }
        }
        countTxt.text = "letters count: " + letterCount;
        playingGame =true;
    }

    public void OnLetterClicked(GameObject pressedGO)
    {
        if (!playingGame)
        {
            return;
        }
        currentButtonGO =pressedGO;
       currentButtonGO.GetComponent<Button>().interactable= false;
       pressedButtonsGO.Add(currentButtonGO);
    }

    public void CheckGuess(string s)
    {
        if (!playingGame)
        {
            return;
        }
            
        char letter = s[0];
        pressedLetters.Add(letter);
        for(int i = 0;i<currentWord.Length; i++)
        {
            if (currentWord[i] == letter || currentWord[i]==char.ToUpper(letter))
            {
                Transform blank=blankParent.GetChild(i+2);
                blank.GetComponent<TextMeshProUGUI>().text= letter.ToString();
                correctCount++;
                if (correctCount == letterCount)
                {
                    playingGame=false;
                    um.gameWon();
                }
            }
        }

        if (!currentWord.Contains(letter.ToString()) && !currentWord.Contains((letter.ToString()).ToUpper()))
        {
            TextMeshProUGUI wrongTxt = currentButtonGO.GetComponent<TextMeshProUGUI>();
            wrongTxt.text = "×";
            wrongTxt.color = Color.red;
            um.mistake(mistakeNO);
            mistakeNO++;
        }
    }

    public void PlayAgain(bool again)
    {
        int index = 0;
        foreach(GameObject go in pressedButtonsGO)
        {
            go.GetComponent<Button>().interactable = true;
            go.GetComponent<TextMeshProUGUI>().text= pressedLetters[index].ToString();
            go.GetComponent<TextMeshProUGUI>().color = Color.black;
            index++;
        }
        pressedButtonsGO.Clear();
        pressedLetters.Clear();
        mistakeNO = 0;
        um.resetBodyParts();
        if (again)
        {
            getWord();
            playingGame = true;
        }
        else
        {
            playingGame= false;
            um.home();
        }
       
    }
}
