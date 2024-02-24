using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEvent : MonoBehaviour
{
    // ������ײ�¼��Ļص�����
    public delegate void OnCollisionEnterDelegate(Collision collision);
    public delegate void OnCollisionStayDelegate(Collision collision);
    public delegate void OnCollisionExitDelegate(Collision collision);

    // ���崥���¼��Ļص�����
    public delegate void OnTriggerEnterDelegate(Collider other);
    public delegate void OnTriggerStayDelegate(Collider other);
    public delegate void OnTriggerExitDelegate(Collider other);

    // ��ײ�¼��Ļص�����
    public OnCollisionEnterDelegate onCollisionEnter;
    public OnCollisionStayDelegate onCollisionStay;
    public OnCollisionExitDelegate onCollisionExit;

    // �����¼��Ļص�����
    public OnTriggerEnterDelegate onTriggerEnter;
    public OnTriggerStayDelegate onTriggerStay;
    public OnTriggerExitDelegate onTriggerExit;

    // ��ײ�¼��ص�������ʵ��
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

    // �����¼��ص�������ʵ��
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
