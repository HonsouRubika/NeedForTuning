using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TemporaryManager : MonoBehaviour
{
    [Header("Start Menu")]
    public GameObject title;
    public GameObject buttonStart;
    public GameObject clickableStartText;

    [Header("Opening First Booster")]
    public GameObject buttonText;
    public Text displayingText;
    [Space(10)]
    public int indexText;
    public Text[] textBook;
    [Space(10)]
    private bool displayingFirstText = false;
    public OpeningBooster openingBooster;
    public bool firstBoosterIsOpen = false;

    [Header("Transition")]
    public GameObject blackScreen;

    [Header("Opening Booster")]
    public bool isOpenBoosterScene;

    void Start()
    {
        indexText = 0;
    }

    public void StartingGame()
    {
        Animator anim = clickableStartText.GetComponent<Animator>();
        anim.SetBool("clickToStart", true);
        blackScreen.SetActive(true);
        StartCoroutine(TransitionBlackScreen());
    }

    IEnumerator TransitionBlackScreen()
    {
        yield return new WaitForSeconds(0.5f);
        Animator anim = blackScreen.GetComponent<Animator>();
        anim.SetBool("fadeInBlackScreen", true);
        yield return new WaitForSeconds(0.5f);
        title.SetActive(false);
        buttonStart.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        anim.SetBool("fadeInBlackScreen", false);
        yield return new WaitForSeconds(2.5f);
        if (indexText == 0)
        {
            displayingFirstText = true;
        }
    }

    public void DisplayText(Text text) //Mettre DisplayingText si référencement manuel
    {
        text = textBook[indexText];
        buttonText.SetActive(true);
        displayingText.text = text.text;
        indexText++;
        Debug.Log("Displaying Text");

        if (indexText == 4)
        {
            openingBooster.ShowingBooster();
            buttonText.SetActive(false);
        }
        else if (indexText == 6)
        {
            buttonText.SetActive(false);
        }
        else if (indexText == 8)
        {
            buttonText.SetActive(false);
            openingBooster.card01.SetActive(false);
            openingBooster.card02.SetActive(false);
            openingBooster.card03.SetActive(false);
            indexText++;
            StartCoroutine(TransitionBlackScreen());
        }
    }

    void Update()
    {
        if (displayingFirstText)
        {
            DisplayText(textBook[indexText]);
            displayingFirstText = false;
        }

        if (indexText == 6 && firstBoosterIsOpen)
        {
            DisplayText(textBook[indexText]);
        }

        if (isOpenBoosterScene)
        {
            openingBooster.canOpenTheBooster = true;
        }
    }
}
