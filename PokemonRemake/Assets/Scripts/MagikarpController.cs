using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagikarpController : MonoBehaviour
{

    static float terrainHeight = 8.6f;
    private GameObject player;
    private Global global;
    private Animator mAnim;
    private Rigidbody mRigid;

    public Global.Pokemon pokemon;
    public float speed = 1.0f;
    public float turnSpeed = 1.0f;

    private Vector3 originPos;
    //private bool battleReady;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        global = player.GetComponent<Global>();
        mAnim = GetComponent<Animator>();
        mRigid = GetComponentInChildren<Rigidbody>();

        originPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 targetTrans = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
        Vector3 translationHorizon = Vector3.forward * Time.deltaTime * speed;
        float heightDiff = Terrain.activeTerrain.SampleHeight(transform.position) - transform.position.y;
        Vector3 translationVertical = Vector3.up * heightDiff;
        transform.Translate(translationHorizon + translationVertical);
        Quaternion q = Quaternion.LookRotation(new Vector3(targetTrans.x, 1000 * heightDiff, targetTrans.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, q, turnSpeed * Time.deltaTime);
        Debug.Log(Terrain.activeTerrain.SampleHeight(transform.position));
        if (transform.position.y < terrainHeight)
        {
            mAnim.SetBool("Walk", true);
        }
        else
        {
            mAnim.SetBool("Walk", false);
        }
    }
}
