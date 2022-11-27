using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance { get; private set; }

    private void Awake()
    {
        if (instance!=null && instance != this)
            Destroy(gameObject);
        else     
        instance = this;
    }

    private void Load()
    {

    }


    private void Save()
    {

    }


}
