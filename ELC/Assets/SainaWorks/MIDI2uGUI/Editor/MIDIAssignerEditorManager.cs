using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace MIDI2uGUI
{
    public class MIDIAssignerEditorManager : IPreprocessBuildWithReport
    {
        public int callbackOrder { get { return 0; } }
        public void OnPreprocessBuild(BuildReport report)
        {
            Debug.Log("MyCustomBuildProcessor.OnPreprocessBuild for target " + report.summary.platform + " at path " + report.summary.outputPath);
            var midiassigners = GameObject.FindObjectsOfType(typeof(MIDIAssigner));
            foreach (MIDIAssigner midiassigner in midiassigners)
            {
                if (midiassigner.midiAssignInfo.guid == "")
                {
                    midiassigner.midiAssignInfo.guid = Guid.NewGuid().ToString("N");
                    Debug.Log(midiassigner.midiAssignInfo.guid);
                }
            }
        }
    }

}
