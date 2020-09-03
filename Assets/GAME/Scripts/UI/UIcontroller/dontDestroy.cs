using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontDestroy : MonoBehaviour
{
    public dontDestroy no;
    void Start()
    {
        DontDestroyOnLoad(no);
    }

}
