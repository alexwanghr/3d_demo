using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class StudyPageUI : MonoBehaviour
{
    public string name;
    public int studyId;
    public string answer;
    public Transform content;
    public Letter letter;
    public InputLetter inputletter;
    public GameObject wrong;
    public GameObject right;
    public List<InputLetter> inputList = new List<InputLetter>();
    public bool win;
    public GameController gameController;
    public Transform tip;

    public void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    private void OnEnable()
    {
        win = false;
        wrong.SetActive(false);
        right.SetActive(false); 
    }

    public void Init(string name, int studyId)
    {
        foreach (Transform child in content) 
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in tip) 
        {
            Destroy(child.gameObject);
        }
        inputList.Clear();
        
        this.name = name;
        this.studyId = studyId;
        char[] letters = RandomName();
        answer.ToUpper();
        char[] answers = GameUtils.Shuffle(answer);
        int index = 0;
        for (int i = 0; i < letters.Length; i++)
        {
            string s = letters[i].ToString();
            if (!s.Equals("0"))
            {
                Letter l = Instantiate(letter, letter.transform.position, transform.rotation, content); 
                l.SetText(letters[i].ToString().ToUpper());
                l.gameObject.SetActive(true);
            }
            else{
                InputLetter input = Instantiate(inputletter,inputletter.transform.position, transform.rotation, content); 
                inputList.Add(input);
                input.SetAnswer(answers[index].ToString());
                index++;
                input.gameObject.SetActive(true);
            }
        }

        foreach (var ch in answers)
        {
            Letter l = Instantiate(letter, letter.transform.position, transform.rotation, tip); 
            l.SetTip(ch.ToString().ToUpper());
            l.gameObject.SetActive(true);
        }
        
        gameObject.SetActive(true);
    }

    char[] RandomName()
    {
        var letters = name.ToCharArray();
        int blankcount = studyId+1;
        answer = "";
        for (int i = 0; i < letters.Length; i++)
        { 
            if (blankcount == 0)
            {
                break;
            }
            if (Random.value > 0.4)
            {
                answer += letters[i];
                letters[i] = char.Parse("0");
                blankcount--;
            }
        }

        return letters;
    }

    public void Check()
    {
        if (NotFinish())
        {
            right.SetActive(false);
            
            foreach (var input in inputList)
            {
                if (!input.getCorrect())
                {
                    wrong.SetActive(true);
                    right.SetActive(false);
                    win = false;
                    return;
                }
            }
        }
        else
        {
            foreach (var input in inputList)
            {
                if (!input.getCorrect())
                {
                    wrong.SetActive(true);
                    right.SetActive(false);
                    win = false;
                    return;
                }
            }
            wrong.SetActive(false);
            right.SetActive(true);
            win = true;
        }
    }

    public bool NotFinish()
    {
        foreach (var input in inputList)
        {
            if (input.getEmpty())
            {
                return true;
            }
        }
        return false;
    }

    public void onClosePage()
    {
        this.gameObject.SetActive(false);
        gameController.RefreshObj(win);
    }
    
}
