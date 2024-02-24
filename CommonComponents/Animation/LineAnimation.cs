using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineAnimation : MonoBehaviour
{
    private Vector3[] points;
    private float perDistance; //ÿ�ξ���
    private float totalTime; 
    private bool _isPaintOver; //A2B�����Ƿ����
    private int _i=0; //������
    public Material lineMaterial;
    public int pCount = 20; //��ĸ���
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
        //ʵʱ���㷽��
        Vector3 dir = endTrans.position - startTrans.position;
        //ʵʱ����ÿ�εĳ���
        perDistance = dir.magnitude / (vexCount - 1); //10�����Ƿ�9��
        for (int i = 0; i < vexCount; i++)
        {
            points[i] = startTrans.position + dir.normalized * perDistance * i;
        }

        //����ǵ�һ��ִ��
        if (!_isPaintOver)
        {
            //ÿ��dtime�����һ���߶�
            totalTime += Time.deltaTime;
            if (totalTime >= dtime)
            {
                _i++;
                if (_i <= vexCount) //���û������
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
