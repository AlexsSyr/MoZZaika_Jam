using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    public Camera followCamera;
    [SerializeField] private float speed;
    private Rigidbody rb;
    private Vector3 m_CameraPos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        m_CameraPos = followCamera.transform.position - transform.position;
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (direction != Vector3.zero)
        {
            direction.Normalize();
            VelocityMovement(direction);
            RotateOnMove(direction);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }



    private void VelocityMovement(Vector3 direction)
    {
        rb.velocity = direction * speed ;
    }


    private void RotateOnMove(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        targetRotation = Quaternion.RotateTowards(
        transform.rotation,
        targetRotation,
        360 * Time.fixedDeltaTime);
        rb.MoveRotation(targetRotation);
    }

    private void LateUpdate()
    {
        followCamera.transform.position = rb.position + m_CameraPos;
    }

}
