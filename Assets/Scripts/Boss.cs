using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int life;
    public int currlife;
    public Animator animator;

    public void StartBattle()
    {
        int level = GameUtils.GetLevel();
        switch (level)
        {
            case(1):
                life = 3;
                break;
            case(2):
                life = 5;
                break;
            case(3):
                life = 8;
                break;
            default:
                life = 3;
                break;
        }

        currlife = life;
        transform.position = new Vector3(0, 0.35f, 40);
        gameObject.SetActive(true);
    }
    
    public int GetBossLife()
    {
        return life;
    }

    public int GetBossCurrLife()
    {
        return currlife;
    }
    
    public void PlayAni(string name)
    {
        animator.Play(name);
    }

    public void getHit()
    {
        currlife--;
        PlayAni("GetHit");
    }
}
