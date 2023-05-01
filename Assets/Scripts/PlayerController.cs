using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayAni
{
    Move=1,
    Stop=2,
}
public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public Animator animator;
    public bool stop;
    private PlayAni currAni;
    public StudyPage studyPage;
    public TestPage testPage;
    public int currLife;

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    public void setStop(bool currstop)
    {
        stop = currstop;
    }

    public void restart()
    {
        currLife = 3;
        transform.position = new Vector3(0, 0.38f, 0);
    }

    void Update()
    {
        if (stop)
        {
            ChangeAnimator(PlayAni.Stop);
            return;
        }
        float speed = 4f;
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * Time.deltaTime * speed;
            ChangeAnimator(PlayAni.Move);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.back * Time.deltaTime * speed;
            ChangeAnimator(PlayAni.Move);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * Time.deltaTime * speed;
            ChangeAnimator(PlayAni.Move);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * Time.deltaTime * speed;
            ChangeAnimator(PlayAni.Move);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Food"))
        {
            InteractObject o = other.GetComponent<InteractObject>();
            studyPage.Init(o.GetName());
            GameUtils.SetCurrObjId(o.getId());
            stop = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        stop = false;
    }

    public void ChangeAnimator(PlayAni ani)
    {
        if (currAni == ani)
        {
            return;
        }

        currAni = ani;
        switch (ani)
        {
            case PlayAni.Move:
                animator.SetTrigger("move");
                break;
            case PlayAni.Stop:
                animator.SetTrigger("stop");
                break;
        }
    }

    public int GetCurrLife()
    {
        return currLife;
    }

    public void getHit()
    {
        currLife--;
    }

}
