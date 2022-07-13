using System;
using UnityEngine.UI;

public class DeviceDescription {
    public Image Image{ get; }
    public string Label{ get;  }
    public string Description{ get; }

    public DeviceDescription(Image image, string label, string description){
        Image = image;
        Label = label;
        Description = description;
    }
}
