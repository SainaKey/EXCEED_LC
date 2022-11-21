using System;
using System.Collections;
using System.Collections.Generic;
using MIDI2uGUI;
using MidiJack;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MIDIAssignInfos
{
    public List<MIDIAssignInfo> MidiAssignInfoList = new List<MIDIAssignInfo>();
}

public class MIDIAssignManager : MonoBehaviour
{
    public ToggleGroup toggleGroup;
    public List<MIDIAssigner> midiAssignerList = new List<MIDIAssigner>();
    //public List<MIDIAssigner> midiAssigners = new List<MIDIAssigner>();

    private static MIDIAssignManager instance;

    private MIDIAssignManager () { // Private Constructor

        Debug.Log("Create MIDIAssignManager GameObject instance.");
    }

    public static MIDIAssignManager Instance {

        get {

            if( instance == null ) {

                GameObject go = new GameObject("MIDIAssignManager");
                instance = go.AddComponent<MIDIAssignManager>();
            }

            return instance;
        }
    }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        toggleGroup = gameObject.AddComponent<ToggleGroup>();
        toggleGroup.allowSwitchOff = true;
        MidiMaster.noteOnDelegate += NoteOn;
        MidiMaster.noteOffDelegate += NoteOff;
        MidiMaster.knobDelegate += Knob;
    }

    private void NoteOn(MidiChannel midiChannel, int noteNum, float velocity)
    {
        foreach (var midiAssigner in midiAssignerList)
        {
            midiAssigner.OnMIDISignal(midiChannel,noteNum,velocity);
        }
    }

    private void NoteOff(MidiChannel midiChannel, int noteNum)
    {
        foreach (var midiAssigner in midiAssignerList)
        {
            midiAssigner.OffMIDISignal(midiChannel,noteNum,0.0f);
        }
    }

    private void Knob(MidiChannel midiChannel, int knobNum, float knobValue)
    {
        foreach (var midiAssigner in midiAssignerList)
        {
            midiAssigner.OnMIDISignal(midiChannel,knobNum,knobValue);
        }
    }

    private void Save()
    {
        MIDIAssignInfos midiAssignInfos = new MIDIAssignInfos();
        foreach (var midiAssigner in midiAssignerList)
        {
            midiAssignInfos.MidiAssignInfoList.Add(midiAssigner.midiAssignInfo);
        }
        var jsonStr = JsonUtility.ToJson(midiAssignInfos, false);
        MidiAssignDataIO.OutputData(jsonStr);
    }

    private void Load()
    {
        var jsonStr = MidiAssignDataIO.InputJsonData();
        var data = JsonUtility.FromJson<MIDIAssignInfos>(jsonStr);
        foreach (var loadedMidiAssignInfo in data.MidiAssignInfoList)
        {
            foreach (var midiAssigner in midiAssignerList)
            {
                if (midiAssigner.midiAssignInfo.guid == loadedMidiAssignInfo.guid)
                {
                    midiAssigner.midiAssignInfo.midiInfos = loadedMidiAssignInfo.midiInfos;
                }
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            foreach (var midiAssigner in midiAssignerList)
            {
                midiAssigner.MIDIMappingReadyModeOn();
            }
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            foreach (var midiAssigner in midiAssignerList)
            {
                midiAssigner.MIDIMappingReadyModeOff();
            }
        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            foreach (var midiAssigner in midiAssignerList)
            {
                midiAssigner.DeleteMIDI();
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }
}
