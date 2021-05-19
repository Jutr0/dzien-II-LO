using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [System.Serializable]
public class InfTasks
{
    public string name;
    [TextArea]
    public string description;
    [TextArea]
    public string task;
    public string odp1;
    public string odp2;
    public string odp3;
    public string odp4;
    public string odp5;
    public dropPlace[] needed = { };
}
[System.Serializable]
    public struct dropPlace
    {
        public float x;
        public float y;
        public string correct;
    }