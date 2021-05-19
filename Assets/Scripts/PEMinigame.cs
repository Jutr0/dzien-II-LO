using TMPro;
using UnityEngine;

public class PEMinigame : MonoBehaviour
{
    public static PEMinigame peminigame;
    GameObject arrow;
    GameObject ball;
    [HideInInspector]
    public Vector3 ballMove;
    Vector3 arrowRotate;
    public float rotateValue;
    public int rotateSwitch = 1;
    public float ballSpeed;
    public TextMeshProUGUI scoreHUD;

    Transform up;
    Transform down;

    public int score = 0;


    // Start is called before the first frame update
    void Start()
    {
        peminigame = this;
        arrow = GameObject.Find("arrow");
        ball = GameObject.Find("ball");
        arrowRotate = new Vector3(0, 0, rotateValue * rotateSwitch);


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        arrowRotate += new Vector3(0, 0, rotateValue * rotateSwitch);
        arrow.transform.rotation = Quaternion.Euler(arrowRotate);
        if (arrowRotate.z.Equals(60))
        {
            rotateSwitch = -rotateSwitch;
        }
        else if (arrowRotate.z.Equals(-60))
        {
            rotateSwitch = -rotateSwitch;
        }
        ball.transform.position += ballMove * Time.deltaTime * ballSpeed;
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {

            rotateSwitch = 0;
            up = arrow.GetComponentsInChildren<Transform>()[1];
            down = arrow.GetComponentsInChildren<Transform>()[2];

            ballMove = up.position - down.position;


        }

    }
    public void NewShoot()
    {
        rotateSwitch = 1;
        ball.transform.position = arrow.transform.position;
        ballMove = Vector3.zero;
        arrowRotate = Vector3.zero;
    }
    public void UpdateScore()
    {
        scoreHUD.text = score.ToString();
    }

}
