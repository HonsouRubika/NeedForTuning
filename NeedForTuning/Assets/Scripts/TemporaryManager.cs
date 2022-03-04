using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryManager : MonoBehaviour
{
    public GameObject clickableStartText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartingGame()
    {
        Animator anim = clickableStartText.GetComponent<Animator>();
        anim.SetBool("clickToStart", true);
        Debug.Log("Starting Game");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
