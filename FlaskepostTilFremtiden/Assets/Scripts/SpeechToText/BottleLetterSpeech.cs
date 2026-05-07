using UnityEngine;
using UnityEngine.UI;
using TextSpeech;
using UnityEngine.Android;
using System.Collections;

public class BottleLetterSpeech : MonoBehaviour
{
    public Text inputText;

    void Start()
{
    StartCoroutine(SetupSpeech());
}

IEnumerator SetupSpeech()
{
    yield return null;

    SpeechToText.Instance.Setting("da-DK");
    SpeechToText.Instance.onResultCallback = OnSpeechResult;

#if UNITY_ANDROID && !UNITY_EDITOR
    SpeechToText.Instance.isShowPopupAndroid = true;
    Permission.RequestUserPermission(Permission.Microphone);
#endif
}

    public void StartRecording()
    {
        SpeechToText.Instance.Setting("da-DK");
        SpeechToText.Instance.isShowPopupAndroid = true;
        SpeechToText.Instance.onResultCallback = OnSpeechResult;

#if UNITY_EDITOR
        OnSpeechResult("Test i editor");
#else
        SpeechToText.Instance.StartRecording("Sig hvad du vil sende i brevet");
#endif
    }

    void OnSpeechResult(string result)
    {
        if (inputText.text == "Lytter..." || string.IsNullOrWhiteSpace(inputText.text))
            inputText.text = result;
        else
            inputText.text += " " + result;
    }

    public void SetSpeechText(string result)
    {
        Debug.Log("SetSpeechText: " + result);
        AddText(result);
    }

    void AddText(string result)
    {
        if (inputText.text == "Lytter..." || string.IsNullOrWhiteSpace(inputText.text))
            inputText.text = result;
        else
            inputText.text += " " + result;
    }

    public void ClearText()
    {
        inputText.text = "Lytter...";
    }
}