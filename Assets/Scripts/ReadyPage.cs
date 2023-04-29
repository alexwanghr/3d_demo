using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyPage : MonoBehaviour
{
    private GameController gameController;
    public Button yesBtn;
    public Button noBtn;

    public void Start()
    {
        yesBtn.onClick.AddListener(onYesClick);
        noBtn.onClick.AddListener(onNoClick);
        if (gameController == null)
        {
            gameController = FindObjectOfType<GameController>();
        }
    }

    public void show()
    {
        gameObject.SetActive(true);
    }
    public void onYesClick()
    {
        gameController.StartBattle();
        gameObject.SetActive(false);
    }

    public void onNoClick()
    {
        gameController.ShowBoss();
        this.gameObject.SetActive(false);
    }
}
