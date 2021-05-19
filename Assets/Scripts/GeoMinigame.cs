using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeoMinigame : MonoBehaviour
{
    public static GeoMinigame geoMinigame;

    [SerializeField]
    GameObject[] puzzleGameObjects;
    List<Image> puzzles = new List<Image>();
    TextMeshProUGUI timer;
    int currTime = 0;
    bool isTimeRunning = false;
    GameObject menu;
    TextMeshProUGUI text;
    Button start;
    Button exit;
    Button replay;
    GameObject container;
    int ocena;
    GameObject timerGameObject;
    float puzzleX;
    float puzzleY;

    byte subIndex;


    // Start is called before the first frame update
    void Start()
    {
        geoMinigame = this;
        GameManager.gameManager.subjects.TryGetValue("Polish", out subIndex);


        menu = GameObject.Find("Menu");
        text = menu.GetComponentsInChildren<TextMeshProUGUI>()[0];
        start = menu.GetComponentsInChildren<Button>()[0];
        exit = menu.GetComponentsInChildren<Button>()[1];
        replay = menu.GetComponentsInChildren<Button>()[2];
        container = GameObject.Find("Container");
        timer = GameObject.Find("Time").GetComponent<TextMeshProUGUI>();
        timerGameObject = timer.GetComponentsInParent<Transform>()[1].gameObject;


        Debug.Log(text.name + start.name + exit.name + replay.name);

        puzzleGameObjects = GameObject.FindGameObjectsWithTag("Puzzle");
        foreach (GameObject puzzle in puzzleGameObjects)
        {
            puzzles.Add(puzzle.GetComponent<Image>());
            puzzle.SetActive(false);
        }
        timerGameObject.SetActive(false);
        exit.gameObject.SetActive(false);
        replay.gameObject.SetActive(false);
        container.gameObject.SetActive(false);
        text.gameObject.SetActive(false);

        GameManager.gameManager.GetOceny();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.gameManager.SetOceny();
            CloseMinigame close = GetComponent<CloseMinigame>();
            close.BackToClass("SalaGeo");
        }
        if (GameManager.gameManager.geographyResit >= 2 && GameManager.gameManager.resits[subIndex])
        {
            GameManager.gameManager.resits[subIndex] = false;
        }

    }

    public bool IsGameEnd()
    {
        foreach (GameObject puzzle in puzzleGameObjects)
        {
            if (!puzzle.GetComponent<PuzzleDragScript>().isGoodPlace) return false;
        }
        isTimeRunning = false;
        return true;
    }

    public void SumUpMinigame()
    {
        GameManager.gameManager.geographyResit++;
        Debug.Log("Wygrałeś!");
        foreach(GameObject puzzle in puzzleGameObjects)
        {
            puzzle.GetComponent<CanvasGroup>().blocksRaycasts = false;
            puzzle.GetComponent<PuzzleDragScript>().isGoodPlace = false;
        }

        Wait(3f);

        foreach(GameObject puzzle in puzzleGameObjects)
        {
            puzzle.SetActive(false);
        }

        container.gameObject.SetActive(false);
        exit.gameObject.SetActive(true);
        text.gameObject.SetActive(true);
        replay.gameObject.SetActive(true);
        if (currTime > 300)
        {
            ocena = 1;
        }
        else if (currTime > 80)
        {
            ocena = 2;
        }else if (currTime > 60)
        {
            ocena = 3;
        }
        else if (currTime > 40)
        {
            ocena = 4;
        }
        else if (currTime > 25)
        {
            ocena = 5;
        }
        else 
        {
            ocena = 6;
        }
        text.text = "Otrzymano ocene " + ocena + " \n\n Masz jedną możliwość poprawy!";
        PlayerManager.playerManager.playerStats.oceny.Add(ocena);
        GameManager.gameManager.SetOceny();

    }
    private IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }
    private IEnumerator Timer()
    {
        yield return new WaitForSecondsRealtime(1f);
        UpdateTimer();
    }
    public void UpdateTimer()
    {
        if (isTimeRunning)
        {
            currTime++;
            timer.text = currTime.ToString();
            StartCoroutine(Timer());
        }
    }

    public void StartGame()
    {
        if (GameManager.gameManager.resits[subIndex])
        {
            currTime = 0;

            start.gameObject.SetActive(false);
            container.gameObject.SetActive(true);
            timerGameObject.SetActive(true);
            replay.gameObject.SetActive(false);
            text.gameObject.SetActive(false);
            exit.gameObject.SetActive(false);

            foreach (GameObject puzzle in puzzleGameObjects)
            {
                puzzle.SetActive(true);
                if (Random.Range(1, 3) == 1)
                {
                    puzzleX = Random.Range(-550f, -440f);
                }
                else puzzleX = Random.Range(440f, 550f);
                puzzleY = Random.Range(-300f, 300f);

                puzzle.GetComponent<RectTransform>().anchoredPosition = new Vector3(puzzleX, puzzleY);
                puzzle.GetComponent<PuzzleDragScript>().SetPuzzleDragable(true);

            }


            isTimeRunning = true;
            StartCoroutine(Timer());

        }
        else
        {
            text.gameObject.SetActive(true);
            text.text = "Osiągnięto limit popraw. Przysługuje ci tylko jedna możliwość poprawy!";
            exit.gameObject.SetActive(true);
            start.gameObject.SetActive(false);
        }
    }
    public void CloseGame()
    {

    }

}
