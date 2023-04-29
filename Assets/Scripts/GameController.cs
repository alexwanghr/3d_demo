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
    public Text studyTxt;
    public int currScore;
    public Boss boss;
    public GameObject gameOverPage;
    public ReadyPage readyPage;
    public BattlePage battlePage;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        Clear();
        GameUtils.Init();
    }

    void Clear()
    {
        checkBossDis = false;
        objList.Clear();
        boss.gameObject.SetActive(false);
        currScore=0;
        scoreTxt.text = "Score: " + currScore;
        foreach (Transform child in objtrans) 
        {
            Destroy(child.gameObject);
        }
    }
    
    public void RestartGame()
    {
        Clear();
        player.restart();
        npc.Show(true);
        InitObjects();
    }

    #region study logic
    void InitObjects()
    {
        List<string> objs = GameUtils.RandomObjs();
        for (int i = 0; i < objs.Count; i++)
        {
            InteractObject o = Instantiate(foodObj.gameObject, transform.position, transform.rotation, objtrans)
                .GetComponent<InteractObject>();
            Vector3 pos = new Vector3(Random.Range(minX, maxX), 0.6f, Random.Range(minZ, maxZ));
            o.Init(i,objs[i]);
            o.gameObject.transform.position = pos;
            objList.Add(o);
        }
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
        npc.ShowPlayerWin();
        studyTxt.text = "Congratulations";
        studyTxt.color = Color.green;
        studyTxt.GetComponent<Animator>().Play("studywin");
        currScore++;
        scoreTxt.text = "Score: " + currScore;

        if (currScore >= GameUtils.getCurrTestScore())
        {
            ShowBoss();
        }
    }

    public void PlayerLose()
    {
        npc.ShowPlayerLose();
        studyTxt.text = "Keep it up";
        studyTxt.color = Color.red;
        studyTxt.GetComponent<Animator>().Play("studywin");
        studyTxt.gameObject.SetActive(true);
    }

    #endregion


    #region test(battle) logic

    private bool checkBossDis;
    public void ShowBoss()
    {
        checkBossDis = true;
        var playerpos = player.transform.position;
        Vector3 pos = new Vector3(0, 0.35f, playerpos.z+15);
        boss.transform.position = pos;
        boss.gameObject.SetActive(true);
        player.setStop(false);
    }

    //Detect collision by checking distance between two Objects
    private void Update()
    {
        if (checkBossDis)
        {
            if (getDistance(player.transform.position, boss.transform.position) < 5)
            {
                readyPage.show();
                player.setStop(true);
            }
        }
    }
    
    public float getDistance(Vector3 pos_a, Vector3 pos_b)
    {
        return (pos_a - pos_b).magnitude;
    }
    
    public void StartBattle()
    {
        Clear();
        npc.Show(false);
        player.restart();
        boss.StartBattle();
        battlePage.gameObject.SetActive(true);
        checkBossDis = false;
        StartCoroutine(createTestObjs());
        player.setStop(false);
    }

    private bool stopGame;
    IEnumerator createTestObjs()
    {
        while (!stopGame && objList.Count<10)
        {
            InitTestObjects();
            yield return new WaitForSeconds(4f);
        }
    }
    
    void InitTestObjects()
    {
        List<string> objs = GameUtils.RandomTestObjs();
        string obj = objs[Random.Range(0, objs.Count - 1)];
        InteractObject o = Instantiate(foodObj.gameObject, transform.position, transform.rotation, objtrans)
            .GetComponent<InteractObject>();
        Vector3 pos = new Vector3(Random.Range(minX, maxX), 0.6f, 30);
        o.InitTestObj(obj);
        o.gameObject.transform.position = pos;
        objList.Add(o);
    }

    public void StopBattle()
    {
        stopGame = true;
        foreach (var testobj in objList)
        {
            testobj.ChangeState(true);
        }
    }

    public void StartBattleFromTest()
    {
        stopGame = false;
        foreach (var testobj in objList)
        {
            testobj.ChangeState(false);
        }
    }

    public void onBattleEnd(bool win)
    {
        foreach (Transform child in objtrans) 
        {
            Destroy(child.gameObject);
        }
        objList.Clear();
        
        if (win)
        {
            GameWin();
        }
        else
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        gameOverPage.SetActive(true);
        boss.gameObject.SetActive(false);
    }

    public void GameWin()
    {
        boss.gameObject.SetActive(false);
        GameUtils.SetLevel(GameUtils.GetLevel()+1);
        RestartGame();
    }
    
    #endregion

}
