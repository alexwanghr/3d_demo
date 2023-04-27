using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPage : MonoBehaviour
{
    public Button level1;
    public Button level2;
    public Button level3;
    public GameController gameController;

    public void Start()
    {
        level1.onClick.AddListener(onLevel1Click);
        level2.onClick.AddListener(onLevel2Click);
        level3.onClick.AddListener(onLevel3Click);
        gameController = FindObjectOfType<GameController>();
    }

    public void onLevel1Click()
    {
        GameUtils.SetLevel(1);
        ClosePage();
    }
    
    public void onLevel2Click()
    {
        GameUtils.SetLevel(2);
        ClosePage();
    }
    
    public void onLevel3Click()
    {
        GameUtils.SetLevel(3);
        ClosePage();
    }

    void ClosePage()
    {
        gameObject.SetActive(false);
        gameController.RestartGame();
    }
}
