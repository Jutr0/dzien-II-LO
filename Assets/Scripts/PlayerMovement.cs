using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D playerRb;
    public float moveSpeed = 10;
    Animator animator;
    AudioSource audioSource;
    public AudioClip walking;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameManager.gameManager.isMiniGameOpen && GameManager.gameManager.isGameStarted)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            animator.SetFloat("MoveX", horizontal);
            animator.SetFloat("MoveY", vertical);

            Vector2 move = new Vector2(horizontal, vertical);

            playerRb.MovePosition(playerRb.position + move * Time.fixedDeltaTime * moveSpeed);
            if (horizontal == 0 && vertical == 0)
            {
                animator.SetBool("IsMoving", false);
                audioSource.Stop();
            }
            else
            {
                animator.SetBool("IsMoving", true);
                if(!audioSource.isPlaying) audioSource.PlayOneShot(walking);

            }
        }

    }
}
