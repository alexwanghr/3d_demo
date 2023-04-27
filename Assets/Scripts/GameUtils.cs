using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class studyObj
{
    public string name;
    public int studyId;

    public studyObj(string n, int id)
    {
        name = n;
        studyId = id;
    }
}

public class GameUtils : MonoBehaviour
{
    private static int level=1;
    private static int curr_objid;
    private static Dictionary<int, List<string>> objDic;
    public static int currTry;

    public static void Init()
    {
        objDic = new Dictionary<int, List<string>>();
        List<string> level1_objs = new List<string>()
        {
            "Banana",  "Cheese","Cherry","Hamburger",
            "Hotdog","Olive","Watermelon","Lamb"
        };
        List<string> level2_objs = new List<string>()
        {
            "Cheesecake",  "Greenpepper","Cookies","Icecream",
            "Melon","Ketchup","Lambleg","Peach","Candy"
        };
        objDic.Add(1,level1_objs);
        objDic.Add(2,level2_objs);
    }

    public static List<studyObj> RandomObjs()
    {
        List<studyObj> currList = new List<studyObj>();
        int size = level * 3;
        var objs = objDic[level];
        int index = Random.Range(0, objs.Count-1);
        
        while (size > 0)
        {
            foreach (var obj in currList)
            {
                if (objs[index] == obj.name)
                {
                    index = Random.Range(0, objs.Count-1);
                }
            }
            
            //3 same objs, each time study become harder
            currList.Add(new studyObj(objs[index],1));
            currList.Add(new studyObj(objs[index],2));
            currList.Add(new studyObj(objs[index],3));
            size--;
        }

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

    public static void setCurrTry(int count)
    {
        currTry = count;
    }

    public static int getCurrTry()
    {
        return currTry;
    }

    public static int getCurrTestScore()
    {
        switch (level)
        {
            case(1):
                return 3;
            case(2):
                return 5;
            case(3):
                return 10;
            default:
                return 3;
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
