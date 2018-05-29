using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.Animations;

public class Recorder : MonoBehaviour
{
    public AnimationClip clip;
    public bool record = false;

    private GameObjectRecorder m_Recorder;

    void Start()
    {
        m_Recorder = new GameObjectRecorder(gameObject);

        m_Recorder.BindComponent<Transform>(gameObject, true);
    }

    void LateUpdate()
    {
        if (clip == null)
            return;

        if (record)
        {
            m_Recorder.TakeSnapshot(Time.deltaTime);
        }
        else if (m_Recorder.isRecording)
        {
            m_Recorder.SaveToClip(clip);
            m_Recorder.ResetRecording();
        }
    }
}