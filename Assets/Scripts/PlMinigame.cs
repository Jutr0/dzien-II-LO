using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlMinigame : MonoBehaviour
{
    public static PlMinigame plMinigame;

    public GameObject minigamePanel;
    Button exit;
    Button replay;
    TextMeshProUGUI title;
    TextMeshProUGUI word;
    TMP_InputField answer;

    public List<Zadanie> tasks;
    List<Zadanie> previousTasks = new List<Zadanie>();

    int resit = 0;
    public int taskNumb = 0;
    int score = 0;
    bool isChecking = false;
    int currTask;

    byte subIndex;





    // Start is called before the first frame update
    void Start()
    {

        GameManager.gameManager.subjects.TryGetValue("Polish", out subIndex);


        GameManager.gameManager.GetOceny();
        plMinigame = this;

        exit = minigamePanel.GetComponentsInChildren<Image>()[0].gameObject.GetComponentsInChildren<Button>()[0];
        replay = minigamePanel.GetComponentsInChildren<Image>()[0].gameObject.GetComponentsInChildren<Button>()[1];
        title = minigamePanel.GetComponentsInChildren<Image>()[0].gameObject.GetComponentsInChildren<TextMeshProUGUI>()[2];
        word = minigamePanel.GetComponentsInChildren<Image>()[0].gameObject.GetComponentsInChildren<TextMeshProUGUI>()[3];
        answer = minigamePanel.GetComponentsInChildren<Image>()[0].gameObject.GetComponentsInChildren<TMP_InputField>()[0];

        currTask = Random.Range(0, tasks.Count);
        word.text = tasks[currTask].tresc;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CheckAnswer();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.gameManager.isMiniGameOpen)
        {
            CloseMiniGame();
        }
        if (resit >= 2 && GameManager.gameManager.resits[subIndex])
        {
            GameManager.gameManager.resits[subIndex] = false;
        }
    }
    public void CheckAnswer()
    {
        if (!isChecking)
        {
            isChecking = true;
            taskNumb++;
            if (answer.text.Trim().ToLower().Equals(tasks[currTask].poprawna.Trim().ToLower()))
            {
                score++;
                answer.GetComponent<UnityEngine.UI.Image>().color = new Color32(93, 194, 31, 255);
                answer.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            }
            else
            {
                answer.GetComponent<UnityEngine.UI.Image>().color = new Color32(219, 59, 37, 255);
                answer.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;

            }

            StartCoroutine(LoadAfter3sec());


            if (previousTasks != null)
            {
                do
                {
                    currTask = UnityEngine.Random.Range(0, tasks.Count);
                } while (previousTasks.Contains(tasks[currTask]));
            }
            else
                currTask = UnityEngine.Random.Range(0, tasks.Count);
        }
    }
    private IEnumerator LoadAfter3sec()
    {
        yield return new WaitForSeconds(3);
        if (taskNumb == 4)
            SumUpMiniGame();
        else
            LoadTask();
        isChecking = false;
    }
    public void LoadTask()
    {
        if (GameManager.gameManager.resits[subIndex])
        {


            
            previousTasks.Add(tasks[currTask]);

            if (answer != null)
            {  
                    answer.gameObject.SetActive(true);
            }
            answer.ActivateInputField();
            answer.text = null;
            exit.gameObject.SetActive(false);
            replay.gameObject.SetActive(false);


            word.text = tasks[currTask].tresc;

            
                answer.GetComponent<UnityEngine.UI.Image>().color = Color.white;
                answer.GetComponentInChildren<TextMeshProUGUI>().color = new Color32(91, 163, 46, 255);
            
        }
        else
        {
            word.gameObject.SetActive(true);
            word.text = "Osiągnięto limit popraw. Przysługuje ci tylko jedna możliwość poprawy!";
            
            answer.gameObject.SetActive(false);
            
            exit.gameObject.SetActive(true);

        }
    }
    private void SumUpMiniGame()
    {

        word.text = "Zakończono sprawdzian z oceną " + (score + 1);
        PlayerManager.playerManager.playerStats.oceny.Add(score + 1);
        GameManager.gameManager.SetOceny();

        resit++;
        
            answer.gameObject.SetActive(false);
        
        exit.gameObject.SetActive(true);
        replay.gameObject.SetActive(true);
        score = 0;
        taskNumb = 0;
        previousTasks.Clear();

    }
    public void CloseMiniGame()
    {
        NPC2.minigameAnimator.SetBool("isMiniGameOpen", false);
        GameManager.gameManager.isMiniGameOpen = false;
    }

}
