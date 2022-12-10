using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    private Rigidbody rb;
    private CharacterController controller;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = gameObject.AddComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        VelocityMovement();
    }

    //void ForceMovement()
    //{
    //    float moveHorizontal = Input.GetAxis("Horizontal");
    //    float moveVertical = Input.GetAxis("Vertical");

    //    Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
    //    movement.Normalize();
    //    rb.AddForce(movement * speed);
    //}

    void VelocityMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        direction.Normalize();
        rb.velocity = direction * speed ;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            targetRotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            360 * Time.fixedDeltaTime);
            rb.MoveRotation(targetRotation);

        }


    }

    //void MoveMovement()
    //{
    //    bool groundedPlayer = controller.isGrounded;

    //    Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    //    move.Normalize();
    //    controller.Move(move * Time.deltaTime * speed);

    //    if (move != Vector3.zero)
    //    {
    //        gameObject.transform.forward = move;
    //    }

    //}


}
