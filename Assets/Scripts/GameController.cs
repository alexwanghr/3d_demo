using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject foodObj;
    public Npc npc;
    public GameObject npcObj;
    public List<InteractObject> objList = new List<InteractObject>();
    public Transform objtrans;
    private int maxX = 7;
    private int minX = -7;
    private int maxZ = 40;
    private int minZ = 6;
    private int closeDistance = 10;
    private PlayerController player;
    public Text scoreTxt;
    public int currScore;
    public Boss boss;
    public GameObject gameOverPage;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        Clear();
        GameUtils.Init();
    }

    void Clear()
    {
        objList.Clear();
        boss.gameObject.SetActive(false);
        currScore=0;
        scoreTxt.text = "Score: " + currScore;
        foreach (Transform child in objtrans) 
        {
            Destroy(child.gameObject);
        }

        player.transform.position = new Vector3(0, 0.38f, 0);
    }

    void InitObjects()
    {
        List<studyObj> objs = GameUtils.RandomObjs();
        for (int i = 0; i < objs.Count; i++)
        {
            InteractObject o = Instantiate(foodObj.gameObject, transform.position, transform.rotation, objtrans)
                .GetComponent<InteractObject>();
            Vector3 pos = new Vector3(Random.Range(minX, maxX), 0.6f, Random.Range(minZ, maxZ));
            o.Init(i,objs[i]);
            o.gameObject.transform.position = pos;
            objList.Add(o);
        }
        
        // for (int i = 0; i < npccount; i++)
        // {
        //     Npc o = Instantiate(npcObj.gameObject, transform.position, transform.rotation, npctrans)
        //         .GetComponent<Npc>();
        //     Vector3 pos = new Vector3(Random.Range(minX, maxX), 0.6f, Random.Range(minZ, maxZ));
        //     o.gameObject.transform.position = pos;
        //     o.gameObject.SetActive(true);
        //     npcList.Add(o);
        // }
    }

    public void RefreshObj(bool win)
    {
        int currid = GameUtils.GetCurrObjId();
        if (win)
        {
            foreach (var o in objList)
            {
                if (o.getId() == currid)
                {
                    Destroy(o.gameObject);
                }
            }

            objList.Clear();
            int curr_count = objtrans.childCount;
            while (curr_count > 0)
            {
                InteractObject o = objtrans.GetChild(curr_count - 1).GetComponent<InteractObject>();
                objList.Add(o);
                curr_count--;
            }
            PlayerWin();
        }
        else
        {
            PlayerLose();
        }
        
        if (player != null)
        {
            player.setStop(false);
        }
    }

    public void PlayerWin()
    {
        Debug.Log("***************STUDY*************** player win");
        var playerPos = player.transform.position;
        npc.ShowPlayerWin();
        currScore++;
        scoreTxt.text = "Score: " + currScore;

        if (currScore >= GameUtils.getCurrTestScore())
        {
            ShowBoss();
        }
    }

    public void PlayerLose()
    {
        Debug.Log("***************STUDY*************** player lose");
        var playerPos = player.transform.position;
        npc.ShowPlayerLose();
    }

    public float getDistance(Vector3 pos_a, Vector3 pos_b)
    {
        return (pos_a - pos_b).magnitude;
    }

    public void RestartGame()
    {
        Clear();
        InitObjects();
    }

    public void ShowBoss()
    {
        boss.Init();
        var playerpos = player.transform.position;
        Vector3 pos = new Vector3(playerpos.x, 0.85f, playerpos.z+10);
        boss.transform.position = pos;
    }

    public void GameOver()
    {
        gameOverPage.SetActive(false);
    }

    public void PassCurrTest()
    {
        boss.gameObject.SetActive(false);
        GameUtils.SetLevel(GameUtils.GetLevel()+1);
        RestartGame();
    }

}
