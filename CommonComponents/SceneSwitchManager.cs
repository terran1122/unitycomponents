using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneSwitchManager : MonoBehaviour
{
    // 是否启用场景淡入淡出
    public bool isFadeEnabled = true;
    // 场景切换淡入淡出速度
    public float fadeSpeed = 2.5f;
    private bool isFading = false;

    // 切换场景
    public void SwitchScene(string oldSceneName,string sceneName)
    {
        if (!isFading)
        {
            isFading = true;
            if (isFadeEnabled)
            {
                //StartCoroutine(FadeScene(oldSceneName,sceneName));
                StartCoroutine(FadeScene(sceneName));
            }
            else
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }

    // 场景淡入淡出
    private IEnumerator FadeScene(string oldSceneName,string sceneName)
    {
        // 获取屏幕宽高
        // 获取屏幕宽高
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // 获取UI画布
        Canvas canvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
        
        // 启用一个黑色遮罩
        GameObject fadeMask = new GameObject("FadeMask");
        fadeMask.transform.SetParent(canvas.transform, false);
        fadeMask.AddComponent<CanvasRenderer>();
        Image fadeImage = fadeMask.AddComponent<Image>();
        //fadeImage.color = Color.black;
        fadeImage.color = new Color(0f, 0f, 0f, 0f);

        // 设置遮罩层大小和位置
        fadeImage.rectTransform.anchorMin = Vector2.zero;
        fadeImage.rectTransform.anchorMax = Vector2.one;
        fadeImage.rectTransform.anchoredPosition = Vector2.zero;
        fadeImage.rectTransform.sizeDelta = new Vector2(screenWidth, screenHeight);

       

        // 淡出当前场景
        yield return StartCoroutine(FadeOut(fadeImage));

        // 加载新场景
        SceneManager.UnloadSceneAsync(oldSceneName).completed += sceneOperation =>
        {
            //SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive).completed += sceneOperation =>
            {

                SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
            };
        };
     
        // 淡入新场景
        yield return StartCoroutine(FadeIn(fadeImage));

        //销毁黑色遮罩
        Destroy(fadeMask);

        isFading = false;
    }

    private IEnumerator FadeScene(string sceneName)
    {
        // 获取屏幕宽高
        // 获取屏幕宽高
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // 获取UI画布
        Canvas canvas = GameObject.Find("UICanvas").GetComponent<Canvas>();

        // 启用一个黑色遮罩
        GameObject fadeMask = new GameObject("FadeMask");
        fadeMask.transform.SetParent(canvas.transform, false);
        fadeMask.AddComponent<CanvasRenderer>();
        Image fadeImage = fadeMask.AddComponent<Image>();
        //fadeImage.color = Color.black;
        fadeImage.color = new Color(0f, 0f, 0f, 0f);

        // 设置遮罩层大小和位置
        fadeImage.rectTransform.anchorMin = Vector2.zero;
        fadeImage.rectTransform.anchorMax = Vector2.one;
        fadeImage.rectTransform.anchoredPosition = Vector2.zero;
        fadeImage.rectTransform.sizeDelta = new Vector2(screenWidth, screenHeight);



        // 淡出当前场景
        yield return StartCoroutine(FadeOut(fadeImage));

        // 加载新场景
        //SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        SceneManager.LoadScene(sceneName);
        

        // 淡入新场景
        yield return StartCoroutine(FadeIn(fadeImage));

        //销毁黑色遮罩
        Destroy(fadeMask);

        isFading = false;
    }
    // 淡出场景（透明度渐变到1）
    private IEnumerator FadeOut(Image fadeImage)
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = Color.clear;
        while (fadeImage.color.a < 1f)
        {
            fadeImage.color += new Color(0f, 0f, 0f, Time.deltaTime * fadeSpeed);
            yield return null;
        }
        fadeImage.gameObject.SetActive(false);
    }

    // 淡入场景（从黑色变为透明）
    private IEnumerator FadeIn(Image fadeImage)
    {

        fadeImage.gameObject.SetActive(true);
        fadeImage.color = new Color(0f, 0f, 0f, 1f);
        print(fadeImage.color);
        while (fadeImage.color.a > 0f)
        {
            fadeImage.color -= new Color(0f, 0f, 0f, Time.deltaTime * fadeSpeed);
            yield return null;
        }
        fadeImage.gameObject.SetActive(false);
    }
}