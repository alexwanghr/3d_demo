using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
    public string name;
    public int id;

    public bool stop;
    public bool isTestObj;
    private PlayerController player;
    private GameController gameController;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        gameController = FindObjectOfType<GameController>();
    }

    public void Init(int i, string word)
    {
        id = i;
        name = word;
        
        try
        {
            Instantiate(Resources.Load(name), transform.position, transform.rotation,this.transform);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception(name);
        }
    }

    public string GetName()
    {
        return name;
    }

    public int getId()
    {
        return id;
    }

    public void InitTestObj(string word)
    {
        //id = i;
        name = word;
        isTestObj = true;
        this.gameObject.name = "Test";
        
        try
        {
            Instantiate(Resources.Load(name), transform.position, transform.rotation,this.transform);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception(name);
        }
    }

    public void Update()
    {
        if (isTestObj)
        {
            if (!stop)
            {
                transform.position += Vector3.back * Time.deltaTime * 2f;
                if (getDistance(player.transform.position, transform.position) < 3 && transform.position.z>=player.transform.position.z)
                {
                    gameController.OpenTestPage(this);
                }

                if (transform.position.z < -1)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
    
    public float getDistance(Vector3 pos_a, Vector3 pos_b)
    {
        return (pos_a - pos_b).magnitude;
    }

    public void ChangeState(bool stop)
    {
        this.stop = stop;
    }
}
