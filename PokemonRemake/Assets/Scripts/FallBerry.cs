using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBerry : MonoBehaviour
{
    public GameObject berryPrefab;
    private GameObject berry;
    public GameObject startPos;
    // Start is called before the first frame update
    private float temer = 10;
    private bool isMiss;
    // Update is called once per frame
    void Update()
    {
        if (berryPrefab == null) return;
       
        if (berry == null)
        {
            temer -= Time.deltaTime;
            if (temer <= 0)
            {
                temer = 10;
                berry = Instantiate(berryPrefab);
                berry.transform.position = startPos.transform.position;
                temer = 10;
            }
        }
    }
}
