using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;

[RequireComponent(typeof(Image))]
public class ColorPointer : MonoBehaviour, IPointerDownHandler {

    [SerializeField] private Color color;
    
    public event Action<Color> ColorPickEvent;
    public Color Color => color;
    
    private Image _image;

    private void OnValidate(){
        _image = GetComponent<Image>();
        _image.color = color;
    }

    public void OnPointerDown(PointerEventData eventData){
        ColorPickEvent?.Invoke(color);
    }
}
