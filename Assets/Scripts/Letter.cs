using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Letter : MonoBehaviour
{
    public string letter;
    public Text text;
    
    public void SetText(string letter)
    {
        text.text = letter;
    }

    public void SetTip(string letter)
    {
        text.text = letter;
        text.color = Color.grey;
        text.fontSize = 50;
        text.fontStyle = FontStyle.Italic;
    }
}
