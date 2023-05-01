using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPage : MonoBehaviour
{
    private bool start;
    private int level;
    private float orginTime;
    private float testTime;
    private float timer = 0;
    public Text timingTxt;
    public GameController gameController;
    public PlayerController player;
    public Boss boss;
    public BattlePage battlePage;
    public Button answerBtn;
    public Transform ansTrans;
    private string ans;
    private List<string> ansList=new List<string>();
    
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        player = FindObjectOfType<PlayerController>();
        boss = FindObjectOfType<Boss>();
        level = GameUtils.GetLevel();
        switch (level)
        {
            case(1):
                orginTime = 8;
                break;
            case(2):
                orginTime = 6;
                break;
            case(3):
                orginTime = 5;
                break;
        }

        testTime = orginTime;
    }
    
    void Update()
    {
        if (start)
        {
            if (testTime <= 0)
            {
                onTimeEnd();
            }
            
            timer += Time.deltaTime;
            if (timer >= 1f)
            {
                timer = 0;
                testTime--;
                timingTxt.text = testTime.ToString();
            }
        }
    }

    public void Init(string name)
    {
        gameController.StopBattle();
        ans = name.ToLower();
        GetWrongAnswer();
        for (int i = 0; i < 3; i++)
        {
            Button o = Instantiate(answerBtn.gameObject, transform.position, transform.rotation, ansTrans)
                .GetComponent<Button>();
            o.GetComponentInChildren<Text>().text = ansList[i];
            if (ansList[i].Equals(ans))
            {
                o.onClick.AddListener(onRightClick);
            }
            else
            {
                o.onClick.AddListener(onWrongClick);
            }
            o.gameObject.SetActive(true);
        }
        
        start = true;
        testTime = orginTime;
        timingTxt.text = testTime.ToString();
        this.gameObject.SetActive(true);
    }

    public void GetWrongAnswer()
    {
        ansList.Clear();
        ansList.Add(ans);
        ansList.Add(new string(GameUtils.Shuffle(ans)));
        ansList.Add(new string(GameUtils.Shuffle(ans)));
        GameUtils.ShuffleList(ansList);
    }

    public void onRightClick()
    {
        boss.getHit();
        Close();
    }

    public void onWrongClick()
    {
        Debug.Log("Test------------on wrong click");
        player.getHit();
        Close();
    }

    public void onTimeEnd()
    {
        Debug.Log("Test------------on time end");
        player.getHit();
        Close();
    }

    void Close()
    {
        battlePage.Refresh();
        gameObject.SetActive(false);
        foreach (Transform child in ansTrans) 
        {
            Destroy(child.gameObject);
        }
        gameController.StartBattleFromTest();
    }

}
