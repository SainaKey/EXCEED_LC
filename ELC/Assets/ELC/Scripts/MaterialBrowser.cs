using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialBrowser : MonoBehaviour
{
    [SerializeField] private List<GameObject> iSetTextureObjList = new List<GameObject>();
    private List<ISetShader> iSetTextures = new List<ISetShader>();
    [SerializeField] private List<MaterialButton> matButtons = new List<MaterialButton>(); 
    void Start()
    {
        iSetTextures.Clear();
        
        foreach (var iSetTextureObj in iSetTextureObjList)
        {
            if (iSetTextureObj.TryGetComponent(out ISetShader iSetTexture))
            {
                iSetTextures.Add(iSetTexture);
            }
        }

        foreach (var matButton in matButtons)
        {
            matButton.Button.onClick.AddListener(() =>
            {
                foreach (var iSetTexture in iSetTextures)
                {
                    iSetTexture.SetShader(matButton.Shader);
                }
            });
        }
    }
}
