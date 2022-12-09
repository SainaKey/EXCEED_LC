using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SachikoController : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] private Toggle[] toggle0;
    [SerializeField] private Toggle[] toggle1;
    [SerializeField] private Toggle[] toggle2;
    [SerializeField] private RawImage rawImage0;
    [SerializeField] private RawImage rawImage1;
    [SerializeField] private RawImage[] lightLaserrawImages;
    private Color color0;
    private Color color1;
    private Color color2;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var toggle in toggle0)
        {
            toggle.onValueChanged.AddListener(value => {
                if (value)
                {
                    color0 = toggle.targetGraphic.color;
                }
            });
        }
        
        foreach (var toggle in toggle1)
        {
            toggle.onValueChanged.AddListener(value => {
                if (value)
                {
                    color1 = toggle.targetGraphic.color;
                }
            });
        }
        
        foreach (var toggle in toggle2)
        {
            toggle.onValueChanged.AddListener(value => {
                if (value)
                {
                    color2 = toggle.targetGraphic.color;
                }
            });
        }
    }
    
    void Update()
    {
        color0.a = slider.value;
        rawImage0.color = color0;
        
        color1.a = slider.value;
        rawImage1.color = color1;
        
        color2.a = slider.value;
        foreach (var lightLaserrawImage in lightLaserrawImages)
        {
            lightLaserrawImage.color = color2;
        }
        
    }
}
