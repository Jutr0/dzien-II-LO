using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CloseMinigame : MonoBehaviour
{
public void BackToClass(string sceneName)
    {
        GameManager.gameManager.isMiniGameOpen = false;
        SceneManager.LoadScene(sceneName);
    }
}
