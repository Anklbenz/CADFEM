using UnityEngine;
using UnityEngine.Video;

public class OnlineVideoLoader : MonoBehaviour {

    
    private const string VIDEO_URL = "https://svtaneko-migration.digitaltwin.ru:8443/Thingworx/FileRepositories/ServiceVizor_Media/video/v%20bypass%20SitePro.mp4";
    public VideoPlayer videoPlayer;
    public string _videoUrl = "yourvideourl";
    // Start is called before the first frame update
    void Start(){
        videoPlayer.url = _videoUrl;
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.Prepare();
    }
}