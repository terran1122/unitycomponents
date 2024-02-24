using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(RawImage))]
public class VideoControllor : MonoBehaviour
{
    public string webcamURL = "http://webcam"; // 摄像头的访问地址

    private RawImage rawImage;
    private VideoPlayer videoPlayer;

    private void Start()
    {
        rawImage = GetComponent<RawImage>();
        videoPlayer = GetComponent<VideoPlayer>();

        // 设置视频播放器的URL
        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = webcamURL;

        // 设置视频输出到RawImage
        videoPlayer.renderMode = VideoRenderMode.APIOnly;
        //videoPlayer.targetTexture = new RenderTexture(1920, 1080, 24);
        //rawImage.texture = videoPlayer.targetTexture;

        // 播放视频
        videoPlayer.Play();
    }

}
