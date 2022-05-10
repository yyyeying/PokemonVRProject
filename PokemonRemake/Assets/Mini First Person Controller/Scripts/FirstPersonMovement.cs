using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public GameObject ProfessorSycamoreNPC;
    public GameObject ShaunaTalkNPC;
    public GameObject TrevorTalkNPC;

    public GameObject ProfessorSycamoreTalkView;
    public GameObject ShaunaTalkView;
    public GameObject TrevorTalkView;
    public AudioSource berryMusic;

    public Camera cam;

    public Global global;
   
    public GameObject startPoint;
    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

    Rigidbody rigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();



    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
        global = GetComponent<Global>();
    }
    private void Update()
    {
        ProfessorSycamoreNPC.transform.LookAt(transform);
        ShaunaTalkNPC.transform.LookAt(transform);
        TrevorTalkNPC.transform.LookAt(transform);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ThrowAttack(Global.Berry.BLUE);

        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ThrowAttack(Global.Berry.GREEN);

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ThrowAttack(Global.Berry.ORANGE);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            ThrowAttack(Global.Berry.PINK);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ThrowAttack(Global.Berry.YELLOW);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (global.battle == null) return;
            bool success = global.battle.ThrowBall();
            if (success)
            {
                var ball = Instantiate(Resources.Load("Bag/Berry/ball")) as GameObject;
                ball.transform.position = startPoint.transform.position;
                ball.AddComponent<Rigidbody>().AddForce(cam.transform.forward * 600);
                Destroy(ball, 3);
                Debug.Log("Throw ball: " + success + ", left: " + global.pokemonBallCount);
            }
           
        }
    }
    public void ThrowAttack(Global.Berry berry)
    {
        bool success = true;
        //if (global.battle == null) return;
        if (global.battle != null)
        {
            success = global.battle.ThrowBerry(berry);
        }
        if (success)
        {
            //Debug.Log("Throw berry: " + success + ", hit: " + hit + ", berry left: " + global.berryCount[berry] + ", Pokemon happiness: " + global.battle.pokemonHappiness);
            var berryObj = Instantiate(Resources.Load("Bag/Berry/" + berry.ToString().ToLower())) as GameObject;
            berryObj.transform.position = startPoint.transform.position;
            berryObj.AddComponent<Rigidbody>().AddForce(cam.transform.forward * 600);       

            Destroy(berryObj, 3);
            global.berryCount[berry]--;

        }
    }
    void FixedUpdate()
    {
        // Update IsRunning from input.
        IsRunning = canRun && Input.GetKey(runningKey);

        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        // Apply movement.
        rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("pick:"+ other.name);
        berryMusic.Play();
        if (other.gameObject.name.Contains("yellow"))
        {
            global.PickBerry(Global.Berry.YELLOW);
            Destroy(other.gameObject);
        }
        if (other.gameObject.name.Contains("green"))
        {
            global.PickBerry(Global.Berry.GREEN);
            Destroy(other.gameObject);

        }
        if (other.gameObject.name.Contains("blue"))
        {
            global.PickBerry(Global.Berry.BLUE);
            Destroy(other.gameObject);

        }
        if (other.gameObject.name.Contains("orange"))
        {
            global.PickBerry(Global.Berry.ORANGE);
            Destroy(other.gameObject);

        }
        if (other.gameObject.name.Contains("pink"))
        {
            global.PickBerry(Global.Berry.PINK);
            Destroy(other.gameObject);

        }

        if (other.gameObject.name.Contains("ProfessorSycamore"))
        {
          ProfessorSycamoreTalkView.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            GetComponentInChildren<FirstPersonLook>().enabled = false;
        }
        if (other.gameObject.name.Contains("Shauna"))
        {
            ShaunaTalkView.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            GetComponentInChildren<FirstPersonLook>().enabled = false;


        }
        if (other.gameObject.name.Contains("Trevor"))
        {
            TrevorTalkView.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            GetComponentInChildren<FirstPersonLook>().enabled = false;


        }
    }
}