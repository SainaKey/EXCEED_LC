using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StroboToggle : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    [SerializeField] private Slider slider;
    [SerializeField] private RawImage stroboRawImage;
    [SerializeField] private Material strobeMaterial;
    
    private int matId;
    // Start is called before the first frame update
    void Start()
    {
        toggle.onValueChanged.AddListener(value =>
        {
            if (value)
            {
                stroboRawImage.color = Color.white;
            }
            else
            {
                stroboRawImage.color = Color.black;
            }
        });

        var shader = strobeMaterial.shader;
        matId = shader.GetPropertyNameId(shader.FindPropertyIndex("_Speed"));
        
        slider.onValueChanged.AddListener(value =>
        {
            strobeMaterial.SetFloat(matId,value);
        });
    }
    
}
