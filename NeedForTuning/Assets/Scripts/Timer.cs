using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    
    private float currentTime = 0f;
    public float timerRounded;
    public TextMeshProUGUI timeText;

    public bool isGhostTimer = false;

    private void Start()
    {
        if (isGhostTimer)
        {
            GameManager.Instance.ghostTimerScript = this;
        }
        else
        {
            GameManager.Instance.timerScript = this;
        }
    }
    void Update()
    {
        if (!isGhostTimer && ChunkManager.Instance.isRuning)
        {
            currentTime += Time.deltaTime;
            timerRounded = Mathf.Round(currentTime * 100f) / 100f;
            timeText.text = timerRounded.ToString();
        }
        else if(isGhostTimer && ChunkManager.Instance.isRuning && !ChunkManager.Instance.isGhostFinished)
        {
            currentTime += Time.deltaTime;
            timerRounded = Mathf.Round(currentTime * 100f) / 100f;
            timeText.text = timerRounded.ToString();
        }
        else
        {
            //do nothing;
        }
        
    }
    public void resetTimer()
    {
        Debug.Log("reset timer");
        currentTime = 0;
        timerRounded = 0;
        timeText.text = "0";
        Debug.Log("Time : " + timerRounded + "  ; actual time : " + timerRounded.ToString());
    }
}
