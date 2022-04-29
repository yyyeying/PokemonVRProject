using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global: MonoBehaviour
{
    public enum GameStat
    {
        WALK,
        BATTLE
    }
    public GameStat status;

    public int pokemonBallCount;
    public int hp;

    private void Awake()
    {
        status = GameStat.WALK;
        pokemonBallCount = 0;
        hp = 100;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
