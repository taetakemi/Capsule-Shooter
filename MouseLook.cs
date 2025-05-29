using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {

    public Vector2 cameraDirection;

    public GameObject Gun;
    public GameObject Player;
    public GameObject[] Camera;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cameraDirection = transform.localRotation.eulerAngles;
    }

    void Update()
    {
        if (GameData.gameEnd) { return; }
        if (GameData.pause) { return; }

        if (Input.GetButtonDown("Change Camera")) {
            int cam = 0;
            for (int i = 0; i < Camera.Length; i++) {
                if (Camera[i].activeSelf) {
                    cam = i;
                }
                Camera[i].SetActive(false);
            }

            if (cam == 0)
            {
                Camera[1].SetActive(true);
            }
            else if (cam == 1)
            {
                Camera[0].SetActive(true);
            }
        }
    }

    void LateUpdate()
    {
        if (GameData.gameEnd) { return; }
        if (GameData.pause) { return; }

#if UNITY_STANDALONE_WIN
        cameraDirection = new Vector2(Input.GetAxisRaw("Mouse X") + cameraDirection.x,
                            Mathf.Clamp(Input.GetAxisRaw("Mouse Y") + cameraDirection.y, -50f, 35f));
        //Rotate y Axis Player
        Player.transform.localRotation = Quaternion.AngleAxis(cameraDirection.x, Vector3.up);
        //Rotate x Axis Camera
        transform.localRotation = Quaternion.AngleAxis(-cameraDirection.y, Vector3.right);
        //Rotate x Axis Gun
        //Gun.transform.localRotation = Quaternion.AngleAxis(-cameraDirection.y, Vector3.right);
#endif

#if UNITY_ANDROID
        //if (Input.touchCount >= 0)
        //    {
        //    //Store the first touch detected.
        //    Touch myTouch = Input.touches[0];

        //    //Check if the phase of that touch equals Began
        //    if (myTouch.phase == TouchPhase.Began)
        //    {
        //        //If so, set touchOrigin to the position of that touch
        //        touchOrigin = myTouch.position;
        //    }

        //    //If the touch phase is not Began, and instead is equal to Ended and the x of touchOrigin is greater or equal to zero:
        //    else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
        //        {
        //        //Set touchEnd to equal the position of this touch
        //        Vector2 touchEnd = myTouch.position;

        //        //Calculate the difference between the beginning and end of the touch on the x axis.
        //        float x = touchEnd.x - touchOrigin.x;

        //        //Calculate the difference between the beginning and end of the touch on the y axis.
        //        float y = touchEnd.y - touchOrigin.y;

        //        //Set touchOrigin.x to -1 so that our else if statement will evaluate false and not repeat immediately.
        //        touchOrigin.x = -1;

        //        //Check if the difference along the x axis is greater than the difference along the y axis.
        //        if (Mathf.Abs(x) >= Mathf.Abs(y))
        //                //If x is greater than zero, set horizontal to 1, otherwise set it to -1
        //                horizontal = x >= 0 ? 1 : -1;
        //            else
        //                //If y is greater than zero, set horizontal to 1, otherwise set it to -1
        //                vertical = y >= 0 ? 1 : -1;
        //    }
        //}
#endif
    }
}
