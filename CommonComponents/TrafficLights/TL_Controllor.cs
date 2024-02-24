using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct LightStruct
{
    public MeshRenderer red_obj;
    public MeshRenderer yellow_obj;
    public MeshRenderer green_obj;

    public Material greenMaterial;
    public Material yellowMaterial;
    public Material redMaterial;
    public Material blackMaterial;

}
public enum TrafficLightState
{
    GREEN,
    GREEN_BLINKING,
    YELLOW,
    YELLOW_BLINKING,
    RED,
    RED_BLINKING
}
//直行交通灯控制
public class TL_Controllor : MonoBehaviour
{
    // Start is called before the first frame update
    private MeshRenderer redLight;
    private MeshRenderer yellowLight;
    private MeshRenderer greenLight;
    private Material greenMaterial;
    private Material yellowMaterial;
    private Material redMaterial;
    private Material blackMaterial;



    public float greenTime = 3.5f;
    public float yellowTime = 0.5f;
    public float redTime = 3.5f;

    public float blinkDuration = 1.5f;
    public float blinkfrequency = 0.3f;

    public TrafficLightState state;
    
    public void init(LightStruct ls, TrafficLightState startstate)
    {
        redLight = ls.red_obj;
        yellowLight = ls.yellow_obj;
        greenLight = ls.green_obj;

        greenMaterial = ls.greenMaterial;
        yellowMaterial = ls.yellowMaterial;
        redMaterial = ls.redMaterial;
        blackMaterial = ls.blackMaterial;

        state = startstate;
    }
    public void play() {
       StartCoroutine(TrafficLight());
    }

    IEnumerator TrafficLight()
    {
        while (true)
        {
            switch (state)
            {
                case TrafficLightState.GREEN:
                    greenLight.material = greenMaterial;
                    greenLight.enabled = true;
                    redLight.material = blackMaterial;
                    yellowLight.material = blackMaterial;
                    yield return new WaitForSeconds(greenTime);

                    // 绿灯闪烁之前的静止时间
                    yield return new WaitForSeconds(blinkDuration);
                    state = TrafficLightState.GREEN_BLINKING;
                    break;

                case TrafficLightState.GREEN_BLINKING:
                    float remainingTime = blinkDuration;
                    while (remainingTime > 0)
                    {
                        greenLight.enabled = !greenLight.enabled;
                        yield return new WaitForSeconds(blinkfrequency);
                        remainingTime -= blinkfrequency;
                    }

                    state = TrafficLightState.YELLOW;
                    break;

                case TrafficLightState.YELLOW:
                    yellowLight.material = yellowMaterial;
                    yellowLight.enabled = true;
                    greenLight.material = blackMaterial;
                    redLight.material = blackMaterial;
                    yield return new WaitForSeconds(yellowTime);

                    // 黄灯闪烁之前的静止时间
                    yield return new WaitForSeconds(blinkDuration);
                    state = TrafficLightState.YELLOW_BLINKING;
                    break;

                case TrafficLightState.YELLOW_BLINKING:
                    remainingTime = blinkDuration;
                    while (remainingTime > 0)
                    {
                        yellowLight.enabled = !yellowLight.enabled;
                        yield return new WaitForSeconds(blinkfrequency);
                        remainingTime -= blinkfrequency;
                    }

                    state = TrafficLightState.RED;
                    break;

                case TrafficLightState.RED:
                    redLight.material = redMaterial;
                    redLight.enabled = true;
                    yellowLight.material = blackMaterial;
                    greenLight.material = blackMaterial;
                    yield return new WaitForSeconds(redTime);

                    // 红灯闪烁之前的静止时间
                    yield return new WaitForSeconds(blinkDuration);
                    state = TrafficLightState.RED_BLINKING;
                    break;

                case TrafficLightState.RED_BLINKING:
                    remainingTime = blinkDuration;
                    while (remainingTime > 0)
                    {
                        redLight.enabled = !redLight.enabled;
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

    //用精灵贴图

}


