using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AwardType
{
    BLUE,
    GREEN,
    ORANGE,
    PINK,
    YELLOW,
    NULL
}

public class AwardItem : MonoBehaviour
{
    public AwardType type;
    public float speed = 8;
    private bool startMove = false;
    private Transform player;
    
    private Rigidbody mRigid;



    void Update()
    {
        if (startMove)
        {
            transform.position = Vector3.Lerp(transform.position, player.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, player.position) < 1f)
            {
                player.GetComponent<PlayerAward>().GetAward(type);
                Destroy(this.gameObject);
            }
        }



    }

    void Start()
    {
        mRigid = GetComponent<Rigidbody>();
        mRigid.velocity = new Vector3(0, Random.Range(-1,0), 0);//让果子随机下落
    }

     void OnCollisionEnter(Collision collision)//运动到地面，检测，然后不动
    {
        if (collision.collider.tag == Tags.ground)
        {
            mRigid.useGravity = false;
            mRigid.isKinematic = true;
            SphereCollider col = this.GetComponent<SphereCollider>();
            col.isTrigger = true;
            col.radius = 0.1f;


        }
    }



    void OnTriggerEnter(Collider col)//触发检测，人接触到物品，物品向人运动
    {
        if (col.tag == Tags.player)
        {
            startMove =true;
            player = col.transform;
        }
    }

}
