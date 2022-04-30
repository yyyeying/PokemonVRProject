using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    private Global global;

    const KeyCode walkModeKey = KeyCode.Z;
    const KeyCode battleModeKey = KeyCode.X;
    const KeyCode throwBlueBerryKey = KeyCode.C;
    const KeyCode throwYellowBerryKey = KeyCode.V;
    const KeyCode throwBallKey = KeyCode.B;

    // Start is called before the first frame update
    void Start()
    {
        global = GetComponent<Global>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(walkModeKey))
        {
            Debug.Log("Switch to walk mode");
            global.status = Global.GameStat.WALK;
            global.battle = null;
        }
        else if(Input.GetKeyDown(battleModeKey))
        {
            Debug.Log("Switch to battle mode");
            global.status = Global.GameStat.BATTLE;
            global.battle = new Battle(Global.Pokemon.BULBASAUR);
        }
        else if(Input.GetKeyDown(throwBlueBerryKey))
        {
            bool success = global.battle.ThrowBerry(Global.Berry.BLUE);
            bool hit = false;
            if(Random.Range(0, 100) > 50)
            {
                hit = true;
            }
            else
            {
                hit = false;
            }
            global.battle.BerryHit(hit);
            Debug.Log("Throw berry: " + success + ", hit: " + hit + ", berry left: " + global.berryCount[Global.Berry.BLUE] + ", Pokemon happiness: " + global.battle.pokemonHappiness);
        }
        else if (Input.GetKeyDown(throwYellowBerryKey))
        {
            bool success = global.battle.ThrowBerry(Global.Berry.YELLOW);
            bool hit = false;
            if (Random.Range(0, 100) > 50)
            {
                hit = true;
            }
            else
            {
                hit = false;
            }
            global.battle.BerryHit(hit);
            Debug.Log("Throw berry: " + success + ", hit: " + hit + ", berry left: " + global.berryCount[Global.Berry.YELLOW] + ", Pokemon happiness: " + global.battle.pokemonHappiness);
        }
        else if(Input.GetKeyDown(throwBallKey))
        {
            bool success = global.battle.ThrowBall();
            global.battle.BallHit();
            Debug.Log("Throw ball: " + success + ", left: " + global.pokemonBallCount);
        }
    }
}
