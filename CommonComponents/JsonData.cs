using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonData : MonoBehaviour
{
    // Start is called before the first frame update
    public static void savebyjson(string filename, object data)
    {
        var json = JsonUtility.ToJson(data, true);
        var path = Path.Combine(Application.persistentDataPath, filename);
        File.WriteAllText(path, json);
        //print(json);
    }
    public static string getbyjson(string filename)
    {
        var path = Path.Combine(Application.persistentDataPath, filename);
        //print(path);
        return File.ReadAllText(path);

    }
}
[System.Serializable]
public class pathpoints
{
    [SerializeField] public string carName;
    [SerializeField] public List<Vector3> pathpoint; //车身坐标
    [SerializeField] public List<Quaternion> rotaion; //车身旋转
    [SerializeField] public List<Quaternion> wheelLFRotaion; //左前轮转角
    [SerializeField] public List<Quaternion> wheelRFRotaion; //后前轮转角
    public pathpoints(string carName)
    {
        this.carName = carName;
        pathpoint = new List<Vector3>();
        rotaion = new List<Quaternion>();
        wheelLFRotaion = new List<Quaternion>();
        wheelRFRotaion = new List<Quaternion>();
    }
}

[System.Serializable]
public class RoadInfo
{
    public int roadLength;
    public string roadState;
    public string roadType;
    public string location;
    public string roadDescription;
    public TrafficSign trafficsign;
}
[System.Serializable]
public class TrafficSign
{
    public int speedlimit;
    public string dir; //方向
}
//杆上的路测设备数量
[System.Serializable]
public class Device
{
    public string devicetype;
    public int num;
}
//路测设备详情
[System.Serializable]
public class DeviceInfo
{
    public string deviceId; //编号
    public string deviceType; //类型
    public string curState; //状态
    public string communicationMethod; //通讯方式
    public string location; //位置
    public string deviceModelNum; //型号
}

[System.Serializable]
public class Pole
{
    public string Polebianhao;
    public List<Device> devicesInthispole;
    public string location;
}

[System.Serializable]
public class PoleData
{
    public Pole[] poles;
}
