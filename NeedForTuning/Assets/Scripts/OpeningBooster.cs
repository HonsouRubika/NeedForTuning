using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpeningBooster : MonoBehaviour
{
    public GameObject prefabCard;

    private GameObject card01;
    private GameObject card02;
    private GameObject card03;


    [Space(20)]
    public Transform initialCardPos;
    public Transform targetCard01;
    public Transform targetCard03;

    private Animator anim;
    [Range(100,1000)] public float speed = 100.0f;
    private bool openBooster = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (openBooster)
        {
            float step = speed * Time.deltaTime;
            card01.transform.position = Vector3.MoveTowards(card01.transform.position, targetCard01.position, step);
            card03.transform.position = Vector3.MoveTowards(card03.transform.position, targetCard03.position, step);
        }        
    }

    public void OpenBooster()
    {
        anim.SetBool("openBooster", true);
        
        card01 = Instantiate(prefabCard, initialCardPos.position, prefabCard.transform.rotation);
        card02 = Instantiate(prefabCard, initialCardPos.position, prefabCard.transform.rotation);
        card03 = Instantiate(prefabCard, initialCardPos.position, prefabCard.transform.rotation);

        StartCoroutine(BoosterDisapear());
    }

    IEnumerator BoosterDisapear()
    {
        yield return new WaitForSeconds(1.5f);
        openBooster = true;
    }
}
