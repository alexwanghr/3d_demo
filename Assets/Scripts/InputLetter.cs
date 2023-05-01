using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputLetter : MonoBehaviour
{
    public InputField inputField;
    public string answer;
    public bool correct;
    public StudyPage studyPage;
    public bool empty=true;
    void Start()
    {
        correct = false;
        inputField = GetComponent<InputField>();
        inputField.onValueChanged.AddListener(delegate {ValueChangeCheck(); });
        studyPage = GetComponentInParent<StudyPage>();
    }

    public void SetAnswer(string str)
    {
        answer = str;
    }

    public void ValueChangeCheck()
    {
        if (string.IsNullOrEmpty(inputField.text))
        {
            empty = true;
        }
        else
        {
            empty = false;
            if (inputField.text.Equals(answer.ToUpper()) || inputField.text.Equals(answer.ToLower()))
            {
                inputField.GetComponentInChildren<Text>().color = Color.green;
                correct = true;
            }
            else
            {
                inputField.GetComponentInChildren<Text>().color = Color.red;
                correct = false;
            }
        }

        studyPage.Check(correct);
    }

    public bool getEmpty()
    {
        return empty;
    }

    public bool getCorrect()
    {
        return correct;
    }

    
}
