using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    Button newGameBtn;
    Button continueBtn;
    Button matfizBtn;
    Button biolchemBtn;
    Button menBtn;
    Button humBtn;
    Button exitBtn;
    Button logo;
    Button profil;
    // Start is called before the first frame update
    private void Start()
    {
        
        newGameBtn = GameObject.Find("New Game").GetComponent<Button>();
        continueBtn = GameObject.Find("Continue").GetComponent<Button>();
        matfizBtn = GameObject.Find("MATFIZ").GetComponent<Button>();
        biolchemBtn = GameObject.Find("BIOLCHEM").GetComponent<Button>();
        menBtn = GameObject.Find("MEN").GetComponent<Button>();
        humBtn = GameObject.Find("HUM").GetComponent<Button>();
        exitBtn = GameObject.Find("Exit").GetComponent<Button>();
        logo = GameObject.Find("Logo").GetComponent<Button>();
        profil = GameObject.Find("PROFIL").GetComponent<Button>();

        newGameBtn.gameObject.SetActive(true);
        continueBtn.gameObject.SetActive(true);
        exitBtn.gameObject.SetActive(true);
        matfizBtn.gameObject.SetActive(false);
        biolchemBtn.gameObject.SetActive(false);
        menBtn.gameObject.SetActive(false);
        humBtn.gameObject.SetActive(false);
        logo.gameObject.SetActive(true);
        profil.gameObject.SetActive(false);

    }

    // Update is called once per frame
    private void Update()
    {
        if (newGameBtn.IsActive())
        {
            matfizBtn.gameObject.SetActive(false);
            biolchemBtn.gameObject.SetActive(false);
            menBtn.gameObject.SetActive(false);
            humBtn.gameObject.SetActive(false);
            profil.gameObject.SetActive(false);
        }
        else
        {
            newGameBtn.gameObject.SetActive(false);
            continueBtn.gameObject.SetActive(false);
            //exitBtn.gameObject.SetActive(false);
            //logo.gameObject.SetActive(false);

        }
    }

    private void ChooseSchoolProfile()
    {
        newGameBtn.gameObject.SetActive(false);
        continueBtn.gameObject.SetActive(false);
        //exitBtn.gameObject.SetActive(false);
        matfizBtn.gameObject.SetActive(true);
        biolchemBtn.gameObject.SetActive(true);
        menBtn.gameObject.SetActive(true);
        humBtn.gameObject.SetActive(true);
        logo.gameObject.SetActive(false);
        profil.gameObject.SetActive(true);

    }
    public void NewGame()
    {
        ChooseSchoolProfile();
    }
    public void Continue()
    {
        PlayerManager.playerManager.Load();
        GameManager.gameManager.isGameStarted = true;
        GameManager.gameManager.LoadLastLevel();
    }
    public void Exit()
    {
        GameManager.gameManager.Exit();
    }
    public void StartWithProfile(GameObject button)
    {
        if (button.name.Equals("MATFIZ"))
        {
            PlayerManager.playerManager.playerStats.profil = Profil.MATFIZ;
        }
        else if (button.name.Equals("BIOLCHEM"))
        {
            PlayerManager.playerManager.playerStats.profil = Profil.BIOCHEM;
        }
        else if (button.name.Equals("MEN"))
        {
            PlayerManager.playerManager.playerStats.profil = Profil.MEN;
        }
        else if (button.name.Equals("HUM"))
        {
            PlayerManager.playerManager.playerStats.profil = Profil.HUM;
        }
        else Debug.Log("blad!");
        PlayerManager.playerManager.Save();
        GameManager.gameManager.isGameStarted = true;
        GameManager.gameManager.LoadFirstLevel();
        
    }

}
