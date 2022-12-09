/*
using System;
using System.Collections;
using System.Collections.Generic;
using UdonSharpEditor;
using UnityEngine;
using UnityEditor;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;
using VRC.Udon.Editor;

namespace SainaWorks.TextureSamplingCoordinater
{
    public class TextureSamplingCoordinateEditor : EditorWindow
    {
        
        private GameObject targetObj;
        private float xValue;
        private float yValue;
        private Texture2D tex; 
        private float rawXValue;
        private float rawYValue;

        private List<UdonBehaviour> vrStageLightingAudioLinkUdons = new List<UdonBehaviour>();
        
        [MenuItem("SainaWorks/TextureSamplingCoordinater")]
        static void ShowWindow()
        {
            GetWindow(typeof(TextureSamplingCoordinateEditor));
        }
        
        
        void OnGUI()
        {
            var obj = EditorGUILayout.ObjectField(targetObj, typeof(GameObject), true);
            targetObj = obj as GameObject;
            xValue = EditorGUILayout.FloatField(xValue);
            yValue = EditorGUILayout.FloatField(yValue);

            var t = EditorGUILayout.ObjectField(tex, typeof(Texture2D), true);
            tex = t as Texture2D;

            rawXValue = EditorGUILayout.FloatField(rawXValue);
            rawYValue = EditorGUILayout.FloatField(rawYValue);
            
            GUI.DrawTexture(new Rect(0, 200, position.width, position.width/16*9), tex, ScaleMode.StretchToFill);
            
            if (GUILayout.Button("Set"))
            {
                Debug.Log(xValue+","+yValue);
                Set();
            }
            
            var ev = Event.current;
            if (ev.type == EventType.ContextClick)
            {
                var pos = Event.current.mousePosition;
                Debug.Log(pos.ToString());
                xValue = pos.x / position.width;
                rawXValue = xValue * 1920;
                var y = (pos.y - 200) / (position.width / 16 * 9);
                yValue = 1 - y;
                rawYValue = yValue * 1080;
                Repaint();
            }
        }

        private void Set()
        {
            
            vrStageLightingAudioLinkUdons.Clear();
            CollectVRStageLighting_AudioLink(targetObj);
            foreach (var udonBehaviour in vrStageLightingAudioLinkUdons)
            {
                SetUdonVariable(udonBehaviour, new Vector2(xValue,yValue), "textureSamplingCoordinates");
            }
            
        }
        
        private void SetUdonVariable<T>(VRC.Udon.UdonBehaviour udon, T param,string symbol)
        {
            // Udon変数の初期化
            IUdonVariable CreateUdonVariable(string symbolName, object value, System.Type type)
            {
                System.Type udonVariableType = typeof(VRC.Udon.Common.UdonVariable<>).MakeGenericType(type);
                return (IUdonVariable)Activator.CreateInstance(udonVariableType, symbolName, value);
            }

            var programAsset = (UdonSharp.UdonSharpProgramAsset)udon.programSource;
            var publicVariables = udon.publicVariables;

            // Udon変数を取得
            if (!publicVariables.TryGetVariableValue(symbol, out object variableValue))
            {
                variableValue = programAsset.GetPublicVariableDefaultValue(symbol);
            }

            // 変数を設定
            variableValue = param;


            // Udon変数をセットする
            
            if (!publicVariables.TrySetVariableValue(symbol, param))
            {
                if (!publicVariables.TryAddVariable(CreateUdonVariable(symbol, variableValue, typeof(T))))
                {
                    
                }
            }

            PrefabUtility.RecordPrefabInstancePropertyModifications(udon);
        }
        
        private void CollectVRStageLighting_AudioLink(GameObject parent)
        {
            if (parent.TryGetComponent(out VRStageLighting_AudioLink_Static vrStageLightingAudioLink))
            {
                vrStageLightingAudioLinkUdons.Add(parent.GetComponent<UdonBehaviour>());
            }

            foreach (Transform childTrans in parent.transform)
            {
                if (childTrans.gameObject.TryGetComponent(out VRStageLighting_AudioLink_Static vrStageLightingAudioLink2))
                {
                    vrStageLightingAudioLinkUdons.Add(childTrans.GetComponent<UdonBehaviour>());
                }
                else
                {
                    CollectVRStageLighting_AudioLink(childTrans.gameObject);
                }
                
            }
            
            if (parent.TryGetComponent(out VRStageLighting_AudioLink_Laser vrStageLightingAudioLinkLaser))
            {
                vrStageLightingAudioLinkUdons.Add(parent.GetComponent<UdonBehaviour>());
            }

            foreach (Transform childTrans in parent.transform)
            {
                if (childTrans.gameObject.TryGetComponent(out VRStageLighting_AudioLink_Laser vrStageLightingAudioLinkLaser2))
                {
                    vrStageLightingAudioLinkUdons.Add(childTrans.GetComponent<UdonBehaviour>());
                }
                else
                {
                    CollectVRStageLighting_AudioLink(childTrans.gameObject);
                }
                
            }
        }

    }
    
    

}
*/