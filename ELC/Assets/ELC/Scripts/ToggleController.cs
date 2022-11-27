using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    [SerializeField] private RawImage targetRawImage;
    [SerializeField] private Toggle toggle;
    void Start()
    {
        toggle.onValueChanged.AddListener(value =>
        {
            if (value)
            {
                targetRawImage.color = ColorIntConverter.ToColors(1)[0];
            }
            else
            {
                targetRawImage.color = ColorIntConverter.ToColors(0)[0];
            }
        });
    }
    
}
