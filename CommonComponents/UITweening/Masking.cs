using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Masking : MonoBehaviour
{
    private RectTransform me;
    // Start is called before the first frame update
    void Start()
    {
        me = GetComponent<RectTransform>();
        me.DOSizeDelta(new Vector2(me.rect.width, 49.7f), 2f).SetLoops(-1);
    }
}
