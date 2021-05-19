using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunchPaperScript : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerManager playerManager = collision.gameObject.GetComponent<PlayerManager>();

            playerManager.playerStats.lunchCard = true;

            Destroy(gameObject);

            
        }
    }



}
