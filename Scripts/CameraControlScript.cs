using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlScript : MonoBehaviour
{
    private Vector3 offset;
    private Quaternion WhiteDefaultOrientation;
    private Quaternion BlackDefaultOrientation;

    public float TurnSpeed;
    public BoardManager board;
    public bool AutoRotate;
    private bool isRotating;
    private Quaternion curRot;
    private Quaternion endRot;
    private float rotInterp;

    public static CameraControlScript instance;

    public SimpleTouchPad touchPad;
    private Quaternion calibrationQuaternion;
    private Vector2 direction;

    // Use this for initialization
    void Start ()
    {
        offset = Camera.main.transform.position - transform.position;
        WhiteDefaultOrientation = Quaternion.identity;
        BlackDefaultOrientation = Quaternion.Euler(0, 180, 0);
        instance = this;
        AutoRotate = true;
        rotInterp = 0;
        CalibrateAccelerometer();
    }

    // Update is called once per frame
    void Update()
    {
        float xPos = Input.GetAxisRaw("Mouse X");
        float yPos = -Input.GetAxisRaw("Mouse Y");


        if (Input.GetMouseButtonDown(2))
        {
            ResetCamera();
        }

        if (Input.GetMouseButton(1))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + (yPos * TurnSpeed * Time.deltaTime), transform.rotation.eulerAngles.y + (xPos * TurnSpeed * Time.deltaTime), transform.rotation.eulerAngles.z);
        }

        if (Input.touchCount > 0)
        {
            xPos = Input.touches[0].deltaPosition.x;
            yPos = Input.touches[0].deltaPosition.y;
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + (yPos * TurnSpeed * Time.deltaTime), transform.rotation.eulerAngles.y + (xPos * TurnSpeed * Time.deltaTime), transform.rotation.eulerAngles.z);
            }
        }

        if (isRotating)
        {
            transform.rotation = Quaternion.Slerp(curRot, endRot, rotInterp);
            rotInterp += (Time.deltaTime * 2);
        }

        if (rotInterp > 1.0f)
        {
            isRotating = false;
            rotInterp = 0;
        }
    }

        //Used to calibrate the Iput.acceleration input
        void CalibrateAccelerometer()
    {
            Vector3 accelerationSnapshot = Input.acceleration;
            Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), accelerationSnapshot);
            calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
        }
    

        //Get the 'calibrated' value from the Input
        Vector3 FixAcceleration(Vector3 acceleration)
    {
            Vector3 fixedacceleration = calibrationQuaternion * acceleration;
            return fixedacceleration;
        }


    void ResetCamera()
    {
        curRot = transform.rotation;
        isRotating = true;
        if (board.isWhiteTurn)
        {
            //transform.rotation = WhiteDefaultOrientation;
            //Camera.main.transform.position = transform.position + offset;
            endRot = WhiteDefaultOrientation;
        }
        else
        {
            //transform.rotation = BlackDefaultOrientation;
            //Camera.main.transform.position = transform.position - offset;
            endRot = BlackDefaultOrientation;
        }

    }

    public void MoveCameraToStartingPosition(bool isWhiteTurn)
    {
        
        
        if (AutoRotate)
        {
            curRot = transform.rotation;
            isRotating = true;
            if (isWhiteTurn)
            {
                //transform.rotation = WhiteDefaultOrientation;
                endRot = WhiteDefaultOrientation;
            }
            else
            {
                //transform.rotation = BlackDefaultOrientation;
                endRot = BlackDefaultOrientation;
            }

            //Camera.main.transform.position = transform.position + offset;
        }
    }

    public void SetAutoRotate()
    {
        AutoRotate = !AutoRotate;
    }

    /*IEnumerator RotateCamera(Quaternion start, Quaternion end)
    {
        float rotInterp = 0;

        while (transform.rotation != end)
        {
            transform.Rotate(new Vector3(0, 5, 0));
        }
        yield return null;

    }*/
}
