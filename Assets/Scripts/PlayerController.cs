using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float ForwardSpeed;
    public float LaneDistance = 4; // The distance between two lanes.

    private CharacterController controller;
    private Vector3 direction;
    private int desiredLane = 1; // 0: left, 1: Middle, 2: right

    // Start is called before the first frame update
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        direction.z = ForwardSpeed;

        // Gather the inputs on which lane we should be.

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            desiredLane++;
            if (desiredLane == 3)
            {
                desiredLane = 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
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

        transform.position = Vector3.Lerp(transform.position, targetPosition, 80 * Time.deltaTime);
    }
    // Update is called once at fixed period of time.
    private void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
    }
}
