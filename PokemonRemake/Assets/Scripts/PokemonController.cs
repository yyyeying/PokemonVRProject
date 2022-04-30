using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonController : MonoBehaviour
{
    static int MIN_DISTANCE = 30;

    // Public variables will be exposed in the inspector window.
    private GameObject player;
    private Global global;
    private Animator mAnim;
    private Rigidbody mRigid;

    public Global.Pokemon pokemon;
    public float speed = 1.0f;
    public float turnSpeed = 1.0f;

    private Vector3 originPos;
    private bool battleReady;
    void Start()
    {
        player = GameObject.Find("Player");
        global = player.GetComponent<Global>();
        mAnim = GetComponent<Animator>();
        mRigid = GetComponentInChildren<Rigidbody>();

        originPos = transform.position;
        battleReady = false;
    }
    void Update()
    {
        switch(global.status)
        {
            case Global.GameStat.WALK:
                // Random move
                mAnim.SetBool("Walk", true);
                Vector3 targetTrans = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
                Vector3 translationHorizon = Vector3.forward * Time.deltaTime * speed;
                float heightDiff = Terrain.activeTerrain.SampleHeight(transform.position) - transform.position.y;
                Vector3 translationVertical = Vector3.up * heightDiff;
                transform.Translate(translationHorizon + translationVertical);
                Quaternion q = Quaternion.LookRotation(new Vector3(targetTrans.x, 1000*heightDiff, targetTrans.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, q, turnSpeed * Time.deltaTime);
                //print("targetTrans: " + targetTrans + ", position: " + transform.position + "Orientation: " + ", hetghtDiff: " + heightDiff);
                break;
            case Global.GameStat.BATTLE:
                if(global.battle.pokemon == pokemon)
                {
                    if(!battleReady)
                    {
                        // Move to player
                        mAnim.SetBool("Walk", true);
                        Vector3 transHorizon = Vector3.forward * Time.deltaTime * speed;
                        float hDiff = Terrain.activeTerrain.SampleHeight(transform.position) - transform.position.y;
                        Vector3 transVertical = Vector3.up * hDiff;
                        transform.Translate(transHorizon + transVertical);
                        transform.LookAt(player.transform.position);
                        float distance = (player.transform.position - transform.position).sqrMagnitude;
                        //Debug.Log(distance);
                        if(distance <= MIN_DISTANCE)
                        {
                            battleReady = true;
                        }
                    }
                    else
                    {
                        mAnim.SetBool("Walk", false);
                    }
                    if(!global.battle.isPlayerTurn)
                    {
                        // It's pokemon's turn!
                        global.battle.PokemonTurn();
                    }
                }
                else 
                {
                    mAnim.SetBool("Walk", false);
                }
                
                break;
        }        
    }
}
