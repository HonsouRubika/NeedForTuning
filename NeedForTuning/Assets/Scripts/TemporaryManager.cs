using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryManager : MonoBehaviour
{
    public GameObject clickableStartText;
    public GameObject blackScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartingGame()
    {
        Animator anim = clickableStartText.GetComponent<Animator>();
        anim.SetBool("clickToStart", true);
        StartCoroutine(TransitionBlackScreen());
    }

    IEnumerator TransitionBlackScreen()
    {
        yield return new WaitForSeconds(1f);
        Animator anim = blackScreen.GetComponent<Animator>();
        anim.SetBool("fadeInBlackScreen", true);
        yield return new WaitForSeconds(2f);
        anim.SetBool("fadeInBlackScreen", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
