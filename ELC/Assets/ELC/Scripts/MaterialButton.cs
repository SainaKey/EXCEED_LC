using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image image;
    private Material material;
    [SerializeField] private Shader shader;

    public Button Button
    {
        get { return this.button; }
    }

    public Shader Shader
    {
        get { return this.shader; }
    }
    void Start()
    {
        material = new Material(shader);
        image.material = material;
    }
    
}
