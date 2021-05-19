using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{

    public string sceneName;
    public Vector2 positions;
    GameObject player;

    private void Start()
    {
    }

    public void LoadMyScene() {

        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene(sceneName);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.gameManager.playerPos = positions;
            LoadMyScene();
            
        }
    }
    private void OnLevelWasLoaded(int level)
    {
        player = GameObject.Find("Player");
        player.transform.position = GameManager.gameManager.playerPos;
        GameManager.gameManager.GetOceny();
        
    }

}
