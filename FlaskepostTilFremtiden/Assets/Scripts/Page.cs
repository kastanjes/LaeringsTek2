using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPage", menuName = "Book/Page")]
public class Page : ScriptableObject
{
    [Header("Tracking")]
    public string imageName;

    [Header("State")]
    public AppState appState;

    [Header("UI")]
    public UIPageType uiPageType;
    [TextArea] public string followupText;
    public bool showContinueButton;

    [Header("Continue Flow")]
    [TextArea] public List<string> continueTexts = new List<string>();
    public bool returnToScanningAfterTexts = true;
    public bool showSTTPanelAfterTexts = false;

    [Header("AR")]
    public GameObject arPrefab;

    [Header("Audio")]
    public AudioClip audioClip;
}