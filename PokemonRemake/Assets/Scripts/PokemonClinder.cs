using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonClinder : MonoBehaviour
{
    public Global global;
    public PokemonController pokemonController;
    private void Start()
    {
        pokemonController = transform.parent.GetComponent<PokemonController>();
        global = GameObject.Find("Player").GetComponent<Global>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Berry"))
        {
            Debug.Log("berry!!!");
            if (global.hp <= 0)
            {
                global.status = Global.GameStat.WALK;
                pokemonController.InfoPanel.HidepokenhappinessSlider(false);
                Debug.Log("hp == 0 ");
            }
            else
            {
                if (global.status != Global.GameStat.BATTLE)
                {
                    Debug.Log("Switch to battle mode");
                    global.status = Global.GameStat.BATTLE;
                    global.battle = new Battle(pokemonController.pokemon);
                }
                global.battle.BerryHit(true);
                pokemonController.InfoPanel.ShowpokenhappinessSlider();
            }
        }
        if (other.gameObject.name.Contains("ball"))
        {
            if (global.battle != null)
            {
                bool isCat = global.battle.BallHit();
                if (isCat)
                {
                    pokemonController.InfoPanel.HidepokenhappinessSlider(true,true);
                    Debug.Log("BallHit: success +" + pokemonController.pokemon);
                    Destroy(transform.parent.gameObject);
                    Destroy(other.gameObject);                  
                }
                else
                {
                    pokemonController.InfoPanel.ShowTips();
                }
            }
        }
    }
}