using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {
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

    [Tooltip("Max angle for camera rotation")]
    [SerializeField] float cameraRotationAgleMax;

    [SerializeField] Transform flashLightTransform;


    [Header( "Pickup" )]
    [Tooltip("Where the picked up object is held.")]
    [SerializeField] Transform pickupPosition;
    [Tooltip("Camera crosshair.")]
    [SerializeField] GameObject pickupCrosshair;
    [Tooltip( "A random transform to parent dropped pickup objects. If none, objects will just be added back to the scene." )]
    [SerializeField] Transform pickupObjects;

    [SerializeField] float rayCastDistance = 2.0f;

    private const string mouseX = "Mouse X";
    private const string mouseY = "Mouse Y";
    private const string pickupTag = "pickup";

    GameObject pickedupItem;

    [Header("Animation")]
    [SerializeField] Animator animator;
    private const string flipCameraUp = "FlipCameraUp";
    private const string flipCameraDown = "FlipCameraDown";
    private bool cameraUp = true;

    // Start is called before the first frame update
    void Start()
    {
        
        Cursor.lockState = CursorLockMode.Locked;
        cameraUp = true;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        RotatePlayer();
        RotateCamera();
        RotateLight();
        DropItem();
        PickupItem();
        CheckGravity();
    }

    private void RotatePlayer() {
        int rotation = cameraUp ? 1 : -1;
        Vector3 eulerAngles = transform.eulerAngles;
        eulerAngles.y += sensitivityX * rotation * Input.GetAxis(mouseX);
        transform.eulerAngles = eulerAngles;
    }

    private void RotateCamera() {
        int rotation = cameraUp ? 1 : -1;
        Vector3 eulerAngles = mainCamera.transform.eulerAngles;
        eulerAngles.x += sensitivityY * rotation * Input.GetAxis(mouseY);

        // This hack is here because Unity has weird euler angles that loop around and doens't differentiate between negative and positive sign
        if (eulerAngles.x > 300 && eulerAngles.x - 360 < -cameraRotationAgleMax + 5) {
                Debug.Log("Less Than");
               eulerAngles.x = -cameraRotationAgleMax + 5;
        }
        else if (eulerAngles.x > cameraRotationAgleMax + 5 && eulerAngles.x < 300)  {
            Debug.Log("More Than");
            eulerAngles.x = cameraRotationAgleMax + 5;
        }
        mainCamera.transform.eulerAngles = eulerAngles;
    }

    private void RotateLight() {
        int rotation = cameraUp ? 1 : -1;
        Vector3 eulerAngles = flashLightTransform.eulerAngles;
        eulerAngles.x += sensitivityY * Input.GetAxis(mouseY);

        // This hack is here because Unity has weird euler angles that loop around and doens't differentiate between negative and positive sign
        if (eulerAngles.x > 300 && eulerAngles.x - 360 < -cameraRotationAgleMax + 5) {
            Debug.Log("Less Than");
            eulerAngles.x = -cameraRotationAgleMax + 5;
        } else if (eulerAngles.x > cameraRotationAgleMax + 5 && eulerAngles.x < 300) {
            Debug.Log("More Than");
            eulerAngles.x = cameraRotationAgleMax + 5;
        }

        flashLightTransform.eulerAngles = eulerAngles;
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

            cameraUp = !cameraUp;   
        }
    }

    private void PickupItem()
    {
        if ( pickedupItem == null && Input.GetMouseButtonDown( 0 ) )
        {
            RaycastHit hit;
            Vector3 fwd = mainCamera.transform.TransformDirection( Vector3.forward );
            if ( Physics.Raycast( mainCamera.transform.position, fwd, out hit, rayCastDistance) )
            {
                if ( hit.transform.gameObject.tag == pickupTag )
                {
                    Debug.Log( "You picked up that " + hit.transform.gameObject.name + " object." );
                    pickedupItem = hit.transform.gameObject;
                    var puRb = pickedupItem.GetComponent<Rigidbody>();
                    if ( puRb )
                        Destroy( puRb );
                    hit.transform.parent = pickupPosition;
                    if (pickupCrosshair != null)
                        pickupCrosshair.SetActive( false );
                }
                else
                {
                    Debug.Log( "Something else was hit: " + hit.transform.gameObject.name );
                }
            }
        }
    }

    private void DropItem()
    {
        if ( pickedupItem != null && Input.GetMouseButtonDown( 1 ) )
        {
            Debug.Log( "dropped " + pickedupItem.name );
            pickedupItem.AddComponent<Rigidbody>();
            pickedupItem.transform.parent = pickupObjects;
            pickedupItem = null;
            if (pickupCrosshair != null)
                pickupCrosshair.SetActive( true );
        }
    }


}
