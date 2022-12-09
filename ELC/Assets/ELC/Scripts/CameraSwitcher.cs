using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private RawImage targetRawImage;
    [SerializeField] private Toggle[] toggles;

    void Start()
    {
        for (int i = 0; i < toggles.Count(); i++)
        {
            var toggle = toggles[i];
            var index = i;
            toggle.onValueChanged.AddListener(value =>
            {
                if (value)
                {
                    targetRawImage.color = ColorIntConverter.ToColors(index)[0];
                }
            });
        }
    }
    
}
