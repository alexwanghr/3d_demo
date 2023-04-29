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
    public GameController gameController;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        gameController = FindObjectOfType<GameController>();
        boss = FindObjectOfType<Boss>();
    }

    public void Refresh()
    {
        bossLife.fillAmount = (float)boss.GetBossCurrLife()/boss.GetBossLife();
        playerLife.fillAmount = (float)player.GetCurrLife() / 3;
        if (boss.GetBossCurrLife() == 0 || player.GetCurrLife() == 0)
        {
            StartCoroutine(wait());
            gameController.onBattleEnd(boss.GetBossCurrLife()==0);
            Close();
        }
    }

    private IEnumerator wait()
    {
        yield return new WaitForSeconds(1f);
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
