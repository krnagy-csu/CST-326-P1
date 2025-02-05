using UnityEngine;
using UnityEngine.InputSystem;

public class BallScript : MonoBehaviour
{
    public GameObject leftPaddle;
    public GameObject rightPaddle;
    public float ballSpeed;
    float ballSpeedMulti;
    public bool started;
    float paddleHeight;
    Rigidbody myRB;
    public float bounceAngle = 75f;  
    bool lastHit = true; //lastHit true = shoot towards right wall, false = shoot towards left wall
    float scoreLeft;
    float scoreRight;
    public GameObject rightScoreText;
    public GameObject leftScoreText;
    public GameObject winnerText;
    public GameObject rightWinText;
    public GameObject leftWinText;
    public GameObject rightScoredNote;
    public GameObject leftScoredNote;
    bool won = false;
    InputAction start;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        paddleHeight = leftPaddle.transform.localScale.y / 2;
        myRB = gameObject.GetComponent<Rigidbody>();
        scoreLeft = 0;
        scoreRight = 0;
        start = InputSystem.actions.FindAction("StartGame");
        startGame();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((!started) && (start.ReadValue<float>() > 0.1f)) {
            started = true;
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            rightScoredNote.SetActive(false);
            leftScoredNote.SetActive(false);
            launchBall();
            if (won == true)
            {
                scoreLeft = 0;
                scoreRight = 0;
                won = false;
                rightWinText.SetActive(false);
                leftWinText.SetActive(false);
                winnerText.SetActive(false);
                rightScoreText.GetComponent<TMPro.TextMeshProUGUI>().text = scoreRight.ToString();
                leftScoreText.GetComponent<TMPro.TextMeshProUGUI>().text = scoreLeft.ToString();
            }
        }
    }

    void startGame()
    {
        ballSpeedMulti = 1;
        if (Random.Range(0,2) >= 1)
        {
            lastHit = true;
        }
        launchBall();
    }
    void launchBall()
    {
        gameObject.transform.position = new Vector3(0, 0, 0);
        ballSpeedMulti = 1;
        if (lastHit)
        {
            myRB.linearVelocity = new Vector3(ballSpeed * ballSpeedMulti, 0, 0);
        }
        else
        {
            myRB.linearVelocity = new Vector3(-ballSpeed * ballSpeedMulti, 0, 0);
        }
        started = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit " + other.name);
        if (other.CompareTag("SideWall"))
        {
            myRB.linearVelocity = new Vector3(myRB.linearVelocity.x, -myRB.linearVelocity.y, myRB.linearVelocity.z);
        }
        if (other.CompareTag("Paddle"))
        {
            float angle = (bounceAngle * (gameObject.transform.position.y - other.transform.position.y) / paddleHeight);
            Mathf.Clamp(angle, -bounceAngle, bounceAngle);
            if (angle > bounceAngle)
            {
                angle = bounceAngle;
            }
            else if (angle < -bounceAngle)
            {
                angle = -bounceAngle;
            }
            Debug.Log(angle);
            ballSpeedMulti *= 1.2f;
            if (other.name == "PaddleLeft")
            {
                myRB.linearVelocity = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle) * (ballSpeed * ballSpeedMulti), Mathf.Sin(Mathf.Deg2Rad * angle) * (ballSpeed * ballSpeedMulti), 0);
            }
            else
            {
                myRB.linearVelocity = new Vector3(-Mathf.Cos(Mathf.Deg2Rad * angle) * (ballSpeed * ballSpeedMulti), Mathf.Sin(Mathf.Deg2Rad * angle) * (ballSpeed * ballSpeedMulti), 0);
            }
        }
        if (other.CompareTag("GoalRight"))
        {
            scoreLeft++;
            lastHit = true;
            Debug.Log("Left scored! Now at " + scoreLeft.ToString());
            leftScoreText.GetComponent<TMPro.TextMeshProUGUI>().text = scoreLeft.ToString();
            started = false;
            myRB.linearVelocity = Vector3.zero;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            if (scoreLeft >= 11)
            {
                won = true;
                leftWinText.SetActive(true);
                winnerText.SetActive(true);
            }
            else
            {
                leftScoredNote.SetActive(true);
            }
        }
        if (other.CompareTag("GoalLeft"))
        {
            scoreRight++;
            lastHit = false;
            Debug.Log("Right scored! Now at " + scoreRight.ToString());
            rightScoreText.GetComponent<TMPro.TextMeshProUGUI>().text = scoreRight.ToString();
            started = false;
            myRB.linearVelocity = Vector3.zero;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            if (scoreRight >= 11)
            {
                won = true;
                rightWinText.SetActive(true);
                winnerText.SetActive(true);
            }
            else
            {
                rightScoredNote.SetActive(true);
            }
        }
    }
}
