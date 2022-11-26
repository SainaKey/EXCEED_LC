using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightController : MonoBehaviour , ISetTexture
{
    //RawImageをもってる
    //RawImageの色調整したり、テクスチャ調整したりするだけ
    //縦横両方に同じやつ渡せばよい
    
    //Colorは外部からもらう
    
    [SerializeField] private List<RawImage> l_rawImages = new List<RawImage>();
    [SerializeField] private List<RawImage> r_rawImages = new List<RawImage>();
    [Space]
    [SerializeField] private Shader shader;
    private Material l_material;
    private Material r_material;
    private bool isSelect;

    [Space] [Header("UI")] 
    [SerializeField] private Toggle selectToggle;
    [SerializeField] private RawImage previewRawImage;
    [SerializeField] private RawImage controlRawImage;
    [SerializeField] private Toggle verticalToggle;
    [SerializeField] private Toggle horizontalToggle;
    [SerializeField] private Toggle offToggle;
    
    [Space]
    [SerializeField] private Toggle[] toggles;
    [SerializeField] private Slider l_opacity;
    [SerializeField] private Slider r_opacity;
    [SerializeField] private Color color;

    private void Start()
    {

        selectToggle.onValueChanged.AddListener(value => isSelect = value);
        
        verticalToggle.onValueChanged.AddListener(value => {
            if (value)
            {
                SetRawImageColor(1);
            }
        });
        
        horizontalToggle.onValueChanged.AddListener(value => {
            if (value)
            {
                SetRawImageColor(2);
            }
        });
        
        offToggle.onValueChanged.AddListener(value => {
            if (value)
            {
                SetRawImageColor(0);
            }
        });
        
        foreach (var toggle in toggles)
        {
            toggle.onValueChanged.AddListener(value => {
                if (value)
                {
                    SetColor(toggle.targetGraphic.color);
                }
            });
        }
        
        l_material = new Material(shader);
        foreach (var lRawImage in l_rawImages)
        {
            lRawImage.material = l_material;
        }
        
        r_material = new Material(shader);
        foreach (var rRawImage in r_rawImages)
        {
            rRawImage.material = r_material;
        }
    }

    void Update()
    {
        UpdateColor();
    }

    private void UpdateColor()
    {
        color.a = l_opacity.value;
        l_material.color = color;
        color.a = r_opacity.value;
        r_material.color = color;
    }

    private void SetColor(Color buttonColor)
    {
        color = buttonColor;
    }

    private void SetRawImageColor(int num)
    {
        if (num > 7)
            return;
        
        controlRawImage.color = ColorIntConverter.ToColors(num)[0];
    }

    public void SetTexture(RenderTexture renderTexture)
    {
        if (isSelect)
        {
            previewRawImage.texture = renderTexture;
            foreach (var l_RawImage in l_rawImages)
            {
                l_RawImage.texture = renderTexture;
            }
            foreach (var r_RawImage in r_rawImages)
            {
                r_RawImage.texture = renderTexture;
            }
        }
    }
    
    
}
