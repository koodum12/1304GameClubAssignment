using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    private int score=0;
    public TextMeshProUGUI text;
    public float time;
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        //score += GetComponent<Level1EnemyScripts>().Level1KillCount;
        //score += GetComponent<Level2Enemy>().Level2KillCount;
        if(score==0)
            text.text = Math.Round(time).ToString();
        
    }
}