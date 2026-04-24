using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    [Header("Page Data")]
    [SerializeField] private List<Page> pages = new List<Page>();

    [Header("Managers")]
    [SerializeField] private UIManager uiManager;
    [SerializeField] private ARContentManager arContentManager;
    [SerializeField] private AudioManager audioManager;

    private Page currentPage;
    private AppState currentState = AppState.Scanning;

    private void Start()
    {
        SetState(AppState.Scanning, null);
    }

    private string currentImageName;

    public void HandleTrackedImage(string imageName, Transform imageTransform)
    {
        Page foundPage = pages.Find(page => page.imageName == imageName);

        if (foundPage == null)
        {
            Debug.LogWarning($"No Page found for image name: {imageName}");
            return;
        }

        if (currentImageName == imageName)
        {
            return;
        }

        currentImageName = imageName;
        currentPage = foundPage;
        SetState(currentPage.appState, imageTransform);
    }

    private void SetState(AppState newState, Transform imageTransform)
    {
        currentState = newState;

        if (uiManager != null)
            uiManager.HideAllUI();

        if (arContentManager != null)
            arContentManager.ClearContent();

        if (audioManager != null && currentPage != null)
            audioManager.PlayPageAudio(currentPage);

        switch (currentState)
        {
            case AppState.Scanning:
                if (uiManager != null)
                    uiManager.ShowScanningUI();
                break;

            case AppState.UIOnly:
                if (uiManager != null && currentPage != null)
                    uiManager.ShowPageUI(currentPage);
                break;

            case AppState.ShowingAR:
                if (uiManager != null && currentPage != null)
                    uiManager.ShowPageUI(currentPage);

                if (arContentManager != null && currentPage != null)
                    arContentManager.ShowPageAR(currentPage, imageTransform);
                break;
        }
    }

    public void OnContinuePressed()
    {
        if (currentPage == null)
            return;

        if (uiManager != null)
            uiManager.ShowFollowupText(currentPage);
    }
}