﻿using UnityEngine;
using System.Collections;

public class Event_SetTimeScale : MonoBehaviour
{

    public float TimeScale;
    // Use this for initialization
    void Start()
    {
        Time.timeScale = TimeScale;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
