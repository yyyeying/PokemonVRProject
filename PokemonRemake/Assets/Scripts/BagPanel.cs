using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Global;

public class BagPanel : MonoBehaviour
{
    private Global global;

    //当前背包的所有物品槽
    public List<GameObject> slotBagList = new List<GameObject>();

    //背包父节点
    public GameObject slotBagParent;
    private void Awake()
    {
        global =GameObject.Find("Player"). GetComponent<Global>();
    }
  
    private void InitBag()
    {
        //初始化背包槽位
        for (int i = 0; i < slotBagList.Count; i++)
        {
            slotBagList[i].AddComponent<BagSlot>();
            slotBagList[i].transform.SetParent(slotBagParent.transform);
            slotBagList[i].GetComponent<BagSlot>().slotID = i;
        }
        ShowBag();

    }
    public void ShowBag()
    {
        slotBagList.ForEach(t =>
        {
            if (t.transform.childCount>0)
            {
                 Destroy(t.transform.GetChild(0).gameObject);
            }
        });
        int a = 0;
        //精灵球数量动态的加载进背包

        if (global.pokemonBallCount > 0)
        {
            GameObject barry = Instantiate(Resources.Load("Bag/Item")) as GameObject;
            barry.GetComponent<Image>().sprite = Resources.Load<Sprite>("Bag/Sprites/ball");
            barry.GetComponentInChildren<Text>().text = global.pokemonBallCount + "";
            barry.transform.parent = slotBagList[a].transform;
            barry.transform.localPosition = Vector2.zero;
            a++;
        }
        //遍历当前所拥有的宝可梦类型和数量 动态的加载进背包

        if (global.pokemons.Count > 0)
        {
            foreach (var item in global.pokemons)
            {
                GameObject barr = Instantiate(Resources.Load("Bag/Item")) as GameObject;
                barr.GetComponent<Image>().sprite = Resources.Load<Sprite>("Bag/Sprites/PokemonIcons/" + item.Key.ToString().ToLower());
                barr.GetComponentInChildren<Text>().text = item.Value + "";
                barr.transform.parent = slotBagList[a].transform;
                barr.transform.localPosition = Vector2.zero;
                a++;
            }

        }
        //遍历当前所拥有的果子类型和数量 动态的加载进背包
        foreach (var item in global.berryCount)
        {
            if (item.Key == Berry.NULL) return;
            GameObject barr = Instantiate(Resources.Load("Bag/Item")) as GameObject;
            barr.GetComponent<Image>().sprite = Resources.Load<Sprite>("Bag/Sprites/" + item.Key. ToString().ToLower());
            barr.GetComponentInChildren<Text>().text = (item.Value<=0? 0: item.Value) + "";
            barr.transform.parent = slotBagList[a].transform;
            barr.transform.localPosition = Vector2.zero;
            a++;
        }
    }
    private void Update()
    {
        //打开隐藏背包
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (slotBagParent.activeSelf)
            {
                slotBagParent.SetActive(false);
            }
            else
            {
                slotBagParent.gameObject.SetActive(true);
                ShowBag();
            }
        }
    }
}
