using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraRaytest : MonoBehaviour
{
    public float keyboardSensitivity;
    [Header("初始物件X、Y軸角度(手動填入)")]
    public float rotX;
    public float rotY;
    private Quaternion localRotation;
    private Quaternion localRotation2;
    //private ObjectGrabber objectGrabber;

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(ray, out hit))
            {
                /*if (hit.transform.GetComponent<ObjectColliderTrigger>() != null)
                {
                    ObjectColliderTrigger colliderTrigger = hit.transform.GetComponent<ObjectColliderTrigger>();
                    colliderTrigger.OnClick();
                }*/

                //if (hit.transform.GetComponent<ObjectGrabber>() != null)
                //{
                //    objectGrabber = hit.transform.GetComponent<ObjectGrabber>();
                //    objectGrabber.HitByRaycast();                    
                //}
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            //if (objectGrabber != null)
            //{
            //    objectGrabber.ReleaseByRaycast();
            //    objectGrabber = null;
            //}
        }

        if (Input.GetMouseButton(1))
        {
            //if (objectGrabber != null)
            //{
            //    objectGrabber.ClickToSpin();
            //}
        }

        if ((Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow)))
        {
            rotY += Time.deltaTime * keyboardSensitivity;
        }
        if ((Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)))
        {
            rotY -= Time.deltaTime * keyboardSensitivity;
        }

        if ((Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow)))
        {
            rotX += Time.deltaTime * keyboardSensitivity;
        }
        if ((Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow)))
        {
            rotX -= Time.deltaTime * keyboardSensitivity;
        }

        localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;
    }
}
