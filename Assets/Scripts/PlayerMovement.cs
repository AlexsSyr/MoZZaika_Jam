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
        VelocityMovement();
    }



    void VelocityMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        direction.Normalize();
        rb.velocity = direction * speed ;
    }

    private void LateUpdate()
    {
        followCamera.transform.position = rb.position + m_CameraPos;
    }

}
