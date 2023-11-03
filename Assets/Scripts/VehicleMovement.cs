using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    public Rigidbody sphereRb;
    private float moveInput;
    private float turnInput;


    [SerializeField] float fwdSpeed;
    [SerializeField] float revSpeed;
    [SerializeField] float turnSpeed;
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }

    private void Start()
    {
        sphereRb.transform.parent = null;
    }

    private void OnEnable()
    {
        playerInputActions.Player.Move.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Player.Move.Disable();
    }



    private void Update()
    {
        transform.position = sphereRb.transform.position;
        turnInput = playerInputActions.Player.Move.ReadValue<Vector2>().x;
        moveInput = playerInputActions.Player.Move.ReadValue<Vector2>().y;
        moveInput *= moveInput > 0 ? fwdSpeed : revSpeed;

        float newRotation = turnInput * turnSpeed * Time.deltaTime * playerInputActions.Player.Move.ReadValue<Vector2>().y;
        transform.Rotate(0, newRotation, 0, Space.World);
    }

    private void FixedUpdate()
    {
        sphereRb.AddForce(transform.forward * moveInput, ForceMode.Acceleration);
    }
}
