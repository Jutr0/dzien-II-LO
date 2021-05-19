using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public bool isMiniGameOpen = false;
    public string firstLevel;
    public string lastLevel;
    public bool isGameStarted = false;
    public Vector2 playerPos;
    public List<int> oceny;
    public bool[] resits = new bool [9];
    public Dictionary<string, byte> subjects = new Dictionary<string, byte>();
    public int informaticsResit = 0;
    public int geographyResit = 0;
    [SerializeField]
    public PlayerStats playerStats;
    public int ileOcen = 0;
    public float srednia = 0;


    // Start is called before the first frame update
    void Start()
    {
        if (gameManager == null)
        {
            gameManager = this;
            DontDestroyOnLoad(gameManager);
        }
        else
        {
            Destroy(gameObject);
        }

        subjects.Add("Math", 0);
        subjects.Add("Polish", 1);
        subjects.Add("Music", 2);
        subjects.Add("History", 3);
        subjects.Add("Chemistry", 4);
        subjects.Add("Biology", 5);
        subjects.Add("Physics", 6);
        subjects.Add("IT", 7);
        subjects.Add("Geography", 8);

        for(int i=0;i<9;i++){
            resits[i] = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene(firstLevel);
    }
    public void LoadLastLevel()
    {
        if (PlayerManager.playerManager.playerStats.currentScene != null)
        {
            lastLevel = PlayerManager.playerManager.playerStats.currentScene;
            SceneManager.LoadScene(lastLevel);
        }
    }
    public void Exit()
    {
        Application.Quit();    
    }
    public List<int> GetOceny()
    {
        PlayerManager.playerManager.playerStats.oceny = oceny;
        PlayerManager.playerManager.RefreshStats();
        return oceny;
    }
    public void SetOceny(List<int> oceny)
    {
        this.oceny = oceny;
    }
    public void SetOceny()
    {
        this.oceny = PlayerManager.playerManager.playerStats.oceny;
    }
    

}
