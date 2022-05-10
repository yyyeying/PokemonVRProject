using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    public Slider playerHpSlider;
    public Slider pokenhappinessSlider;
    private Global global;
    public GameObject tips;
    public AudioSource success;
    // Start is called before the first frame update
    void Start()
    {
        pokenhappinessSlider.gameObject.SetActive(false);
        playerHpSlider.value = 100;
        global = GameObject.Find("Player").GetComponent<Global>();
    }
    private void Update()
    {
        if (global.hp<=0)
        {
            global.hp = 0;
            global.status = Global.GameStat.WALK;
            HidepokenhappinessSlider(false);
            tips.GetComponentInChildren<Text>().text = "HP < 0! Maybe next time!";
        }
        playerHpSlider.value = global.hp;
    }
    public void ShowpokenhappinessSlider()
    {
        pokenhappinessSlider.gameObject.SetActive(true);
        pokenhappinessSlider.value = global.battle.pokemonHappiness;
        if (pokenhappinessSlider.value>50)
        {
            tips.SetActive(true);
            tips.GetComponentInChildren<Text>().text = "Happiness > 50! Time to throw the ball!";
            Invoke("HideTips", 1);
        }
    }
    public void HidepokenhappinessSlider(bool isShowTips, bool isSuccess = false)
    {
        pokenhappinessSlider.gameObject.SetActive(false);
        pokenhappinessSlider.value = 0;

        if (isShowTips)
        {
            success.Play();
            tips.SetActive(true);
            tips.GetComponentInChildren<Text>().text = "success_hit";
            Invoke("HideTips", 1);
        }
    }

    public void ShowTips()
        {
                tips.SetActive(true);
                tips.GetComponentInChildren<Text>().text = "fail_hit";
                Invoke("HideTips",1);
            
        }
   
    public void HideTips()
    {
        tips.SetActive(false);
        tips.GetComponentInChildren<Text>().text = "";
    }
    public void  AddBall()
    {
        global. pokemonBallCount++;
    }

    public void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        GameObject.Find("First Person Camera").GetComponent<FirstPersonLook>().enabled = true;
    }
}
