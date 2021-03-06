using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    static int MAX_HP = 100;
    static int MIN_HP = 20;
    static int REC_INT = 60;
    static int BERRY_NUM_INIT = 10;
    static int BALL_NUM_INIT = 10;

    public enum GameStat
    {
        WALK,
        BATTLE
    }
    public enum Berry
    {
        BLUE,
        GREEN,
        ORANGE,
        PINK,
        YELLOW,
        NULL
    }
    public enum Pokemon
    {
        BULBASAUR,
        SQUIRTLE,
        CHARMANDER,
        PONYTA,
        LAPRAS,
        MAGIKARP,
        CYNDAQUIL,
        BELLOSSOM,
        MOLTRES,
        PIDGEY,
        EEVEE,
        PIKACHU
    }

    public GameStat status;
    public Battle battle;
    public InfoPanel infoPanel;

    public int pokemonBallCount;
    public int hp;
    public Dictionary<Berry, int> berryCount = new Dictionary<Berry, int>();
    public Dictionary<Pokemon, int> pokemons = new Dictionary<Pokemon, int>();
    public Dictionary<Pokemon, Berry> likeList = new Dictionary<Pokemon, Berry>
    {
        {Pokemon.BULBASAUR, Berry.GREEN},
        {Pokemon.SQUIRTLE, Berry.GREEN},
        {Pokemon.CHARMANDER, Berry.PINK},
        {Pokemon.CYNDAQUIL, Berry.ORANGE},
        {Pokemon.BELLOSSOM, Berry.ORANGE},
        {Pokemon.EEVEE, Berry.YELLOW},
        {Pokemon.PIKACHU, Berry.BLUE}
    };

    private int recoverConunt;

    private void Awake()
    {
        status = GameStat.WALK;
        pokemonBallCount = BALL_NUM_INIT;
        hp = MAX_HP;
        foreach (Berry b in System.Enum.GetValues(typeof(Berry)))
        {
            berryCount[b] = BERRY_NUM_INIT;
        }
        battle = null;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("Status: " + status + ", HP: " + hp);
        RecoverHp();
    }

    // Recover HP while walking
    private void RecoverHp()
    {
        if (status == GameStat.WALK && hp < MAX_HP)
        {
            recoverConunt++;
            if (recoverConunt == REC_INT)
            {
                hp++;
                recoverConunt = 0;
            }
        }
    }

    public bool PickBerry(Berry berry)
    {
        if (!berryCount.ContainsKey(berry))
        {
            Debug.Log("PickBerry: KeyError.");
            return false;
        }
        berryCount[berry]++;
        return true;
    }
}

public class Battle
{
    static int HAPPY_INIT = 30;
    static int HAPPY_THRESHOLD = 60;
    static int HAPPY_MAX = 100;

    static int HAPPY_LIKE = 30;
    static int HAPPY_DISLIKE = 5;

    static int HP_LIKE = 5;
    static int HP_DISLIKE = 20;

    static int SUCC_INIT = 20;
    static int SUCC_HI = 90;

    public bool isPlayerTurn;
    public int pokemonHappiness;
    public int successRate;
    public int throwCount;

    public Global.Berry lastBerry;
    public Global.Berry thrownBerry;

    public Global.Pokemon pokemon;

    private GameObject player;
    private Global global;
    public Battle(Global.Pokemon mPokemon)
    {
        player = GameObject.Find("Player");
        global = player.GetComponent<Global>();
        isPlayerTurn = true;
        pokemon = mPokemon;
        pokemonHappiness = HAPPY_INIT;
        successRate = SUCC_INIT;
        lastBerry = Global.Berry.NULL;
        thrownBerry = Global.Berry.NULL;
        throwCount = 0;
    }

    public bool ThrowBerry(Global.Berry berry)
    {
        //if (global.status != Global.GameStat.BATTLE)
        //{
        //    Debug.Log("ThrowBerry: WrongGameStatusError.");
        //    return false;
        //}
        if (!global.berryCount.ContainsKey(berry))
        {
            Debug.Log("ThrowBerry: KeyError.");
            return false;
        }
        if (global.berryCount[berry] > 0)
        {
            Debug.Log("ThrowBerry: Throw berry " + berry + " success.");
            global.berryCount[berry]--;
            thrownBerry = berry;
            lastBerry = Global.Berry.NULL;
            return true;
        }
        else
        {
            Debug.Log("ThrowBerry: NoBerry.");
            thrownBerry = Global.Berry.NULL;
            lastBerry = Global.Berry.NULL;
            return false;
        }
    }

    public void BerryHit(bool success)
    {
        if (success)
        {
            lastBerry = thrownBerry;
            if (pokemonHappiness < HAPPY_MAX)
            {
                if (lastBerry == global.likeList[pokemon])
                {
                    pokemonHappiness = Mathf.Min(pokemonHappiness + HAPPY_LIKE, HAPPY_MAX);
                }
                else
                {
                    pokemonHappiness = Mathf.Min(pokemonHappiness + HAPPY_DISLIKE, HAPPY_MAX);
                }
            }
            thrownBerry = Global.Berry.NULL;
            if (global.status == Global.GameStat.BATTLE)
            {
                isPlayerTurn = false;
            }
        }
        else
        {
            lastBerry = Global.Berry.NULL;
        }
    }

    public bool ThrowBall()
    {
        if (global.status != Global.GameStat.BATTLE)
        {
            Debug.Log("ThrowBall: WrongGameStatusError.");
            return false;
        }
        if (global.pokemonBallCount > 0)
        {
            Debug.Log("ThrowBall: Success.");
            global.pokemonBallCount--;
            return true;
        }
        else
        {
            Debug.Log("ThrowBall: NoBall.");
            return false;
        }
    }

    public bool BallHit()
    {
        if (pokemonHappiness >= HAPPY_THRESHOLD)
        {
            successRate = SUCC_HI;
        }
        else
        {
            successRate = SUCC_INIT;
        }
        int cat = Random.Range(0, 100);
        if (cat < successRate)
        {
            Debug.Log("BallHit: success");
            global.status = Global.GameStat.WALK;
            if (global.pokemons.ContainsKey(pokemon))
            {
                global.pokemons[pokemon] += 1;
            }
            else
            {
                global.pokemons.Add(pokemon, 1);
            }
            return true;
        }
        else
        {
            Debug.Log("BallHit: fail");
            if (throwCount >= 3 || global.hp < 30 || global.pokemonBallCount == 0)
            {
                global.status = Global.GameStat.WALK;
                global.infoPanel.HidepokenhappinessSlider(false);
            }
            isPlayerTurn = false;
            return false;
        }
    }

    public void PokemonTurn()
    {
        if (isPlayerTurn)
        {
            return;
        }
        if (lastBerry == global.likeList[pokemon])
        {
            global.hp -= HP_LIKE;
        }
        else
        {
            global.hp -= HP_DISLIKE;
        }
        global.hp = global.hp <= 0 ? 0 : global.hp;
        isPlayerTurn = true;
        Debug.Log("Pokemon attack, HP: " + global.hp);
    }
}