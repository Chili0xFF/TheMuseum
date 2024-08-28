using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerManager : MonoBehaviour
{
    [Header("Player settings")]
    [Range(0.1f,5f)]
    public float mouseSensitivity = 1f;
    public float speed = 1f;
    public float interactionDistance = 1f;
    public LayerMask interactionLayer;
    
    [Header("External")]
    [SerializeField] Camera _playerMainCamera;
    [SerializeField] Transform orientation;
    
    [Header("Internal")]
    PlayerInputActions playerInputActions;
    private Rigidbody rb;
    private InteractiveObject detectedObject;
    private InteractiveObject newDetectedObject;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInputActions = new();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump;
        playerInputActions.Player.Interact.performed += Interact;
        Cursor.lockState = CursorLockMode.Locked;


    }
    private Vector2 turn;
    private void Update()
    {
        #region Movement TO BE REMADE
        Debug.LogWarning("This section is to be remade!");
        Vector2 movement = new();
        
        if (isGrounded) movement = playerInputActions.Player.Move.ReadValue<Vector2>();
        movement.x *= -1;
        Vector3 movementRotated = Quaternion.Euler(0, -90, 0)*(orientation.transform.forward * movement.x + orientation.transform.right * movement.y);
        this.gameObject.transform.position += movementRotated * speed*0.01f;
        #endregion
        #region Looking around
        turn.x += Input.GetAxis("Mouse X") * mouseSensitivity;
        turn.y += Input.GetAxis("Mouse Y") * mouseSensitivity;
        _playerMainCamera.transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
        orientation.transform.localRotation = Quaternion.Euler(0, turn.x, 0);
        #endregion
        #region Interactive detection
        var ray = new Ray(_playerMainCamera.transform.position, _playerMainCamera.transform.forward);
        Debug.DrawRay(_playerMainCamera.transform.position, _playerMainCamera.transform.forward, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactionDistance, interactionLayer))
        {
            newDetectedObject = hit.transform.gameObject.GetComponent<InteractiveObject>();
            if (newDetectedObject != detectedObject)
            {
                detectedObject?.StopDetected();
                detectedObject = newDetectedObject;
                detectedObject.StartDetected();
            }
        }
        else if(detectedObject!=null){
            detectedObject.StopDetected();
            detectedObject = null;
        }
        #endregion

    }
    #region Interaction handling
    private void Interact(InputAction.CallbackContext callbackContext) {
        if (detectedObject == null) return;

        detectedObject.StartAction();
    }
    #endregion
    #region Jumping
    bool canJump = true;
    bool isGrounded = true;
    public void Jump(InputAction.CallbackContext callbackContext) {
        if (!canJump) return;
        rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        canJump = false;
        isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canJump = true;
            isGrounded = true;
        }
    }
    #endregion
}
