using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattlePage : MonoBehaviour
{
    public Image playerLife;
    public Image bossLife;
    public Boss boss;
    public PlayerController player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        boss = FindObjectOfType<Boss>();
    }

    public void Refresh()
    {
        bossLife.fillAmount = boss.GetBossCurrLife()/boss.GetBossLife();
        playerLife.fillAmount = player.GetCurrLife() / 3;
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
    }

    public void OnEnable()
    {
        player = FindObjectOfType<PlayerController>();
        boss = FindObjectOfType<Boss>();
        bossLife.fillAmount = 1;
        playerLife.fillAmount = 1;
    }
}
