using System;
using System.Collections;
using System.Collections.Generic;
using MIDI2uGUI;
using MidiJack;
using UnityEngine;
using UnityEngine.UI;

public class MIDIAssignManager : MonoBehaviour
{
    //save
    //load
    //midiInputをmidiassignerに伝える
    public ToggleGroup toggleGroup;
    public List<MIDIAssigner> midiAssigners = new List<MIDIAssigner>();

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
        foreach (var midiAssigner in midiAssigners)
        {
            midiAssigner.OnMIDISignal(midiChannel,noteNum,velocity);
        }
    }

    private void NoteOff(MidiChannel midiChannel, int noteNum)
    {
        foreach (var midiAssigner in midiAssigners)
        {
            midiAssigner.OffMIDISignal(midiChannel,noteNum,0.0f);
        }
    }

    private void Knob(MidiChannel midiChannel, int knobNum, float knobValue)
    {
        foreach (var midiAssigner in midiAssigners)
        {
            midiAssigner.OnMIDISignal(midiChannel,knobNum,knobValue);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            foreach (var midiAssigner in midiAssigners)
            {
                midiAssigner.MIDIMappingReadyModeOn();
            }
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            foreach (var midiAssigner in midiAssigners)
            {
                midiAssigner.MIDIMappingReadyModeOff();
            }
        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            foreach (var midiAssigner in midiAssigners)
            {
                midiAssigner.DeleteMIDI();
            }
        }
    }
}
