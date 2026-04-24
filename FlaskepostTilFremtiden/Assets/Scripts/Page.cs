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

    [Header("AR")]
    public GameObject arPrefab;

    [Header("Audio")]
    public AudioClip audioClip;
}