using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject leftPaddle;
    public GameObject rightPaddle;
    public GameObject ball;
    public float paddleSpeed;
    public float ballSpeed;
    public float ballSpeedMulti;
    InputAction moveRight;
    InputAction moveLeft;
    Rigidbody rightRB;
    Rigidbody leftRB;
    float paddleHeight;
    public float moveForce;
    void Start()
    {
        moveRight = InputSystem.actions.FindAction("RightMove");
        moveLeft = InputSystem.actions.FindAction("LeftMove");
        rightRB = rightPaddle.GetComponent<Rigidbody>();
        leftRB = leftPaddle.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rightRB.AddForce(new Vector3(0,moveRight.ReadValue<float>()*moveForce,0));
        leftRB.AddForce(new Vector3(0, moveLeft.ReadValue<float>() * moveForce, 0));
        if (Mathf.Abs(rightRB.linearVelocity.y) > Mathf.Abs(moveRight.ReadValue<float>() * paddleSpeed))
        {
            rightRB.linearVelocity = new Vector3(0, moveRight.ReadValue<float>() * paddleSpeed, 0);
        }
        if (Mathf.Abs(leftRB.linearVelocity.y) > Mathf.Abs(moveLeft.ReadValue<float>() * paddleSpeed))
        {
            leftRB.linearVelocity = new Vector3(0, moveLeft.ReadValue<float>() * paddleSpeed, 0);
        }
    }
}
