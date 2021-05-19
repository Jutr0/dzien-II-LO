using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Quest
{
    public string name;
    public int lvl;
    [TextArea]
    public string description;
    public int goals;
}
