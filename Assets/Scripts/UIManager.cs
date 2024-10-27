using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameManager gm;
    public RectTransform bgGO;
    public GameObject menuButtons;
    public GameObject creditGO;
    public RectTransform endGameGO;
    public LeanTweenType dropEase;
    public LeanTweenType popEase;
    public GameObject keyboardGO;
    public float duration=0.3f;

    [Header("Hangman Variables")]
    public GameObject[] bodyParts;
    public Sprite deadHead;
    public Sprite normalHead;
    public TextMeshProUGUI endGameTxt;
    public TextMeshProUGUI wordTxt;

    [Header("Sound Variables")]
    public GameObject soundButton;
    private bool isMute = false;
    public Sprite normalSound;
    public Sprite muteSound;
    public MusicManager mm;


    void Start()
    {
        LeanTween.moveY(bgGO, 0f, duration).setEase(dropEase).setOnComplete(enableButtons);
    }

    void enableButtons()
    {
        LeanTween.scale(menuButtons, new Vector2(1f, 1f), duration).setEase(popEase).setDelay(0.2f);
        LeanTween.scale(soundButton, new Vector2(1f, 1f), duration).setEase(popEase).setDelay(0.2f);
    }


    public void play()
    {
        LeanTween.moveY(bgGO, 1300f, duration).setEase(dropEase);
        LeanTween.moveY(menuButtons.GetComponent<RectTransform>(), 1300f, duration).setEase(dropEase);
        LeanTween.scale(keyboardGO, new Vector2(1f, 1f), duration).setEase(popEase).setDelay(0.2f);
    }

    public void credits(string direction)
    {
        if (direction == "up")
        {
            LeanTween.moveY(creditGO.GetComponent<RectTransform>(), 0f, duration).setEase(dropEase);
            LeanTween.moveY(bgGO, 1300f, duration).setEase(dropEase);
            LeanTween.moveY(menuButtons.GetComponent<RectTransform>(), 1300f, duration).setEase(dropEase);
        }
        else if(direction == "down")
        {
            LeanTween.moveY(creditGO.GetComponent<RectTransform>(), -1300f, duration).setEase(dropEase);
            LeanTween.moveY(bgGO, 0f, duration).setEase(dropEase);
            LeanTween.moveY(menuButtons.GetComponent<RectTransform>(), 0f, duration).setEase(dropEase);
        }
    }

    public void ChangeSound()
    {
        if(!isMute)
        {
            isMute = true;
            soundButton.GetComponent<Image>().sprite=muteSound;
            mm.MuteMusic(true);
        }
        else
        {
            isMute=false;
            soundButton.GetComponent<Image>().sprite = normalSound;
            mm.MuteMusic(false);
        }
    }

    public void mistake(int n)
    {
        mm.PlayErrorSound();
        if (n==0)
        {
            LeanTween.moveY(bodyParts[n].GetComponent<RectTransform>(), -77f, duration).setEase(dropEase).setDelay(0.2f);
        }
        else
        {
            popBodyParts(n);
        }
    }

    void popBodyParts(int n)
    {
        if(n==6)
        {
            LeanTween.scale(bodyParts[n], new Vector2(1f, 1f), duration).setEase(popEase).setDelay(0.2f).setOnComplete(gameLost);
        }
        else
        {
            LeanTween.scale(bodyParts[n], new Vector2(1f, 1f), duration).setEase(popEase).setDelay(0.2f);
        }
    }

    void gameLost()
    {
        mm.PlayFailSound();
        gm.playingGame=false;
        bodyParts[1].GetComponent<Image>().sprite = deadHead;
        endGameTxt.text = "you lost!!";
        wordTxt.text ="("+ gm.currentWord+")";
        LeanTween.moveY(endGameGO, 200f, duration).setEase(dropEase).setDelay(0.2f);
    }

    public void gameWon()
    {
        mm.PlayWinSound();
        endGameTxt.text = "you won!!";
        wordTxt.text = "bravo!!";
        LeanTween.moveY(endGameGO, 200f, duration).setEase(dropEase).setDelay(0.2f);
    }

    public void resetBodyParts()
    {
        LeanTween.moveY(bodyParts[0].GetComponent<RectTransform>(), 480f, duration).setEase(dropEase).setDelay(0.2f);
        LeanTween.moveY(endGameGO, -250f, duration).setEase(dropEase);
        for (int i = 1; i < bodyParts.Length; i++)
        {
            LeanTween.scale(bodyParts[i], new Vector2(0f, 0f), 0.1f);
            if (i == 1)
            {
                bodyParts[i].GetComponent<Image>().sprite = normalHead;
            }
        }
    }

    public void home()
    {
        LeanTween.moveY(bgGO, 0f, duration).setEase(dropEase).setDelay(0.2f);
        LeanTween.moveY(menuButtons.GetComponent<RectTransform>(), 0f, duration).setEase(dropEase).setDelay(0.2f);
        LeanTween.scale(keyboardGO, new Vector2(0f, 0f), duration).setEase(popEase).setDelay(0.2f);
    }
}
