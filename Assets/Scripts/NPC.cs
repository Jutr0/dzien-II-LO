using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public Quest quest;
    public Canvas canvas;
    TextMeshProUGUI description;
    Image image;
    TextMeshProUGUI infoE;
    bool decision = false;
    public GameObject minigame;
    public static Animator minigameAnimator;


    void Awake()
    {

        minigame = GameObject.Find("MiniGame");
        minigameAnimator = minigame.GetComponentInChildren<Animator>();
        image = canvas.GetComponentInChildren<Image>();
        description = image.GetComponentInChildren<TextMeshProUGUI>();
        infoE = canvas.GetComponentsInChildren<TextMeshProUGUI>()[1];
        infoE.gameObject.SetActive(false);
        image.gameObject.SetActive(false);

    }
    private void Update()
    {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.CompareTag("Player") && !GameManager.gameManager.isMiniGameOpen)
        {


            if (Input.GetKeyDown(KeyCode.E))
            {
                infoE.gameObject.SetActive(false);
                image.gameObject.SetActive(true);
                description.text = quest.description;
                decision = true;
            }
            if (decision)
            {
                if (Input.GetKeyDown(KeyCode.T))
                {
                   
                    minigierka();
                    decision = false;
                    image.gameObject.SetActive(false);

                }
                else if (Input.GetKeyDown(KeyCode.N))
                {
                    description.text = "No cóż, może następnym razem";
                    decision = false;   
                }
                
            }
        }
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            infoE.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            infoE.gameObject.SetActive(false);
            image.gameObject.SetActive(false);
            decision = false;
        }
    }

    public void minigierka()
    {
        Debug.Log("minigierka");
        GameManager.gameManager.isMiniGameOpen = true;
        MathMinigame.mathMinigame.LoadTask();
        minigameAnimator.SetBool("isMiniGameOpen",true);

    }

}
