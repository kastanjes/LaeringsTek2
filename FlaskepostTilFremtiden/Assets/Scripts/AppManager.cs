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
    private int currentContinueIndex = 0;
    private string currentImageName;
    private bool letterSent = false;


    private void Start()
    {
        SetState(AppState.Scanning, null);
    }

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

        currentContinueIndex = 0;
        
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

        if (currentPage.continueTexts != null && currentContinueIndex < currentPage.continueTexts.Count)
        {
            string text = currentPage.continueTexts[currentContinueIndex]
                .Replace("{navn1}", PlayerData.Name1)
                .Replace("{navn2}", PlayerData.Name2);
            uiManager.ShowCustomText(text);
            currentContinueIndex++;
            return;
        }

        if (currentPage.showSTTPanelAfterTexts && !letterSent)
        {
            uiManager.ShowSTTPanel();
            return;
        }

        if (letterSent || currentPage.returnToScanningAfterTexts)
        {
            ResetToScanning();
        }
    }

    public void OnSendLetterPressed()
    {
        letterSent = true;

        if (uiManager != null)
            uiManager.SendLetter();
    }

    public void OnMutePressed()
    {
        if (audioManager != null)
            audioManager.ToggleMute();
    }

    public void ResetToScanning()
    {
        currentPage = null;
        currentContinueIndex = 0;
        letterSent = false;

        if (arContentManager != null)
            arContentManager.ClearContent();

        SetState(AppState.Scanning, null);
    }
}