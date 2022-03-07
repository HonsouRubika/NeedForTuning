using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryManager : MonoBehaviour
{
    [Header("Start Menu")]
    public GameObject title;
    public GameObject buttonStart;
    public GameObject clickableStartText;

    [Header("Opening First Booster")]
    //Spawn Bulle Texte
    //Scroll du Booster 

    // Road To Deck Building

    public GameObject blackScreen;

    // Start is called before the first frame update
    void Start()
    {
        
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
