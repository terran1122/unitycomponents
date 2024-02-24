using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneSwitchManager : MonoBehaviour
{
    // �Ƿ����ó������뵭��
    public bool isFadeEnabled = true;
    // �����л����뵭���ٶ�
    public float fadeSpeed = 2.5f;
    private bool isFading = false;

    // �л�����
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

    // �������뵭��
    private IEnumerator FadeScene(string oldSceneName,string sceneName)
    {
        // ��ȡ��Ļ���
        // ��ȡ��Ļ���
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // ��ȡUI����
        Canvas canvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
        
        // ����һ����ɫ����
        GameObject fadeMask = new GameObject("FadeMask");
        fadeMask.transform.SetParent(canvas.transform, false);
        fadeMask.AddComponent<CanvasRenderer>();
        Image fadeImage = fadeMask.AddComponent<Image>();
        //fadeImage.color = Color.black;
        fadeImage.color = new Color(0f, 0f, 0f, 0f);

        // �������ֲ��С��λ��
        fadeImage.rectTransform.anchorMin = Vector2.zero;
        fadeImage.rectTransform.anchorMax = Vector2.one;
        fadeImage.rectTransform.anchoredPosition = Vector2.zero;
        fadeImage.rectTransform.sizeDelta = new Vector2(screenWidth, screenHeight);

       

        // ������ǰ����
        yield return StartCoroutine(FadeOut(fadeImage));

        // �����³���
        SceneManager.UnloadSceneAsync(oldSceneName).completed += sceneOperation =>
        {
            //SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive).completed += sceneOperation =>
            {

                SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
            };
        };
     
        // �����³���
        yield return StartCoroutine(FadeIn(fadeImage));

        //���ٺ�ɫ����
        Destroy(fadeMask);

        isFading = false;
    }

    private IEnumerator FadeScene(string sceneName)
    {
        // ��ȡ��Ļ���
        // ��ȡ��Ļ���
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // ��ȡUI����
        Canvas canvas = GameObject.Find("UICanvas").GetComponent<Canvas>();

        // ����һ����ɫ����
        GameObject fadeMask = new GameObject("FadeMask");
        fadeMask.transform.SetParent(canvas.transform, false);
        fadeMask.AddComponent<CanvasRenderer>();
        Image fadeImage = fadeMask.AddComponent<Image>();
        //fadeImage.color = Color.black;
        fadeImage.color = new Color(0f, 0f, 0f, 0f);

        // �������ֲ��С��λ��
        fadeImage.rectTransform.anchorMin = Vector2.zero;
        fadeImage.rectTransform.anchorMax = Vector2.one;
        fadeImage.rectTransform.anchoredPosition = Vector2.zero;
        fadeImage.rectTransform.sizeDelta = new Vector2(screenWidth, screenHeight);



        // ������ǰ����
        yield return StartCoroutine(FadeOut(fadeImage));

        // �����³���
        //SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        SceneManager.LoadScene(sceneName);
        

        // �����³���
        yield return StartCoroutine(FadeIn(fadeImage));

        //���ٺ�ɫ����
        Destroy(fadeMask);

        isFading = false;
    }
    // ����������͸���Ƚ��䵽1��
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

    // ���볡�����Ӻ�ɫ��Ϊ͸����
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