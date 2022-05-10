using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public Global global;
    public PokemonController pokemonController;
    private AudioSource battle;
    private AudioSource walk;
    private int count;
    private void Start()
    {
        pokemonController = transform.parent.GetComponent<PokemonController>();
        global = GameObject.Find("Player").GetComponent<Global>();
        battle = GameObject.Find("Ball.001").GetComponent<AudioSource>();
        walk = GameObject.Find("Cube.041").GetComponent<AudioSource>();
        count = 1;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Berry")&&count==1)
        {           
            battle.Play();
            walk.Pause();
            count++;
        }
        if (other.gameObject.name.Contains("ball"))
        {
            battle.Stop();
            walk.PlayDelayed(100);
        }
    }
}
