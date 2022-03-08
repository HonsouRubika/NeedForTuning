using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WreckingBall : MonoBehaviour
{
    private bool swapDir = false;
    float t = 0;
    [Range (1,50)]
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        float startpos = Random.Range(-1, 2);
        
        transform.rotation = Quaternion.Euler(0f,0f,startpos*15f);
    }

    // Update is called once per frame
    void Update()
    {
        

        if (!swapDir)
        {
            if (t < 1) t += Time.deltaTime * (speed/10);
            else swapDir = true;
        }
        else
        {
            if (t > 0) t -= Time.deltaTime * (speed / 10);
            else swapDir = false;
        }

        transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 15), Quaternion.Euler(0, 0, -15), t);
    }
}
