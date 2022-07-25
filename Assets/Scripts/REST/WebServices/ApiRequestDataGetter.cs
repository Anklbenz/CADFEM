#nullable enable
using System;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;

public abstract class ApiRequestDataGetter {
    private const int RESPONSE_OK = 200;
    private const string REQUEST_CONTENT_TYPE = "application/json";
    private const string REQUEST_METHOD = "POST";

    public event Action<long>? OnServerResponseEvent;
    protected string AuthorizationHeader = "";

    protected ApiRequestDataGetter(string authorizationHeader){
        AuthorizationHeader = authorizationHeader;
    }
    protected async UniTask<T?> Get<T>(string url, string param = ""){
        var json = await GetRequestResponse_Json(url, param);
        return json != null ? JsonUtility.FromJson<T>(json) : default;
    }

    private async UniTask<string?> GetRequestResponse_Json(string url, string paramJson = ""){
        var webRequest = UnityWebRequest.Get(url);
        webRequest.method = REQUEST_METHOD;
        webRequest.SetRequestHeader("Accept", REQUEST_CONTENT_TYPE);
        webRequest.SetRequestHeader("Content-Type", REQUEST_CONTENT_TYPE);
        webRequest.SetRequestHeader("Authorization", AuthorizationHeader);
        //  webRequest.SetRequestHeader("appKey", THINGWORX_APP_KEY);
        if (paramJson != "")
            webRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(paramJson));

        webRequest.certificateHandler = new ForceAcceptAllCertificates();
        var operation = webRequest.SendWebRequest();

        while (!operation.isDone)
            await UniTask.Yield();

        var responseCode = webRequest.responseCode;
        OnServerResponseEvent?.Invoke(responseCode);

        webRequest.certificateHandler.Dispose();
        webRequest.uploadHandler?.Dispose();

        return responseCode == RESPONSE_OK ? webRequest.downloadHandler.text : null;
    }
}
