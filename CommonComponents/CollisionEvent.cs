using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEvent : MonoBehaviour
{
    // 定义碰撞事件的回调函数
    public delegate void OnCollisionEnterDelegate(Collision collision);
    public delegate void OnCollisionStayDelegate(Collision collision);
    public delegate void OnCollisionExitDelegate(Collision collision);

    // 定义触发事件的回调函数
    public delegate void OnTriggerEnterDelegate(Collider other);
    public delegate void OnTriggerStayDelegate(Collider other);
    public delegate void OnTriggerExitDelegate(Collider other);

    // 碰撞事件的回调函数
    public OnCollisionEnterDelegate onCollisionEnter;
    public OnCollisionStayDelegate onCollisionStay;
    public OnCollisionExitDelegate onCollisionExit;

    // 触发事件的回调函数
    public OnTriggerEnterDelegate onTriggerEnter;
    public OnTriggerStayDelegate onTriggerStay;
    public OnTriggerExitDelegate onTriggerExit;

    // 碰撞事件回调函数的实现
    private void OnCollisionEnter(Collision collision)
    {
        if (onCollisionEnter != null)
        {
            onCollisionEnter(collision);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (onCollisionStay != null)
        {
            onCollisionStay(collision);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (onCollisionExit != null)
        {
            onCollisionExit(collision);
        }
    }

    // 触发事件回调函数的实现
    private void OnTriggerEnter(Collider other)
    {
        if (onTriggerEnter != null)
        {
            onTriggerEnter(other);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (onTriggerStay != null)
        {
            onTriggerStay(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (onTriggerExit != null)
        {
            onTriggerExit(other);
        }
    }
}
