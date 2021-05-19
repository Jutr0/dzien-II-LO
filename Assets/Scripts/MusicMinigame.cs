using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MusicMinigame : MonoBehaviour
{
    public static MusicMinigame musicMinigame;

    public GameObject minigamePanel;

    Button exit;
    Button replay;
    Button start;
    TextMeshProUGUI sum;
    GameObject container;
    List<Button> buttons;
    public List<int> repeatCode = new List<int>();
    public int repeatNum = 0;

    int resit = 0;
    public int currRepeatNum = 0;
    public bool isRepeating = false;
    public int score = 0;
    int squareIterator = 0;
      
    byte subIndex;


    // Start is called before the first frame update
    void Start()
    {
        GameManager.gameManager.GetOceny();
        musicMinigame = this;

        exit = minigamePanel.GetComponentsInChildren<Image>()[0].gameObject.GetComponentsInChildren<Button>()[0];
        replay = minigamePanel.GetComponentsInChildren<Image>()[0].gameObject.GetComponentsInChildren<Button>()[1];
        sum = minigamePanel.GetComponentsInChildren<Image>()[0].gameObject.GetComponentsInChildren<TextMeshProUGUI>()[2];
        start = minigamePanel.GetComponentsInChildren<Image>()[0].gameObject.GetComponentsInChildren<Button>()[2];
        container = GameObject.Find("Container");
        buttons = container.GetComponentsInChildren<Button>().ToList<Button>();

        start.gameObject.SetActive(false);
        exit.gameObject.SetActive(false);
        replay.gameObject.SetActive(false);
        sum.gameObject.SetActive(false);
        container.SetActive(false);

        GameManager.gameManager.subjects.TryGetValue("Music", out subIndex);

        }

    // Update is called once per frame
    void Update()
    {
        if (resit >= 2 && GameManager.gameManager.resits[subIndex])
        {
            GameManager.gameManager.resits[subIndex] = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.gameManager.isMiniGameOpen)
        {
            CloseMinigame();
        }
    }
    public void NewGame()
    {
        if (GameManager.gameManager.resits[subIndex]) {
            start.gameObject.SetActive(true);
            exit.gameObject.SetActive(false);
            replay.gameObject.SetActive(false);
            sum.gameObject.SetActive(false);

            repeatNum = 0;
            score = 0;
            repeatCode.Clear();
            currRepeatNum = 0;
        }
        else
        {
            sum.gameObject.SetActive(true);
            sum.text = "Osiągnięto limit popraw. Przysługuje ci tylko jedna możliwość poprawy!";
            exit.gameObject.SetActive(true);
        }
    }
    public void LoadMinigame()
    {
        if(!container.activeSelf)container.SetActive(true);
        if(start.gameObject.activeSelf)start.gameObject.SetActive(false);

            if (!isRepeating)
            {
                repeatCode.Add(Random.Range(1, 10));
                repeatNum++;
            }
            if (repeatNum == 10)
            {
                SumUpMinigame();
            }
            else
            {

                isRepeating = true;
                StartCoroutine(RepeatWait());



            }
        
    }
 
    private IEnumerator RepeatWait()
    {
             
        
            buttons[repeatCode[squareIterator]-1].image.color = Color.gray;

        yield return new WaitForSeconds(1f);

        buttons[repeatCode[squareIterator]-1].image.color = Color.white;

        yield return new WaitForSeconds(0.2f);

        if (++squareIterator < repeatCode.Count)
        {
            LoadMinigame();
        }
        else 
        {
            isRepeating = false;
            squareIterator = 0; 
        }
    }
    public void CheckRepeat(Button btn)
    {
        if (!isRepeating)
        {
            Debug.Log("Checking");
            if (btn.name.Equals(repeatCode[currRepeatNum++].ToString()))
            {
                score++;
                Debug.Log("correct");
                if (currRepeatNum >= repeatCode.Count)
                {
                    currRepeatNum = 0;
                    LoadMinigame();

                }
            }
            else 
            { 
                Debug.Log("incorrect");
                SumUpMinigame();
            
            
            }
        }
    }
    private void SumUpMinigame()
    {
        resit++;
        container.SetActive(false);
        exit.gameObject.SetActive(true);
        replay.gameObject.SetActive(true);
        sum.gameObject.SetActive(true);

        sum.text = "Zakończono test z oceną: ";
        switch (repeatNum)
        {
            case 1:
            case 2:
                sum.text += '1';
                PlayerManager.playerManager.playerStats.oceny.Add(1);
                break;
            case 3:
                sum.text += '2';
                PlayerManager.playerManager.playerStats.oceny.Add(2);
                break;
            case 4:
                sum.text += '3';
                PlayerManager.playerManager.playerStats.oceny.Add(3);
                break;
            case 5:
            case 6:
                sum.text += '4';
                PlayerManager.playerManager.playerStats.oceny.Add(4);
                break;
            case 7:
            case 8:
                sum.text += '5';
                PlayerManager.playerManager.playerStats.oceny.Add(5);
                break;
            case 9: 
            case 10:
                sum.text += '6';
                PlayerManager.playerManager.playerStats.oceny.Add(6);
                break;
        }
        GameManager.gameManager.SetOceny();

    }
    public void CloseMinigame()
    {
        NPC3.minigameAnimator.SetBool("isMiniGameOpen", false);
        GameManager.gameManager.isMiniGameOpen = false;
    }
}
