using UnityEngine;

public class ViewController {
    private const float TRANSPARENCY_MIN_VALUE = 0.05f;
    private const float TRANSPARENCY_MAX_VALUE = 1.0f;

    private readonly Renderer[] _outerBoxChildRenderers, _doorChildRenders;
    private readonly int _alphaScale = Shader.PropertyToID("_AlphaScale");

    private readonly RectTransform _schemeInstanceRect;
    private readonly Transform _modelTransform, _outerBoxTransform;

    public ViewController(Transform modelTransform, Transform doorTransform, Transform outerBoxTransform, RectTransform schemeInstanceRect){
        _modelTransform = modelTransform;
        _outerBoxTransform = outerBoxTransform;

        _schemeInstanceRect = schemeInstanceRect;

        _doorChildRenders = doorTransform.GetComponentsInChildren<Renderer>();
        _outerBoxChildRenderers = outerBoxTransform.GetComponentsInChildren<Renderer>();
    }

    public void ModelView(bool state) => _modelTransform.gameObject.SetActive(state);
    public void OnlyFillingView(bool state) => _outerBoxTransform.gameObject.SetActive(!state);

    public void ForceLineView(bool state) => TransparencyView(_outerBoxChildRenderers, state);

    public void SchemeView(bool state){
        _schemeInstanceRect.gameObject.SetActive(state);
        TransparencyView(_doorChildRenders, state);
    }

    private void TransparencyView(Renderer[] rends, bool state){
        var value = state ? TRANSPARENCY_MIN_VALUE : TRANSPARENCY_MAX_VALUE;

        foreach (var rend in rends)
            rend.material.SetFloat(_alphaScale, value);
    }
}
