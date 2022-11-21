using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using MidiJack;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace MIDI2uGUI
{
    [System.Serializable]
    public enum uGUIType
    {
        Button,
        Toggle,
        Slider
    }

    [System.Serializable]
    public class MidiInfo
    {
        public MidiChannel midiChannel;
        public int midiNum;

        public MidiInfo(MidiChannel midiChannel, int midiNum)
        {
            this.midiChannel = midiChannel;
            this.midiNum = midiNum;
        }
    }
    [System.Serializable]
    public class MIDIAssignInfo
    {
        public string guid;
        public List<MidiInfo> midiInfos = new List<MidiInfo>();
    }
    public class MIDIAssigner : MonoBehaviour
    {
        [SerializeField] private uGUIType uGUIType;
        public MIDIAssignInfo midiAssignInfo;
        private Toggle instanceToggle;
        private TMP_Text instanceText;
        private GameObject instanceToggleObj;
        private Button button;
        private Toggle toggle;
        private Slider slider;

        private PointerEventData eventData;

        private bool isSelect = false;
        private void Awake()
        {
            if (midiAssignInfo.guid == "")
            {
                //midiAssignInfo.guid = Guid.NewGuid().ToString("N");
                Debug.LogError("this midiassigner doesnt have guid");
            }
            
            if (TryGetComponent(out Button b))
            {
                uGUIType = uGUIType.Button;
                button = b;
            }
            else if (TryGetComponent(out Toggle t))
            {
                uGUIType = uGUIType.Toggle;
                toggle = t;
            }
            else if (TryGetComponent(out Slider s))
            {
                uGUIType = uGUIType.Slider;
                slider = s;
            }
            else
            {
                Debug.LogError("uGUI is not assigned");
            }
        }

        private void Start()
        {
            var obj = (GameObject)Resources.Load ("MIDIAssignToggle");
            instanceToggleObj = Instantiate(obj,transform);
            var midiAssignTogglePresenter = instanceToggleObj.GetComponent<MIDIAssignTogglePresenter>();
            instanceToggle = midiAssignTogglePresenter.Toggle;
            instanceText = midiAssignTogglePresenter.Text;
            instanceToggle.group = MIDIAssignManager.Instance.toggleGroup;
            instanceToggle.onValueChanged.AddListener(value => OnChangeMIDIAssignToggle(value));
            instanceToggleObj.SetActive(false);
            
            MIDIAssignManager.Instance.midiAssignerList.Add(this);

            eventData = new PointerEventData(null);
        }

        public void MIDIMappingReadyModeOn()
        {
            instanceToggleObj.SetActive(true);
        }
        
        public void MIDIMappingReadyModeOff()
        {
            instanceToggle.isOn = false;
            instanceToggleObj.SetActive(false);
        }

        private void OnChangeMIDIAssignToggle(bool b)
        {
            isSelect = b;
        }

        public void OnMIDISignal(MidiChannel midiChannel, int midiNum , float value)
        {
            if (isSelect)
            {
                AssignMIDI(midiChannel,midiNum);
            }
            MIDI2uGUI(midiChannel,midiNum,value);
        }
        
        public void OffMIDISignal(MidiChannel midiChannel, int midiNum , float value)
        {
            if (isSelect)
            {
                //AssignMIDI(midiChannel,midiNum);
            }
            MIDI2uGUI(midiChannel,midiNum,value);
        }

        private void AssignMIDI(MidiChannel midiChannel, int midiNum)
        {
            bool isAdded = false;
            var midiInfos = midiAssignInfo.midiInfos;
            foreach (var midiInfo in midiInfos)
            {
                if (midiInfo.midiChannel == midiChannel)
                {
                    Debug.Log(midiInfo.midiNum + "a" + midiNum);
                    if (midiInfo.midiNum == midiNum)
                    {
                        isAdded = true;
                        Debug.Log("a");
                        return;
                    }
                }
                
            }
            if(!isAdded)
                midiInfos.Add(new MidiInfo(midiChannel,midiNum));

            UpdateText();
        }

        public void DeleteMIDI()
        {
            if (isSelect)
            {
                midiAssignInfo.midiInfos.RemoveAt(midiAssignInfo.midiInfos.Count - 1);
            }
            UpdateText();
        }

        private void UpdateText()
        {
            string midiInfoStr = "";
            for (int i = midiAssignInfo.midiInfos.Count - 1; i >= 0; i--)
            {
                var midiInfo = midiAssignInfo.midiInfos[i];
                var midiChStr = "Ch:" + midiInfo.midiChannel + ",";
                var midiNumStr = "Num:" + midiInfo.midiNum;
                midiInfoStr += midiChStr + midiNumStr + "/";
            }


            instanceText.text = midiInfoStr;
        }

        private void MIDI2uGUI(MidiChannel midiChannel, int midiNum,float value)
        {
            bool isAdded = false;
            var midiInfos = midiAssignInfo.midiInfos;
            foreach (var midiInfo in midiInfos)
            {
                if (midiInfo.midiChannel == midiChannel)
                {
                    if (midiInfo.midiNum == midiNum)
                    {
                        isAdded = true;
                    }
                }
                
            }

            if (!isAdded)
                return;
            
            if (uGUIType == uGUIType.Button)
                MIDI2Button(midiChannel, midiNum, value);
            else if(uGUIType == uGUIType.Toggle)
                MIDI2Toggle(midiChannel, midiNum, value);
            else if(uGUIType == uGUIType.Slider)
                MIDI2Slider(midiChannel, midiNum, value);
        }

        private void MIDI2Button(MidiChannel midiChannel, int midiNum, float value)
        {
            if (value >= 0.5)
            {
                button.OnPointerClick(eventData);
            }
            else
            {
                
            }
        }

        private void MIDI2Toggle(MidiChannel midiChannel, int midiNum, float value)
        {
            if (value >= 0.5)
            {
                toggle.isOn = !toggle.isOn;
            }
        }

        private void MIDI2Slider(MidiChannel midiChannel, int midiNum, float value)
        {
            //Debug.Log(value);
            slider.normalizedValue = value;
        }

        private void OnDestroy()
        {
            var tmpList = new List<MIDIAssigner>(MIDIAssignManager.Instance.midiAssignerList);
            tmpList.Remove(this);
            MIDIAssignManager.Instance.midiAssignerList = tmpList;
        }
    }

}
