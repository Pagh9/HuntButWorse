using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1f;
    public float rotationSpeed = 1f;

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    void movement()
    {
        // if i remember correctly there is 3 ways to make movement scripts, as far as i know. 
        
        // Get horizontal mouse movement
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("MouseY");

        // Calculate the rotation amount based on mouse movement
        Vector3 rotation = Vector3.up * mouseX * rotationSpeed * Time.deltaTime;
        Vector3 rotation1 = Vector3.up * mouseY * rotationSpeed * Time.deltaTime;

        // Apply the rotation to the character
        transform.Rotate(rotation, Space.World);

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        Debug.Log("HELP ME");


    }
}
