using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ScreenshotCapture : MonoBehaviour {

    [SerializeField] private Image img;
  
    public async UniTask<Texture2D> TakeScreenshot(){

        var screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        await UniTask.WaitForEndOfFrame(this);

        screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenShot.Apply();
        return screenShot;
    }

    private async void Update(){
        if (!Input.GetKeyDown(KeyCode.S)) return;
        var s = await TakeScreenshot();
       var sp = Sprite.Create(s, new Rect(0, 0, s.width, s.height), new Vector2(1f,1f), 100f);
      // img.sprite = new Sprite(sp)
    }
}