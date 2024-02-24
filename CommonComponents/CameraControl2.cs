using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;

public enum CamState { 
    overlook,
    freelook,
    firstperson
}
public class CameraControl2 : MonoBehaviour
{
    //高度
    public float minHeight = 5f;  //最小高度
    public float maxHeight = 300f;  //最大高度
    //角度
    public float minAngleX = 0f;   //最小角度
    public float maxAngleX = 180f;  //最大角度
    //速度
    public float minSpeed = 80f;   //最小速度
    public float maxSpeed = 300f;  //最大速度
    public float rotateSpeed = 100;    //旋转速度
    public float scrollSpeed = 20;    //滚轮速度

    public CamState MainCam_State; 
    //控制变量
    private Vector3 viewPos_0;         //鼠标左键初始坐标
    private Vector3 viewPos_1;         //鼠标右键初始坐标

    private Vector3 startCameraPos;           //相机初始位置
    private Vector3 targetCameraPos;          //相机目标位置

    private Vector2 startCameraEuler;         //初始角度
    private Vector2 targetCameraEuler;        //目标角度


    //相机控制
    public CinemachineVirtualCamera mainCam; //接受一个虚拟相机对象
    public Camera mainCamera;       //mainCam的camera组件来检测鼠标移动
    private Transform ObjTransform;  //mainCam的Transform

    //相机是否可控制
    private bool isCameraCtrl = true;           
    //相机操作有效性判断 鼠标是否点到UI
    private bool isClickUI = false;

    

    public void init(CinemachineVirtualCamera CVC,Camera c)
    {
        mainCam = CVC;
        ObjTransform = mainCam.transform;
        //mainCamera = mainCam.GetComponent<Camera>();
        mainCamera = c;
        targetCameraPos = ObjTransform.position;
        targetCameraEuler = ObjTransform.eulerAngles;
    }
    public void init(CinemachineVirtualCamera CVC)
    {
        mainCam = CVC;
        ObjTransform = mainCam.transform;
        mainCamera = mainCam.GetComponent<Camera>();
        targetCameraPos = ObjTransform.position;
        targetCameraEuler = ObjTransform.eulerAngles;
    }
    /*public override void RegisterAction()
    {
        GameEvent.StartCameraControl += StartCameraControl;
        GameEvent.StopCameraControl += StopCameraControl;
    }

    public override void RemoveAction()
    {
        GameEvent.StartCameraControl -= StartCameraControl;
        GameEvent.StopCameraControl -= StopCameraControl;
    }*/

    public void listen()
    {
        //Debug.Log(ObjTransform.position.ToString() + "update");
        //相机控制
        if (isCameraCtrl)
        {
            //操作有效性判断
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
            {
                isClickUI = EventSystem.current.IsPointerOverGameObject();
                //print(isClickUI);
            }
            if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2))
            {
                isClickUI = false;
            }
            if (!isClickUI)
            {
                //平移控制
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(2))
                {
                    //自由视角
                    viewPos_0 = mainCamera.ScreenToViewportPoint(Input.mousePosition);
                    startCameraPos = ObjTransform.position;
                    startCameraEuler = ObjTransform.eulerAngles;
                }
                if (Input.GetMouseButton(0) || Input.GetMouseButton(2))
                {
                    float moveSpeed = GetMoveSpeed();
                    Vector3 dis = (mainCamera.ScreenToViewportPoint(Input.mousePosition) - viewPos_0) * moveSpeed;
                    dis = new Vector3(-dis.x * 3f, 0f, -dis.y);
                    dis = Quaternion.AngleAxis(ObjTransform.eulerAngles.y, Vector3.up) * dis;
                    targetCameraPos = startCameraPos + dis;
                }
                //旋转控制
                if (Input.GetMouseButtonDown(1))
                {
                    viewPos_1 = mainCamera.ScreenToViewportPoint(Input.mousePosition);
                    startCameraPos = ObjTransform.position;
                    startCameraEuler = ObjTransform.eulerAngles;
                }
                if (Input.GetMouseButton(1))
                {
                    Vector2 angle = (mainCamera.ScreenToViewportPoint(Input.mousePosition) - viewPos_1) * rotateSpeed;
                    angle = new Vector3(-angle.y, angle.x);
                    targetCameraEuler = startCameraEuler + angle;
                }
                //缩进控制
                //ObjTransform.Translate(Vector3.forward * Input.GetAxis("Mouse ScrollWheel") * scrollSpeed);
                if (Input.mouseScrollDelta != Vector2.zero)
                {
                    float moveSpeed = GetMoveSpeed() * 0.1f;
                    targetCameraPos += ObjTransform.forward * moveSpeed * Input.mouseScrollDelta.y;

                }

                //键盘控制
                /*if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                {
                    float speedArgs = 0.3f;
                    if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                    {
                        speedArgs = 1f;
                    }
                    float moveSpeed = GetMoveSpeed();
                    targetCameraPos += (ObjTransform.forward * moveSpeed * speedArgs * Input.GetAxis("Vertical") * Time.deltaTime + ObjTransform.right * moveSpeed * speedArgs * Input.GetAxis("Horizontal") * Time.deltaTime);
                }*/

                //相机运动
                LimitCamera();
                //控制相机运动
                if (Vector3.Distance(ObjTransform.position, targetCameraPos) > 0.1f)
                {
                    ObjTransform.position = Vector3.Lerp(ObjTransform.position, targetCameraPos, 0.1f);
                }
                //控制相机旋转
                if (Quaternion.Angle(ObjTransform.rotation, Quaternion.Euler(targetCameraEuler)) > 0.1f)
                {
                    ObjTransform.rotation = Quaternion.Lerp(ObjTransform.rotation, Quaternion.Euler(targetCameraEuler), 0.1f);
                }

                
            }
        }

        //开启相机控制
        void StartCameraControl()
        {

            isCameraCtrl = true;
            viewPos_0 = mainCamera.ScreenToViewportPoint(Input.mousePosition); //设置鼠标左键初始坐标
            viewPos_1 = mainCamera.ScreenToViewportPoint(Input.mousePosition); //设置鼠标右键初始坐标
            startCameraPos = ObjTransform.position;
            startCameraEuler = ObjTransform.eulerAngles;
            targetCameraPos = ObjTransform.position;
            targetCameraEuler = ObjTransform.eulerAngles;
        }

        //停止相机控制
        void StopCameraControl()
        {
            isCameraCtrl = false;
        }


        //获取相机移动速度
        float GetMoveSpeed()
        {
            float rate = (ObjTransform.position.y - minHeight) / (maxHeight - minHeight);
            return minSpeed + (maxSpeed - minSpeed) * rate;
        }

        //限制相机位置，角度
        void LimitCamera()
        {

            //位置限制
            //if (targetCameraPos.y < minHeight) { targetCameraPos.y = minHeight; }
            //targetCameraPos.x = Mathf.Clamp(targetCameraPos.x, minX, maxX);
            targetCameraPos.y = Mathf.Clamp(targetCameraPos.y, minHeight, maxHeight);
            //targetCameraPos.z = Mathf.Clamp(targetCameraPos.z, minZ, maxZ);
            //角度限制
            if (targetCameraEuler.x > 180) { targetCameraEuler.x -= 360f; }
            if (targetCameraEuler.x < -180) { targetCameraEuler.x += 360; }
            if (targetCameraEuler.y > 180) { targetCameraEuler.y -= 360f; }
            if (targetCameraEuler.y < -180) { targetCameraEuler.y += 360; }
            targetCameraEuler.x = Mathf.Clamp(targetCameraEuler.x, minAngleX, maxAngleX);
        }


    }
}
