using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int life;
    public int currlife;
    public Animator animator;

    public void Init()
    {
        int level = GameUtils.GetLevel();
        switch (level)
        {
            case(1):
                life = 5;
                break;
            case(2):
                life = 8;
                break;
            case(3):
                life = 10;
                break;
            default:
                life = 5;
                break;
        }

        currlife = life;
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
        life--;
        if (life == 0)
        {
            Dead();
        }

        PlayAni("DefaultGetHit");
    }

    public void Dead()
    {
        gameObject.SetActive(false);
    }
}
