using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Blinker : MonoBehaviour
{
    // Start is called before the first frame update
    private Image img;
    void Start()
    {
        img = GetComponent<Image>();
        img.DOFade(0.8f,1.5f).SetLoops(-1);
    }

}
