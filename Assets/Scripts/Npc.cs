using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class Npc : MonoBehaviour
{
    public Animator animator;
    Vector3 targetpos1;
    Vector3 targetpos2;
    Vector3 pos;
    private float time;
    private float maxTime;
    private float moveDistance;
 
    private bool start;
    private bool end;

    public void Start()
    {
        animator = GetComponent<Animator>();
        this.transform.Rotate(new Vector3(0,140,0));
        maxTime = Random.Range(3, 7);
        moveDistance = 2;
        targetpos1 = transform.position + new Vector3(moveDistance, 0, moveDistance);
        targetpos2 = transform.position + new Vector3(-moveDistance, 0 ,2*moveDistance);
        start = true;
        end = true;
    }

    public void PlayAni(string name)
    {
        animator.Play(name);
    }

    public void ShowPlayerWin()
    {
        PlayAni("Victory");
    }
    
    public void ShowPlayerLose()
    {
        PlayAni("Dizzy");
    }
    
    void Update()
    {
        //Move();
    }
 
    Vector3 RandV3(Vector3 a,Vector3 b)
    {
        return new Vector3(Random.Range(a.x,b.x),a.y,Random.Range(a.z,b.z));
    }
 
 
    void Move()
    {
        if (start)
        {
            if (end)
            {
                end = false;
                pos = RandV3(targetpos1, targetpos2);
                transform.LookAt(pos);
            }
            transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime);
            if (transform.position == pos)
            {
                start = false;
                maxTime = Random.Range(3, 5);
            }
        }
        else
        {
            time += Time.deltaTime;
        }
 
        if (time >= maxTime)
        {
            time = 0;
            start = true;
            end = true;
        }
    }

    public void Show(bool show)
    {
        gameObject.SetActive(show);
    }
}
