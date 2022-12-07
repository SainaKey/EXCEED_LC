using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedController : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float speed;
    [SerializeField] private Color[] colors;
    [SerializeField] private RawImage[] rawImages;
    // Start is called before the first frame update
    void Start()
    {
        slider.onValueChanged.AddListener(value => ChangeSpeed(value));
    }

    private void ChangeSpeed(float value)
    {
        speed = value;
        var a = (int)((speed + 5f) / 10f * 511f);
        var index = 0;
        colors = new Color[3];
        foreach (var col in ColorIntConverter.ToColors(a))
        {
            colors[index] = col;
            index++;
        }
        for (int i = 0; i < colors.Length; i++)
        {
            rawImages[i].color = colors[i];
        }
    }
}
