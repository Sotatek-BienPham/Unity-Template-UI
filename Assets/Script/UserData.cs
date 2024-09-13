using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class UserData : MonoBehaviour
{
    public static UserData Instance { get; private set; }
    public QuestManager questManager;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

}
