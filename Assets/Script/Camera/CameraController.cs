using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [System.Serializable]
    public class CamSetting
    {
        public float moveSpeed = 5;
        public float mouseX_Sensivity = 5;
        public float mouseY_Sensivity = 5;
        public float minClamp = -30;
        public float maxClamp = 90;
        public float rotateSpeed = 5;
        public float zoomFiledOfView = 30;
        public float origrinalZoomFiledOfView = 60;
        public float zommSpeed = 5;

    }
    [SerializeField]
    CamSetting camSettings;

    [System.Serializable]
    public class CamInputSetting
    {
        public string mouseXAxis = "Mouse X";
        public string mouseYAxis = "Mouse Y";
        public string amiInput = "Fire2";

    }
    [SerializeField]
    CamInputSetting camInputSettings;

    Camera mainCam;
    Camera UICam;
    Transform center;
    Transform target;

    float camXRotate = 0, camYRotate = 0;
    void Start()
    {
        mainCam = Camera.main;
        center = transform.GetChild(0);
        target = GameObject.FindGameObjectWithTag("Player").transform;
        UICam = mainCam.GetComponentInChildren<Camera>();
    }

    void Update()
    {
        if (!target)
        {
            return;
        }
        rotateCam();
        zoomCam();
    }
    private void LateUpdate()
    {
        if (target)
        {
            FollowPlayer();
        }else
        {
            findPlayer();
        }
    }
    void FollowPlayer()
    {
        Vector3 moveVector = Vector3.Lerp(transform.position, target.transform.position,
            Time.deltaTime * camSettings.moveSpeed);
        transform.position = moveVector;
    }
    void findPlayer()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void rotateCam()
    {
        camXRotate += (Input.GetAxis(camInputSettings.mouseYAxis) * camSettings.mouseX_Sensivity) * -1;
        camYRotate += (Input.GetAxis(camInputSettings.mouseXAxis) * camSettings.mouseY_Sensivity);
        camXRotate = Mathf.Clamp(camXRotate, camSettings.minClamp, camSettings.maxClamp);
        camYRotate = Mathf.Repeat(camYRotate, 360);
        Vector3 rotatingAngle = new Vector3(camXRotate, camYRotate, 0);
        Quaternion rotate = Quaternion.Slerp(center.transform.localRotation, 
            Quaternion.Euler(rotatingAngle), camSettings.rotateSpeed * Time.deltaTime);
        center.transform.localRotation = rotate;
    }
    void zoomCam()
    {
        if (Input.GetButton(camInputSettings.amiInput))
        {
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, camSettings.zoomFiledOfView,
                camSettings.zommSpeed * Time.deltaTime);
            UICam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, camSettings.zoomFiledOfView,
                camSettings.zommSpeed * Time.deltaTime);
        }
        else
        {
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, camSettings.origrinalZoomFiledOfView,
                camSettings.zommSpeed * Time.deltaTime);
            UICam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, camSettings.origrinalZoomFiledOfView,
                camSettings.zommSpeed * Time.deltaTime);
        }
    }
}
