using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    
    public IEnumerator Shake (float duration, float magnitude) 
    {
        Vector3 originalPos = GetComponent<CameraController>().originPos;
        Camera camera = GetComponent<CameraController>().cam;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = camera.transform.localPosition.x + Random.Range(-1f, 1f) * magnitude;
            float y = camera.transform.localPosition.y + Random.Range(-1f, 1f) * magnitude;

            camera.transform.localPosition = new Vector3(x, y, GetComponent<CameraController>().originPos.z);

            elapsed += Time.deltaTime;

            yield return null;

            //explosionDone = true;
        }

        camera.transform.localPosition = originalPos;
        //explosionDone = false;
    }

    public IEnumerator ResetBool(float duration, bool boolean)
    {
        boolean = true;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        boolean = false;
        
        /*yield return new WaitForSeconds(duration);
        boolean = false;
        yield return null;*/
    }
}
