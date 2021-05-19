using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class MathMinigame : MonoBehaviour
{
    public List <Zadanie> tasks;
    List<Zadanie> previousTasks = new List<Zadanie>();
    public int currTask = 0;
    public GameObject board;
    public static MathMinigame mathMinigame;
    public int score = 0;
    public int taskNumb = 0;
    TextMeshProUGUI mainContent;
    [SerializeField]
    Button[] answers;
    bool isChecking = false;
    byte resit = 0;

    byte subIndex;
    public string subjectName;


    private void Start()
    {
        GameManager.gameManager.subjects.TryGetValue(subjectName, out subIndex);
        GameManager.gameManager.GetOceny();
        mathMinigame = this;
        mainContent = board.GetComponentInChildren<TextMeshProUGUI>();
        answers = board.GetComponentsInChildren<Button>();


    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.gameManager.isMiniGameOpen)
        {
            CloseMiniGame();
        }


        if (resit >= 2 && GameManager.gameManager.resits[subIndex])
        {
            GameManager.gameManager.resits[subIndex] = false;
        }

    }


    public void CheckAnswer(GameObject button) {
        if (!isChecking)
        {
            isChecking = true;
            taskNumb++;
            if (button.name.Equals(tasks[currTask].poprawna))
            {
                Debug.Log("Poprawna Odpowiedz! " + button.name + " na " + tasks[currTask].poprawna);
                button.GetComponent<UnityEngine.UI.Image>().color = new Color32(93, 194, 31, 255);
                button.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
                score++;
            }
            else
            {
                Debug.Log("Niepoprawna Odpowiedz! " + button.name + " na " + tasks[currTask].poprawna);
                button.GetComponent<UnityEngine.UI.Image>().color = new Color32(219, 59, 37, 255);
                button.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;

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

            if (answers != null)
            {
                foreach (Button answ in answers)
                {
                    answ.gameObject.SetActive(true);
                }

            }


            mainContent.text = tasks[currTask].tresc;

            answers[0].GetComponentInChildren<TextMeshProUGUI>().text = tasks[currTask].odpA;
            answers[1].GetComponentInChildren<TextMeshProUGUI>().text = tasks[currTask].odpB;
            answers[2].GetComponentInChildren<TextMeshProUGUI>().text = tasks[currTask].odpC;
            answers[3].GetComponentInChildren<TextMeshProUGUI>().text = tasks[currTask].odpD;
            answers[4].gameObject.SetActive(false);
            answers[5].gameObject.SetActive(false);

            foreach (Button ansBtn in answers)
            {
                ansBtn.GetComponent<UnityEngine.UI.Image>().color = Color.white;
                ansBtn.GetComponentInChildren<TextMeshProUGUI>().color = new Color32(91, 163, 46, 255);
            }


        }
        else
        {
            mainContent.gameObject.SetActive(true);
            mainContent.text = "Osiągnięto limit popraw. Przysługuje ci tylko jedna możliwość poprawy!";
            try
            {
                foreach (Button answ in answers)
                {
                    answ.gameObject.SetActive(false);
                }
                answers[4].gameObject.SetActive(true);
            }
            catch (Exception e) {
                Debug.Log(e);
                    }

        }
    }

    private void SumUpMiniGame()
    {

        mainContent.text = "Zakończono sprawdzian z oceną " + (score+1);
        PlayerManager.playerManager.playerStats.oceny.Add(score + 1);
        GameManager.gameManager.SetOceny();
        resit++;
        foreach(Button answ in answers) {
            answ.gameObject.SetActive(false);
        }
        answers[4].gameObject.SetActive(true);
        answers[5].gameObject.SetActive(true);
        score = 0;
        taskNumb = 0;
        previousTasks.Clear();

    }
    public void CloseMiniGame()
    {
        NPC.minigameAnimator.SetBool("isMiniGameOpen", false);
        GameManager.gameManager.isMiniGameOpen = false;
    }

}
