using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using Unity.Mathematics;
using System;


public class PlayerManager : MonoBehaviour
{
    public static PlayerManager playerManager;
    public PlayerStats playerStats;
    float maxHunger = 100;
    public Canvas phoneCanvas;
    TextMeshProUGUI hungry;
    TextMeshProUGUI money;
    TextMeshProUGUI lunchcard;
    TextMeshProUGUI grades;
    Animator animator;
    public List<int> oceny;


    // Start is called before the first frame update
    void Start()
    {
        playerManager = this;

        hungry = phoneCanvas.GetComponentInChildren<Image>().GetComponentInChildren<Image>().GetComponentsInChildren<TextMeshProUGUI>()[0];
        money = phoneCanvas.GetComponentInChildren<Image>().GetComponentInChildren<Image>().GetComponentsInChildren<TextMeshProUGUI>()[1];
        grades = phoneCanvas.GetComponentInChildren<Image>().GetComponentInChildren<Image>().GetComponentsInChildren<TextMeshProUGUI>()[2];
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameManager.isGameStarted)
        {   
            
                
            if (Input.GetKeyDown(KeyCode.C))
            {
                Save();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                RefreshStats();
            }
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                animator.SetBool("IsPhoneOpen", !animator.GetBool("IsPhoneOpen"));
                GameManager.gameManager.GetOceny();
                //RefreshStats();
            }
        }
        if (GameManager.gameManager.isMiniGameOpen)
        {
            phoneCanvas.gameObject.SetActive(false);
        }
        else phoneCanvas.gameObject.SetActive(true);
    }
    public void Save()
    {

       
    }
    public void Load() {
 
    }
    public void RefreshStats()
    {
        hungry.text = "Głód: " + playerStats.hunger + "/" + maxHunger;
        money.text = "Pieniądze: " + playerStats.money;
        if (playerStats.oceny.Count() != 0)
        {
            grades.text = "Średnia: " + Math.Round(playerStats.oceny.Average(), 2);
        }
        else 
        { 
            grades.text = "Średnia: ";
        }


    }
    private void OnLevelWasLoaded(int level)
    {
        playerStats = JsonUtility.FromJson<PlayerStats>(File.ReadAllText(Application.dataPath + "/save.json"));
        //transform.position = playerStats.position;
    }
    
}
