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
    public Text[] textBook;
    [Space(10)]
    public int indexText;
    private bool displayingFirstText = false;

    [Space(10)]
    public GameObject blackScreen;

    // Start is called before the first frame update
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
        yield return new WaitForSeconds(1f);
        displayingFirstText = true;
    }

    public void DisplayText(Text text)
    {
        GameObject textObject = text.gameObject;
        textObject.SetActive(true);
        indexText++;
        Debug.Log("Displaying Text");
    }

    // Update is called once per frame
    void Update()
    {
        if (displayingFirstText)
        {
            DisplayText(textBook[indexText]);
            displayingFirstText = false;
        }
    }
}
