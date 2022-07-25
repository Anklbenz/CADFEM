using Cysharp.Threading.Tasks;
using UnityEngine;

public class WebRequestSender : ApiRequestDataGetter {
    private string _servicesUrl;

    public WebRequestSender(string servicesUrl, string authorizationHeader) : base(authorizationHeader){
        _servicesUrl = servicesUrl;
    }

    public void SetServicesUrl(string url) => _servicesUrl = url;
    public void SetAuthorizationHeader(string header) => AuthorizationHeader = header;

    //T request result, TU param class
    public async UniTask<T> GetServiceResult<T, TU>(string serviceName, TU param ) where T : class{
        var requestUrl = _servicesUrl + serviceName;
        var operationIdParamInJson = JsonUtility.ToJson(param);
        return await Get<T>(requestUrl, operationIdParamInJson);
    }
}
