using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineAnimation : MonoBehaviour
{
    private Vector3[] points;
    private float perDistance; //每段距离
    private float totalTime; 
    private bool _isPaintOver; //A2B动画是否完成
    private int _i=0; //计数器
    public Material lineMaterial;
    public int pCount = 20; //点的个数
    public LineRenderer line;

    void Start()
    {
        points = new Vector3[pCount];
    }
    public void clearpoints()
    {
        _i = 0;
        line.positionCount = 0;
        _isPaintOver = false;
    }
    public void shotline(LineRenderer line0, Transform startTrans, Transform endTrans, int vexCount, float dtime)
    {
        //实时计算方向
        Vector3 dir = endTrans.position - startTrans.position;
        //实时计算每段的长度
        perDistance = dir.magnitude / (vexCount - 1); //10个点是分9段
        for (int i = 0; i < vexCount; i++)
        {
            points[i] = startTrans.position + dir.normalized * perDistance * i;
        }

        //如果是第一次执行
        if (!_isPaintOver)
        {
            //每隔dtime秒绘制一段线段
            totalTime += Time.deltaTime;
            if (totalTime >= dtime)
            {
                _i++;
                if (_i <= vexCount) //如果没绘制完
                {
                    line0.positionCount = _i;
                    for (int i = 0; i < _i; i++)
                        line0.SetPosition(i, points[i]);
                }
                else
                    _isPaintOver = true;
                totalTime = 0;
            }
        }
        else
        {
            _i = 0;
            line0.positionCount = vexCount;
            for (int i = 0; i < vexCount; i++)
                line0.SetPosition(i, points[i]);
        }

    }
}
