using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public Camera followCamera;
    [SerializeField] private float speed;
    private Rigidbody rb;
    private Vector3 m_CameraPos;
    private Vector3 direction;
    private bool isLock;

    void Start()
    {
        isLock = false;
        rb = GetComponent<Rigidbody>();
        m_CameraPos = followCamera.transform.position - transform.position;
    }

    void FixedUpdate()
    {
        if (isLock)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));


        if (direction == Vector3.zero)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        direction.Normalize();
        VelocityMovement(direction);
        RotateOnMove(direction);

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

    public void ToLock()
    {
        isLock = true;
    }

}
