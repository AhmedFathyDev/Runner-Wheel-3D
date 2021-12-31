using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float Gravity;
    public float JumpForce;
    public float LaneDistance; // The distance between two lanes.
    public float ForwardSpeed;
    public float MaxSpeed;
    public Animator Animator;

    private CharacterController controller;
    private Vector3 direction;
    private int desiredLane; // 0: left, 1: Middle, 2: right
    private bool isSliding;

    // Start is called before the first frame update
    private void Start()
    {
        Gravity = -20f;
        JumpForce = 9f;
        LaneDistance = 2.5f;
        ForwardSpeed = 10f;
        MaxSpeed = 15;

        desiredLane = 1;
        isSliding = false;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!PlayerManager.IsGameStarted || PlayerManager.GameOver)
        {
            return;
        }

        Animator.SetBool("IsGameStarted", true);
        Animator.SetBool("IsGrounded", controller.isGrounded);

        direction.z = ForwardSpeed;
        
        // Increase the speed
        if (ForwardSpeed < MaxSpeed)
        {
            ForwardSpeed += 0.1f * Time.deltaTime;
        }

        if (controller.isGrounded)
        {
            if (SwipeManager.SwipeUp)
            {
                jump();
            }
        }
        else
        {
            direction.y += Gravity * Time.deltaTime;
        }

        if (SwipeManager.SwipeDown && !isSliding)
        {
            StartCoroutine(Slide());
        }
        
        // Gather the inputs on which lane we should be.

        if (SwipeManager.SwipeRight)
        {
            desiredLane++;
            if (desiredLane == 3)
            {
                desiredLane = 2;
            }
        }
        else if (SwipeManager.SwipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
            {
                desiredLane = 0;
            }
        }

        // Calculate where we should be in the future.

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * LaneDistance;
        }
        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * LaneDistance;
        }

        // transform.position = targetPosition;
        if (transform.position != targetPosition)
        {
            Vector3 diff = targetPosition - transform.position;
            Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;

            if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            {
                controller.Move(moveDir);
            }
            else
            {
                controller.Move(diff);
            }
        }

        controller.Move(direction * Time.deltaTime);
    }

    // Update is called once at fixed period of time.
    //private void FixedUpdate()
    //{
    //    if (!PlayerManager.IsGameStarted || PlayerManager.GameOver)
    //    {
    //        return;
    //    }

    //    controller.Move(direction * Time.deltaTime);
    //}

    private void jump()
    {
        direction.y = JumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            PlayerManager.GameOver = true;
            FindObjectOfType<AudioManager>().PlaySound("GameOver");
        }
    }

    private IEnumerator Slide()
    {
        isSliding = true;
        Animator.SetBool("IsSliding", true);
        controller.center = new Vector3(0, -0.5f, 0);
        controller.height = 1;

        yield return new WaitForSeconds(1.3f);

        controller.height = 2;
        controller.center = new Vector3(0, 0, 0);
        Animator.SetBool("IsSliding", false);
        isSliding = false;
    }
}
