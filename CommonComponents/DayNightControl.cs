using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Reflection;

public class DayNightControl : MonoBehaviour
{
    
    private Light Glight; //全局灯光
    private Quaternion GlightQ;  //灯光初始角度
    private float GlightStrenth; //灯光初始强度

    private Color skyinitcolor,cloudinitcolor; //天空初始颜色 
    private float deltaR, deltaG, deltaB;
    private float deltaCR, deltaCG, deltaCB;

    private ScriptableRendererFeature renderFeature_Snow;
    private Material Snow_Material;
    private float snow_strength = 105;
    private float snow_intensity = 0;
    private bool _isfog = false;
    private Transform fogplan;
    private Material fog_mat;
    private float fog_intensity = 35;
    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.skybox.SetColor("_ZenithColor", new Color(0.235f,0.391f,0.737f));
        RenderSettings.skybox.SetColor("_CloudColor", Color.white);
        RenderSettings.skybox.SetFloat("_StarBrightness", 0);
        RenderSettings.skybox.SetFloat("_SunBrightness",5);


        Glight = GameObject.Find("Directional Light").GetComponent<Light>();
        GlightQ = Glight.transform.rotation;
        GlightStrenth = 2;
        Glight.intensity = GlightStrenth;

        skyinitcolor = RenderSettings.skybox.GetColor("_ZenithColor");
        cloudinitcolor = RenderSettings.skybox.GetColor("_CloudColor");
        deltaR = skyinitcolor.r - 0.04f;
        deltaG = skyinitcolor.g - 0.04f;
        deltaB = skyinitcolor.b - 0.04f;

        deltaCR = cloudinitcolor.r - 0.35f;
        deltaCG = cloudinitcolor.g - 0.35f;
        deltaCB = cloudinitcolor.b - 0.35f;
        //print(skyinitcolor);
        //print(deltaR + "," + deltaG + "," + deltaB);
        UniversalRenderPipelineAsset URPAsset = (UniversalRenderPipelineAsset)QualitySettings.renderPipeline;
        FieldInfo propertyInfo = URPAsset.GetType().GetField("m_RendererDataList", BindingFlags.Instance | BindingFlags.NonPublic);
        UniversalRendererData URPRenderData = (UniversalRendererData)(((ScriptableRendererData[])propertyInfo?.GetValue(URPAsset))?[0]);
        renderFeature_Snow = URPRenderData.rendererFeatures[2];
        Snow_Material = Resources.Load<Material>("Shader/SG_Snow");
        Snow_Material.SetColor("_SnowColor", new Color(1, 1, 1, 0 / 255));
        Snow_Material.SetFloat("_SnowIntensity", snow_intensity);

        fogplan = Camera.main.transform.Find("Plane");
        fog_mat = fogplan.GetComponent<Renderer>().material;
        //fog_mat.SetFloat("Vector1_6F1EA0F8", 0);
    }

    // Update is called once per frame
    public void DayNight(float strength)
    {
        //print(strength); //0-1
        GameObject.Find("Directional Light").transform.rotation = Quaternion.Euler(GlightQ.eulerAngles.x - strength * 50, GlightQ.eulerAngles.y, GlightQ.eulerAngles.z);
        Color skycolor = new Color(skyinitcolor.r - deltaR * strength, skyinitcolor.g - deltaG * strength, skyinitcolor.b - deltaB * strength);
        Color cloudcolor = new Color(cloudinitcolor.r - deltaCR * strength, cloudinitcolor.g - deltaCG * strength, cloudinitcolor.b - deltaCB * strength);
        RenderSettings.skybox.SetColor("_ZenithColor", skycolor);
        RenderSettings.skybox.SetColor("_CloudColor", cloudcolor);
        RenderSettings.skybox.SetFloat("_StarBrightness", 0.5f * strength);
        RenderSettings.skybox.SetFloat("_SunBrightness", 5 - strength*4);
        Glight.intensity = GlightStrenth - strength;

        if (strength >= 0.5)
            transform.Find("BEffects").gameObject.SetActive(true);
        else
            transform.Find("BEffects").gameObject.SetActive(false);
    }
    public void RainOn(float strength)
    {
        transform.Find("Weather/RainEffects").gameObject.SetActive(true);
    }
    public void RainOFF(float strength)
    {
        transform.Find("Weather/RainEffects").gameObject.SetActive(false);
    }
    public void SnowOn(float strength)
    {
        transform.Find("Weather/SnowEffects").gameObject.SetActive(true);
        renderFeature_Snow.SetActive(true);
        StartCoroutine(ChangeSnowStrength(0, snow_strength,5f));
        StartCoroutine(ChangeSnowIntensity(0, 1, 5f));
    }
    public void SnowOFF(float strength)
    {
        transform.Find("Weather/SnowEffects").gameObject.SetActive(false);
        renderFeature_Snow.SetActive(false);
    }
    private IEnumerator ChangeSnowStrength(float startv, float targetv,float duration)
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            // 根据插值算法计算当前数值
            float currentValue = Mathf.Lerp(startv, targetv, elapsedTime / duration);
            Color c = new Color(1, 1, 1, currentValue/255);
            //将当前数值赋给你想要改变的变量
            Snow_Material.SetColor("_SnowColor",c);
            //print(currentValue);
            // 增加已经流逝的时间
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // 最后确保目标值被准确赋给你想要改变的变量
        Snow_Material.SetColor("_SnowColor", new Color(1, 1, 1, snow_strength/255));
    }
    private IEnumerator ChangeSnowIntensity(float startv, float targetv, float duration)
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            // 根据插值算法计算当前数值
            float currentValue = Mathf.Lerp(startv, targetv, elapsedTime / duration);
           
            Snow_Material.SetFloat("_SnowIntensity", currentValue);
            //print(currentValue);
            // 增加已经流逝的时间
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // 最后确保目标值被准确赋给你想要改变的变量
        Snow_Material.SetFloat("_SnowIntensity", targetv);
    }
    private IEnumerator ChangeFogIntensity(float startv, float targetv, float duration)
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            // 根据插值算法计算当前数值
            float currentValue = Mathf.Lerp(startv, targetv, elapsedTime / duration);
            //将当前数值赋给你想要改变的变量
            fog_mat.SetFloat("Vector1_6F1EA0F8", currentValue);
            //print(currentValue);
            // 增加已经流逝的时间
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // 最后确保目标值被准确赋给你想要改变的变量
        fog_mat.SetFloat("Vector1_6F1EA0F8", fog_intensity);
    }
    public void FogOn(float strength)
    {
        _isfog = true;
        fogplan.gameObject.SetActive(true);
        StartCoroutine(ChangeFogIntensity(0, 35, 6));
    }
    public void FogOFF(float strength)
    {
        _isfog = false;
        fog_mat.SetFloat("Vector1_6F1EA0F8", 0);
        fogplan.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (_isfog)
        {
            if (Camera.main.transform.position.y >= 52)
            {
                fog_mat.SetFloat("Vector1_705D9E76", 50);
            }
            else if (Camera.main.transform.position.y >= 33)
            {
                fog_mat.SetFloat("Vector1_705D9E76", 33);
            }
            else if (Camera.main.transform.position.y >= 25)
            {
                fog_mat.SetFloat("Vector1_705D9E76", 22);
            }
            else if (Camera.main.transform.position.y >= 10)
            {
                fog_mat.SetFloat("Vector1_705D9E76", Camera.main.transform.position.y+0.5f);
            }
            else
            {
                fog_mat.SetFloat("Vector1_705D9E76", 10);
            }
        }
    }
}
