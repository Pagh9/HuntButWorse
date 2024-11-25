using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 150f;
    private bool isGrounded;
    public float jumpForce = 10;

    public Rigidbody rb;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        movement();
        Rotation();
        jump();
    }

    void movement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.Translate(movement * speed * Time.deltaTime, Space.Self);

        


    }

    void Rotation()
    {
        // Get horizontal mouse movement
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Calculate the rotation amount based on mouse movement
        Vector3 rotationHorizontal = Vector3.up * mouseX * rotationSpeed * Time.deltaTime;
        Vector3 rotationVertical = Vector3.up * mouseY * rotationSpeed * Time.deltaTime;

        // Apply the rotation to the character
        transform.Rotate(rotationHorizontal, Space.World);

        transform.Rotate(rotationVertical, Space.Self);
        
    }

    void jump()
    {
       if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            Debug.Log("I JUMPED BITHCES!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
