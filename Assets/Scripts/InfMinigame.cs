using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfMinigame : MonoBehaviour
{
    public static InfMinigame infMinigame;

    public List<DropInfAnsw> dropPlaces;
    public List<InfTasks> infTasks;
    List<InfTasks> previousTasks = new List<InfTasks>();
    GameObject background;
    TextMeshProUGUI task;
    TextMeshProUGUI question;
    List<DragInfAnsw> answers;
    GameObject exit;

    public static int currTask;
    public int score = 0;
    public int currScore = 0;
    int taskNumb = 0;
    bool isPlaying = true;


    byte subIndex;



    // Start is called before the first frame update
    void Start()
    {
        GameManager.gameManager.subjects.TryGetValue("IT", out subIndex);
        GameManager.gameManager.GetOceny();
        infMinigame = this;

        exit = GameObject.Find("Exit");
        dropPlaces = GameObject.Find("DropContainer").GetComponentsInChildren<DropInfAnsw>().ToList();
        answers = GameObject.Find("AnswersContainer").GetComponentsInChildren<DragInfAnsw>().ToList();
        background = GameObject.Find("Background");
        task = background.GetComponentsInChildren<TextMeshProUGUI>()[0];
        question = background.GetComponentsInChildren<TextMeshProUGUI>()[1];
        foreach(DropInfAnsw drop in dropPlaces)
        {
            drop.gameObject.SetActive(false);
            
        }
        exit.SetActive(false);

        LoadQuestion();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && isPlaying)
        {
            CheckAnswers();
        }
        if (GameManager.gameManager.informaticsResit >= 2 && GameManager.gameManager.resits[subIndex])
        {
            GameManager.gameManager.resits[subIndex] = false;
        }
    }
    public void StartGame()
    {
        isPlaying = true;
        GameObject.Find("AnswersContainer").SetActive(true);
        task.gameObject.SetActive(true);
        LoadQuestion();
    }
    public void CheckAnswers()
    {
        
        foreach (DropInfAnsw drop in dropPlaces)
        {
            if (drop.isGoodPlace) currScore++;
        }
        if(currScore == infTasks[currTask].needed.Length)
        {
            score++;
        }

        if (taskNumb == 4)
        {
            SumUpMiniGame();
            return;
        }
        
        LoadQuestion();
        
    }
    public void LoadQuestion()
    {
        if (GameManager.gameManager.resits[subIndex])
        {
            int answPosition = -380;
            taskNumb++;
            currScore = 0;

            foreach (DropInfAnsw drop in dropPlaces)
            {
                drop.isGoodPlace = false;
                drop.gameObject.SetActive(false);

            }
            foreach (DragInfAnsw answ in answers)
            {
                answ.GetComponent<RectTransform>().anchoredPosition = new Vector2(answPosition, 0);
                answPosition += 190;
                answ.GetComponent<CanvasGroup>().blocksRaycasts = true;
                answ.GetComponent<RectTransform>().sizeDelta = new Vector2(150, 60);


            }
            if (previousTasks != null)
            {
                do
                {
                    currTask = Random.Range(0, infTasks.Count);
                } while (previousTasks.Contains(infTasks[currTask]));
            }
            else
                currTask = Random.Range(0, infTasks.Count);

            previousTasks.Add(infTasks[currTask]);

            task.text = infTasks[currTask].task;
            question.text = infTasks[currTask].description;
            answers[0].gameObject.GetComponent<TextMeshProUGUI>().text = infTasks[currTask].odp1;
            answers[1].gameObject.GetComponent<TextMeshProUGUI>().text = infTasks[currTask].odp2;
            answers[2].gameObject.GetComponent<TextMeshProUGUI>().text = infTasks[currTask].odp3;
            answers[3].gameObject.GetComponent<TextMeshProUGUI>().text = infTasks[currTask].odp4;
            answers[4].gameObject.GetComponent<TextMeshProUGUI>().text = infTasks[currTask].odp5;
            for (int i = 0; i < infTasks[currTask].needed.Length; i++)
            {
                dropPlaces[i].gameObject.SetActive(true);
                dropPlaces[i].gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(infTasks[currTask].needed[i].x, infTasks[currTask].needed[i].y);
            }
        }
        else
        {
            question.gameObject.SetActive(true);
            question.text = "Osiągnięto limit popraw. Przysługuje ci tylko jedna możliwość poprawy!";
            isPlaying = false;
            foreach( DragInfAnsw drag in answers)
            {
                drag.gameObject.SetActive(false);
            }
            foreach (DropInfAnsw drop in dropPlaces)
            {
                drop.gameObject.SetActive(false);
            }
            task.gameObject.SetActive(false);
            exit.SetActive(true);
            GameObject.Find("AnswersContainer").SetActive(false);
        }
    }

    private void SumUpMiniGame()
    {
        GameManager.gameManager.informaticsResit++;
        isPlaying = false;

        question.text = "Zakończono sprawdzian z oceną " + (score + 1);
        PlayerManager.playerManager.playerStats.oceny.Add(score + 1);
        GameManager.gameManager.SetOceny();

        GameObject.Find("AnswersContainer").SetActive(false);
        task.gameObject.SetActive(false);
        exit.SetActive(true);
        foreach (DropInfAnsw drop in dropPlaces)
        {
            drop.isGoodPlace = false;
            drop.gameObject.SetActive(false);
        }


        score = 0;
        taskNumb = 0;
        previousTasks.Clear();

    }
    public void CloseMinigame()
    {

    }

}
