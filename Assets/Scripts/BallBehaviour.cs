using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            Goal(true);
            PEMinigame.peminigame.ballMove = Vector3.zero;
        }
        else if (collision.gameObject.CompareTag("Goalkeeper") || collision.gameObject.CompareTag("Out"))
        {
            Goal(false);
            PEMinigame.peminigame.ballMove = Vector3.zero;

        }
    }
    private void Goal(bool isGoal)
    {

        if (isGoal)
        {
            PEMinigame.peminigame.score++;
            PEMinigame.peminigame.UpdateScore();
        }
        else
        {

        }
        PEMinigame.peminigame.NewShoot();

    }    
}
