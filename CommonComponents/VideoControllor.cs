using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(RawImage))]
public class VideoControllor : MonoBehaviour
{
    public string webcamURL = "http://webcam"; // ����ͷ�ķ��ʵ�ַ

    private RawImage rawImage;
    private VideoPlayer videoPlayer;

    private void Start()
    {
        rawImage = GetComponent<RawImage>();
        videoPlayer = GetComponent<VideoPlayer>();

        // ������Ƶ��������URL
        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = webcamURL;

        // ������Ƶ�����RawImage
        videoPlayer.renderMode = VideoRenderMode.APIOnly;
        //videoPlayer.targetTexture = new RenderTexture(1920, 1080, 24);
        //rawImage.texture = videoPlayer.targetTexture;

        // ������Ƶ
        videoPlayer.Play();
    }

}
