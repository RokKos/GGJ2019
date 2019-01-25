using System.Collections;
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

    private const string mouseX = "Mouse X";
    private const string mouseY = "Mouse Y";

    [Header("Animation")]
    [SerializeField] Animator animator;
    private const string flipCameraUp = "FlipCameraUp";
    private const string flipCameraDown = "FlipCameraDown";
    private bool cameraUp = false;

    // Start is called before the first frame update
    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        cameraUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        RotatePlayer();
        RotateCamera();
        CheckGravity();

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

    void CheckGravity() {
        if (Input.GetKeyDown(KeyCode.G)) {
            Physics.gravity = new Vector3(0, -1 * Physics.gravity.y, 0f);
            if (!cameraUp) {
                animator.SetTrigger(flipCameraUp);
            } else{
                animator.SetTrigger(flipCameraDown);
            }
            

        }
    }





}
