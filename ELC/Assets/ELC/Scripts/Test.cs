using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private int inputInt;
    [SerializeField] private Color[] colors;
    [SerializeField] private int outputInt;


    // Update is called once per frame
    void Update()
    {
        colors = ColorIntConverter.ToColors(inputInt);
        outputInt = ColorIntConverter.ToInt(colors);
    }

    public void ButtonTest()
    {
        Debug.Log("BUttonTest");
    }

}
