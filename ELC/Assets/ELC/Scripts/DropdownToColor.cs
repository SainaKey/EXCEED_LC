using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DropdownToColor : MonoBehaviour
{
    [SerializeField] private int maxCount;
    [SerializeField] private int currentCount;
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private Color[] colors;
    [SerializeField] private RawImage[] rawImages;
    
    // Start is called before the first frame update
    void Start()
    {
        dropdown.options.Clear();
        for (int i = 0; i < maxCount; i++)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData(i.ToString()));
        }
        dropdown.onValueChanged.AddListener(value => OnDropDownChange(value));
    }

    private void OnDropDownChange(int value)
    {
        currentCount = value;
        colors = new Color[2];
        var tmpColors = ColorIntConverter.ToColors(currentCount);
        int dif = Mathf.Abs(tmpColors.Length - colors.Length);
        Array.Copy(tmpColors,0,colors,dif,tmpColors.Length);
        for (int i = 0; i < colors.Length; i++)
        {
            rawImages[i].color = colors[i];
        }
    }
}
