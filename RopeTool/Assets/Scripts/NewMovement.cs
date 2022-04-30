using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMovement : MonoBehaviour
{
    // Game State
  //  [SerializeField] GameMenuManager manager;

    //Movement Speeds
    [SerializeField] float vMove = 20f;
    [SerializeField] float lookRotateSpeed = 100f;

    //Acceleration
    private float aForward = 2f;
    private float aSide = 2f;
    private float aMouse = 2f;

    //Current Velocity
    [SerializeField] private Vector3 vMoveCurrent;
    [SerializeField] private float vMouseCurrentx;
    [SerializeField] private float vMouseCurrenty;
    [SerializeField] private Vector3 moveDirection;

    //Camera Rotate
    private Vector2 lookInput;
    private Vector2 screenCenter;
    private Vector2 mouseDistance;

    //Forces
    Rigidbody rb;

    //Rockets
    GameObject Left;
    GameObject Right;
    GameObject Front;
    GameObject Back;
    GameObject Top;
    GameObject Bottom;

    //Materials
    [SerializeField] Material inactiveMat;
    [SerializeField] Material activeMat;
   

    // Start is called before the first frame update
    void Start()
    {

        Cursor.visible = false;
        screenCenter.x = Screen.width / 2;
        screenCenter.y = Screen.height / 2;
        rb = gameObject.GetComponent<Rigidbody>();

        Left = GameObject.Find("Left");
        Right = GameObject.Find("Right");
        Front = GameObject.Find("Front");
        Back = GameObject.Find("Back");
        Top = GameObject.Find("Top");
        Bottom = GameObject.Find("Bottom");
    }

    // Update is called once per frame
    void Update()
    {
        //LOOK DIRECTION

        //Get Mouse Location on Screen
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;
        mouseDistance.x = Mathf.Pow((lookInput.x - screenCenter.x) / screenCenter.y, 3);
        mouseDistance.y = Mathf.Pow((lookInput.y - screenCenter.y) / screenCenter.y, 3);
        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);
        //Calculate Linear Interpolation for Input
        vMouseCurrentx = Mathf.Lerp(vMouseCurrentx, mouseDistance.x, aMouse * Time.deltaTime);
        vMouseCurrenty = Mathf.Lerp(vMouseCurrenty, mouseDistance.y, aMouse * Time.deltaTime);
        //Rotation code for each axis, x and y are controlled by the mouse and z is controlled by pressing A and D
        transform.Rotate(-vMouseCurrenty * lookRotateSpeed * Time.deltaTime, vMouseCurrentx * lookRotateSpeed * Time.deltaTime, 0.0f, Space.Self);


        //MOVEMENT CODE

        if (Back.activeSelf)
        {
            if (Input.GetAxis("Z") == 1)
            {
                moveDirection += Back.transform.forward;
                Back.GetComponent<MeshRenderer>().material = activeMat;
            }
            else
            {
                Back.GetComponent<MeshRenderer>().material = inactiveMat;
            }
        }
        if (Front.activeSelf)
        {
            if (Input.GetAxis("Z") == -1)
            {
                moveDirection += Front.transform.forward;
                Front.GetComponent<MeshRenderer>().material = activeMat;
            }
            else
            {
                Front.GetComponent<MeshRenderer>().material = inactiveMat;
            }
        }
        if (Left.activeSelf)
        {
            if (Input.GetAxis("X") == 1)
            {
                moveDirection += Left.transform.forward;
                Left.GetComponent<MeshRenderer>().material = activeMat;
            }
            else
            {
                Left.GetComponent<MeshRenderer>().material = inactiveMat;
            }
        }
        if (Right.activeSelf)
        {
            if (Input.GetAxis("X") == -1)
            {
                moveDirection += Right.transform.forward;
                Right.GetComponent<MeshRenderer>().material = activeMat;
            }
            else
            {
                Right.GetComponent<MeshRenderer>().material = inactiveMat;
            }
        }
        if (Bottom.activeSelf)
        {
            if (Input.GetAxis("Y") == 1)
            {
                moveDirection += Bottom.transform.forward;
                Bottom.GetComponent<MeshRenderer>().material = activeMat;
            }
            else
            {
                Bottom.GetComponent<MeshRenderer>().material = inactiveMat;
            }
        }
        if (Top.activeSelf)
        {
            if (Input.GetAxis("Y") == -1)
            {
                moveDirection += Top.transform.forward;
                Top.GetComponent<MeshRenderer>().material = activeMat;
            }
            else
            {
                Top.GetComponent<MeshRenderer>().material = inactiveMat;
            }
        }

        moveDirection = moveDirection.normalized;

        vMoveCurrent.x = Mathf.Lerp(vMoveCurrent.x, Mathf.Abs(Input.GetAxisRaw("X")) * vMove, aForward * Time.deltaTime);
        Debug.Log(vMoveCurrent.x);
        vMoveCurrent.y = Mathf.Lerp(vMoveCurrent.y, Mathf.Abs(Input.GetAxisRaw("Y")) * vMove, aForward * Time.deltaTime);
        vMoveCurrent.z = Mathf.Lerp(vMoveCurrent.z, Mathf.Abs(Input.GetAxisRaw("Z")) * vMove, aForward * Time.deltaTime);

        //vMoveCurrent.x = Mathf.Lerp(vMove, vMoveCurrent.x, aForward * Time.deltaTime); //W and S for forward/back

        //Calculate Forward Velcoity and move with rigidbody velocity
        //rb.velocity = new Vector3(transform.forward.x * vMoveCurrent, transform.forward.y * vMoveCurrent, transform.forward.z * vMoveCurrent);
        rb.velocity = new Vector3(moveDirection.x * vMoveCurrent.x, moveDirection.y * vMoveCurrent.y, moveDirection.z * vMoveCurrent.z);

    }
}
