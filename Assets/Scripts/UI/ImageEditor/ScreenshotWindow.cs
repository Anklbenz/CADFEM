using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class ScreenshotWindow : Window {
    [SerializeField] private Image screenshotImage;
    [SerializeField] private TMP_Text hintLabel;
    [SerializeField] private Button shootButton, reshotButton, acceptButton, cancelButton;

    [SerializeField] private RectTransform takeShotPanel, acceptResultPanel;

    private UniTaskCompletionSource<Image> _screenShotCompletionSource;

    public async UniTask<Image> TakeScreenshotProcess(){
        _screenShotCompletionSource = new UniTaskCompletionSource<Image>();

        return await _screenShotCompletionSource.Task;
    }

    private async void OnShootClick(){
        var screenSprite = await TakeScreenshot();

        screenshotImage.sprite = screenSprite;

        acceptResultPanel.gameObject.SetActive(true);
        takeShotPanel.gameObject.SetActive(false);
    }

    private void OnAcceptClick(){
        _screenShotCompletionSource.TrySetResult(screenshotImage);
    }

    private void OnCancelClick(){
        _screenShotCompletionSource.TrySetResult(null);
    }

    private void OnReshotClick(){
        acceptResultPanel.gameObject.SetActive(false);
        takeShotPanel.gameObject.SetActive(true);
    }

    private async UniTask<Sprite> TakeScreenshot(){
        var screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        await UniTask.WaitForEndOfFrame(this);

        screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenShot.Apply();
        return Sprite.Create(screenShot, new Rect(0, 0, screenShot.width, screenShot.height), new Vector2(1f, 1f), 100f);
    }

    private void OnEnable(){
        shootButton.onClick.AddListener(OnShootClick);
        reshotButton.onClick.AddListener(OnReshotClick);
        cancelButton.onClick.AddListener(OnCancelClick);
    }

    private void OnDisable(){
        shootButton.onClick.RemoveListener(OnShootClick);
        reshotButton.onClick.RemoveListener(OnReshotClick);
        cancelButton.onClick.RemoveListener(OnCancelClick);
    }
}