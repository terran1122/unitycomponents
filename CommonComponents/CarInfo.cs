using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CarInfo : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public string carName;
    [SerializeField] public string carlisence;
    [SerializeField] public DeviceInfo[] cardevices;
    [SerializeField] public string cartype;
    [SerializeField] public string gear;
    [SerializeField] public float speed;
}

