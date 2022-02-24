using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField] float playerSpeed = 10.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private Transform playerTransform;
    private float yamn=0f;
    private float pitch = 0f;
    private float sensi = 6f;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerTransform = transform;
        controller = gameObject.AddComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 currentPosition = playerTransform.position;
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = (
            playerTransform.right * Input.GetAxis("Horizontal")
            + playerTransform.forward * Input.GetAxis("Vertical")
        );
        move.y = 0;
        //Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            
            gameObject.transform.position = move;
        }
        yamn += Input.GetAxis("Mouse X")*sensi;
        pitch -= Input.GetAxis("Mouse Y")*sensi;
        pitch = Mathf.Clamp(pitch, -90f, 90f);
        transform.eulerAngles = new Vector3(pitch, yamn, 0f);
        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        
    }
}
