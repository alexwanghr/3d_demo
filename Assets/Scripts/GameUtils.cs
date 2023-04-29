using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameUtils : MonoBehaviour
{
    private static int level=1;
    private static int curr_objid;
    private static Dictionary<int, List<string>> objDic;
    public static Dictionary<string, int> studytimeDic = new Dictionary<string, int>();
    //To learn a word repeatedly, need to record the number of times you learn
    
    public static void Init()
    {
        objDic = new Dictionary<int, List<string>>();
        List<string> level1_objs = new List<string>()
        {
            "Banana", "Hamburger","Olive","Watermelon","Lamb"
        };
        List<string> level2_objs = new List<string>()
        {
            "Cheesecake",  "Greenpepper","Cookies","Icecream","Ketchup","Peach","Candy"
        };
        List<string> level3_objs = new List<string>()
        {
            "Cheese","Cherry", "Hotdog","Lambleg", "Melon"
        };
        objDic.Add(1,level1_objs);
        objDic.Add(2,level2_objs);
        objDic.Add(3,level3_objs);
    }

    public static List<string> RandomObjs()
    {
        List<string> currList = new List<string>();
        int size = level * 3;
        var objs = objDic[level];
        int index = Random.Range(0, objs.Count-1);
        
        while (size > 0)
        {
            while (!currList.Contains(objs[index]))
            {
                Debug.Log("RandomObjs: " + objs[index]);
                //3 same objs, each time study become harder
                currList.Add(objs[index]);
                currList.Add(objs[index]);
                currList.Add(objs[index]);
                size--;
            }
            index = Random.Range(0, objs.Count - 1);
        }

        ShuffleList(currList);
        studytimeDic.Clear();
        return currList;
    }
    
    public static List<string> RandomTestObjs()
    {
        List<string> currList = objDic[level];
        ShuffleList(currList);
        return currList;
    }

    public static void SetLevel(int l)
    {
        if (l > 3)
        {
            level = 3;
            return;
        }
        level = l;
    }

    public static int GetLevel()
    {
        return level;
    }

    public static void SetCurrObjId(int id)
    {
        curr_objid = id;
    }
    
    public static int GetCurrObjId()
    {
        return curr_objid;
    }

    //when player get over than this score, player can go into final battle(test)
    public static int getCurrTestScore()
    {
        switch (level)
        {
            case(1):
                return 1;
            case(2):
                return 5;
            case(3):
                return 10;
            default:
                return 3;
        }
    }

    public static void setStudyTime(string name)
    {
        if (studytimeDic.ContainsKey(name))
        {
            int time = studytimeDic[name];
            time++;
            studytimeDic[name] = time;
        }
        else
        {
            studytimeDic.Add(name,1);
        }
    }

    public static int getStudyTime(string name)
    {
        if (studytimeDic.ContainsKey(name))
        {
            return studytimeDic[name];
        }
        else
        {
            studytimeDic.Add(name,1);
            return 1;
        }
    }
    
     /*
     * Clever way to shuffle a List<T>
     * https://forum.unity.com/threads/clever-way-to-shuffle-a-list-t-in-one-line-of-c-code.241052/
     */
    public static char[] Shuffle(string str) {
        char[] ts = str.ToCharArray();
        var count = ts.Length;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }

        return ts;
    }
    
    public static void ShuffleList<T>(IList<T> ts) {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i) {
            var r = Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
}
