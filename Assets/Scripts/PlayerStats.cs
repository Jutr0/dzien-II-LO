using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerStats
{

    public Vector2 position;
    public float hunger = 100f;
    public float money;
    public bool lunchCard;
    public List <int> oceny;
    public List<Quest> quests;
    public string currentScene = "SampleScene";
    public Profil profil;
}
public enum Profil { 
MATFIZ,
BIOCHEM,
MEN,
HUM
}