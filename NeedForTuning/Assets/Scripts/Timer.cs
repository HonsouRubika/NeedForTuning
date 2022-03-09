using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    
    private float currentTime = 0f;
    public float timerRounded;
    public TextMeshProUGUI timeText;

    void Update()
    {
        if (ChunkManager.Instance.isRuning)
        {
            currentTime += Time.deltaTime;
            timerRounded = Mathf.Round(currentTime * 100f) / 100f;
            timeText.text = timerRounded.ToString();
        }
        
    }
    public void resetTimer()
    {
        currentTime = 0;
        timerRounded = 0;
        timeText.text = timerRounded.ToString();
    }
}
