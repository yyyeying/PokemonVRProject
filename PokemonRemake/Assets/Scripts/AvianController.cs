using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvianController : MonoBehaviour
{
    static float height = 16f;
    static int max_count = 400;
    private GameObject player;
    private Global global;
    private Animator mAnim;
    private Rigidbody mRigid;

    public Global.Pokemon pokemon;
    public float speed = 1.0f;
    public float turnSpeed = 1.0f;

    private Vector3 originPos;
    private Vector3 targetTrans;
    private int count;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        global = player.GetComponent<Global>();
        mAnim = GetComponent<Animator>();
        mRigid = GetComponentInChildren<Rigidbody>();

        originPos = transform.position;
        targetTrans = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(count == max_count)
        {
            //Debug.Log("Reset orientation");
            targetTrans = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
            count = 0;
        }
        Vector3 translationHorizon = Vector3.forward * Time.deltaTime * speed;
        float heightDiff = Terrain.activeTerrain.SampleHeight(transform.position) - transform.position.y + height;
        Vector3 translationVertical = Vector3.up * heightDiff;
        transform.Translate(translationHorizon + translationVertical);
        Quaternion q = Quaternion.LookRotation(new Vector3(targetTrans.x, 100 * heightDiff, targetTrans.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, q, turnSpeed * Time.deltaTime);
        //Debug.Log(Terrain.activeTerrain.SampleHeight(transform.position));
        count++;
    }
}
