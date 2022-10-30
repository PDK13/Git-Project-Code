using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

/// <summary>
/// see here https://lightbuzz.com/speech-recognition-unity/
/// </summary>
public class SpeechRecognition : MonoBehaviour
{
    [Header("Start")]

    public string[] m_Keywords = new string[] { "up", "down", "left", "right", "stop" };

    public ConfidenceLevel m_ListenVolumn = ConfidenceLevel.Low;

    [Header("Voice")]

    public string m_Listen;

    protected PhraseRecognizer m_Recognizer;

    private void Start()
    {
        if (m_Keywords != null)
        {
            m_Recognizer = new KeywordRecognizer(m_Keywords, m_ListenVolumn);
            m_Recognizer.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
            m_Recognizer.Start();
            Debug.LogFormat("{0}: Speech Listener runing: {1}", name, m_Recognizer.IsRunning);
        }

        foreach (var m_Device in Microphone.devices)
        {
            Debug.LogFormat("{0}: Found Device(s) name: {1}", name, m_Device);
        }
    }

    private void OnDestroy()
    {
        if (m_Keywords != null && m_Recognizer.IsRunning)
        {
            m_Recognizer.OnPhraseRecognized -= Recognizer_OnPhraseRecognized;
            m_Recognizer.Stop();
        }
    }

    private void Recognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        m_Listen = args.text;
        Debug.LogFormat("{0}: Listen you said: {1}!", name, m_Listen);
    }
}