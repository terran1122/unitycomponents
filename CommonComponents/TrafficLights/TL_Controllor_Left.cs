using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct LightStruct_Sprite
{
    public SpriteRenderer red_obj;
    public SpriteRenderer yellow_obj;
    public SpriteRenderer green_obj;

    public Sprite greenSprite;
    public Sprite yellowSprite;
    public Sprite redSprite;
    public Sprite blackSprite;

}

//左转交通灯控制
public class TL_Controllor_Left : MonoBehaviour
{
    // Start is called before the first frame update
    /*private MeshRenderer redLight;
    private MeshRenderer yellowLight;
    private MeshRenderer greenLight;
    private Material greenMaterial;
    private Material yellowMaterial;
    private Material redMaterial;
    private Material blackMaterial;*/


    private SpriteRenderer redLight_sprite;
    private SpriteRenderer yellowLight_sprite;
    private SpriteRenderer greenLight_sprite;
    private Sprite green_sprite;
    private Sprite yellow_sprite;
    private Sprite red_sprite;
    //private Sprite black_sprite;

    public float greenTime = 5.0f;
    public float yellowTime = 0.5f;
    public float redTime = 4.0f;

    public float blinkDuration = 1.5f;
    public float blinkfrequency = 0.3f;

    public bool isUseSprite = false;
    private TrafficLightState state;
    public void init(LightStruct_Sprite ls, TrafficLightState startstate)
    {
        redLight_sprite = ls.red_obj;
        yellowLight_sprite = ls.yellow_obj;
        greenLight_sprite = ls.green_obj;

        green_sprite = ls.greenSprite;
        yellow_sprite = ls.yellowSprite;
        red_sprite = ls.redSprite;
        //black_sprite = ls.blackSprite;

        state = startstate;
    }
    
    public void play() {
       StartCoroutine(TrafficLight());
    }

 

    //用精灵贴图
    IEnumerator TrafficLight()
    {
        while (true)
        {
            switch (state)
            {
                case TrafficLightState.GREEN:
                    greenLight_sprite.enabled = true;
                    greenLight_sprite.sprite = green_sprite;
                    redLight_sprite.enabled = false;
                    yellowLight_sprite.enabled = false;
                    yield return new WaitForSeconds(greenTime);

                    // 绿灯闪烁之前的静止时间
                    yield return new WaitForSeconds(blinkDuration);
                    state = TrafficLightState.GREEN_BLINKING;
                    break;

                case TrafficLightState.GREEN_BLINKING:
                    float remainingTime = blinkDuration;
                    while (remainingTime > 0)
                    {
                        greenLight_sprite.enabled = !greenLight_sprite.enabled;
                        yield return new WaitForSeconds(blinkfrequency);
                        remainingTime -= blinkfrequency;
                    }

                    state = TrafficLightState.YELLOW;
                    break;

                case TrafficLightState.YELLOW:
                    yellowLight_sprite.enabled = true;
                    yellowLight_sprite.sprite = yellow_sprite;
                    greenLight_sprite.enabled = false;
                    redLight_sprite.enabled = false;
                    yield return new WaitForSeconds(yellowTime);

                    // 黄灯闪烁之前的静止时间
                    yield return new WaitForSeconds(blinkDuration);
                    state = TrafficLightState.YELLOW_BLINKING;
                    break;

                case TrafficLightState.YELLOW_BLINKING:
                    remainingTime = blinkDuration;
                    while (remainingTime > 0)
                    {
                        yellowLight_sprite.enabled = !yellowLight_sprite.enabled;
                        yield return new WaitForSeconds(blinkfrequency);
                        remainingTime -= blinkfrequency;
                    }

                    state = TrafficLightState.RED;
                    break;

                case TrafficLightState.RED:
                    redLight_sprite.enabled = true;
                    redLight_sprite.sprite = red_sprite;
                    yellowLight_sprite.enabled = false;
                    greenLight_sprite.enabled = false;
                    yield return new WaitForSeconds(redTime);

                    // 红灯闪烁之前的静止时间
                    yield return new WaitForSeconds(blinkDuration);
                    state = TrafficLightState.RED_BLINKING;
                    break;

                case TrafficLightState.RED_BLINKING:
                    remainingTime = blinkDuration;
                    while (remainingTime > 0)
                    {
                        redLight_sprite.enabled = !redLight_sprite.enabled;
                        yield return new WaitForSeconds(blinkfrequency);
                        remainingTime -= blinkfrequency;
                    }

                    state = TrafficLightState.GREEN;
                    break;

                default:
                    break;
            }
        }
    }
}


