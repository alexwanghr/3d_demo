using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
    public string name;
    public int id;
    public int studyId;
    
    public void Init(int i, studyObj obj)
    {
        studyId = obj.studyId;
        id = i;
        name = obj.name;
        
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

    public int getStudyId()
    {
        return studyId;
    }
    
}
