﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Moving")]
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] float speed;
    [SerializeField] float maxMovingVelocity;

    [Header("Rotation")]
    [SerializeField] Camera mainCamera;

    [Tooltip("Sensitivity for player rotation on X axis")]
    [SerializeField] float sensitivityX;

    [Tooltip("Sensitivity for camera rotation on Y axis")]
    [SerializeField] float sensitivityY;

    [Header( "Pickup" )]
    [SerializeField] Transform pickupPosition;
    [Tooltip( "A random transform to parent dropped pickup objects" )]
    [SerializeField] Transform pickupObjects;

    private const string mouseX = "Mouse X";
    private const string mouseY = "Mouse Y";
    private const string pickupTag = "pickup";

    GameObject pickedupItem;

    // Start is called before the first frame update
    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        RotatePlayer();
        RotateCamera();
        DropItem();
        PickupItem();
    }

    private void RotatePlayer() {
        Vector3 eulerAngles = transform.eulerAngles;
        eulerAngles.y += sensitivityX * Input.GetAxis(mouseX);
        transform.eulerAngles = eulerAngles;
    }

    private void RotateCamera() {
        Vector3 eulerAngles = mainCamera.transform.eulerAngles;
        eulerAngles.x += sensitivityY * Input.GetAxis(mouseY);
        mainCamera.transform.eulerAngles = eulerAngles;
    }

    private void MovePlayer() {
        Vector3 velocity = rigidbody.velocity;
        float accelaration = speed * Time.fixedDeltaTime;

        Vector3 moveDirection = new Vector3();

        // --- Moving Forward / Backward ---
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            moveDirection += transform.forward;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            moveDirection -= transform.forward;
        }


        // --- Moving Left / Right ---
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            moveDirection -= transform.right;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow)) {
            moveDirection += transform.right;
        }

        if (moveDirection.sqrMagnitude > 0) {
            moveDirection.Normalize();
            rigidbody.velocity = speed * moveDirection;
        }
    }

    private void PickupItem()
    {
        if ( pickedupItem == null && Input.GetMouseButtonDown( 0 ) )
        {
            RaycastHit hit;
            Vector3 fwd = mainCamera.transform.TransformDirection( Vector3.forward );
            if ( pickedupItem == null && Physics.Raycast( mainCamera.transform.position, fwd, out hit ) )
            {
                if ( hit.transform.gameObject.tag == pickupTag )
                {
                    Debug.Log( "You picked up that " + hit.transform.gameObject.name + " object." );
                    pickedupItem = hit.transform.gameObject;
                    var puRb = pickedupItem.GetComponent<Rigidbody>();
                    if ( puRb )
                        Destroy( puRb );
                    hit.transform.parent = pickupPosition;
                }
            }
        }
    }

    private void DropItem()
    {
        if ( pickedupItem != null && Input.GetMouseButtonDown( 1 ) )
        {
            pickedupItem.AddComponent<Rigidbody>();
            pickedupItem.transform.parent = pickupObjects;
            pickedupItem = null;
        }
    }

}
