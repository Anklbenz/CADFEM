using System.Text;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ThingsGetter : MonoBehaviour {
 //   private const string THINGWORX_APP_KEY = "f154cb1a-e591-4a28-8c58-9c9b8f740d0e";
//    private const string THINGWORX_AR_THING = "CADFEM.Solutions.EAM.AugmentedRealityServicesThing";
    private const string LOGIN = "Basic QWRtaW5pc3RyYXRvcjoxMjM0NXF3ZXJ0YXNkZkc=";
    private const string REQUEST_CONTENT_TYPE = "application/json";
    private const string THING_TYPE = "{\"type\": \"Thing\",\"nameMask\": \"Asset_*\"}";
    private const string URL = "https://svtaneko-migration.digitaltwin.ru:8443/Thingworx/Things/";
    private const string THINGS_LIST_URL =
        "https://svtaneko-migration.digitaltwin.ru:8443/Thingworx/Resources/EntityServices/Services/GetEntityList";
    
    [SerializeField] private Button loadButton;
    [SerializeField] private Text assetsListText;
    
    private void Awake(){
        loadButton.onClick.AddListener(LoadAssetsButton_Clicked);
    }

    private async UniTask<ThingsData.AssetsData> GetAssetsFromThingworx(){
        var json = await GetThingWorxRequest(THING_TYPE);

        var assets = JsonUtility.FromJson<ThingsData.AssetsData>(json);
        return assets;
    }

    private async UniTask<string> GetThingWorxRequest(string param = ""){
        var webRequest = UnityWebRequest.Get(THINGS_LIST_URL);
        webRequest.method = "POST";
        webRequest.SetRequestHeader("Accept", REQUEST_CONTENT_TYPE);
        webRequest.SetRequestHeader("Content-Type", REQUEST_CONTENT_TYPE);
        webRequest.SetRequestHeader("Authorization", LOGIN);
        //  webRequest.SetRequestHeader("appKey", THINGWORX_APP_KEY);
        if (param != "")
            webRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(param));

        webRequest.certificateHandler = new ForceAcceptAllCertificates();
        var operation = webRequest.SendWebRequest();
        
        while (!operation.isDone)
            await UniTask.Yield();

        var response = webRequest.responseCode;
        Debug.Log(response);
        
        webRequest.certificateHandler.Dispose();
        webRequest.uploadHandler?.Dispose();
        return webRequest.downloadHandler.text;
    }

    public async void LoadAssetsButton_Clicked(){

        var assets = await GetAssetsFromThingworx();
        var result = $"Total: {assets.rows.Length}\n";

        foreach (var asset in assets.rows)
            result += $"{asset.name}: {asset.description}\n";

        assetsListText.text = result;
    }
}
