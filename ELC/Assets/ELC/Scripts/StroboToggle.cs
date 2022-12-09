using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StroboToggle : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    [SerializeField] private RawImage stroboRawImage;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
