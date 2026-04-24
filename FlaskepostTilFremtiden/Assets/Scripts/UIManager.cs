using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject scanningPanel;
    [SerializeField] private GameObject introPanel;
    [SerializeField] private GameObject persistentUI;

    [Header("Optional UI")]
    [SerializeField] private TMP_Text followupTextLabel;
    [SerializeField] private GameObject continueButton;

    public void HideAllUI()
    {
        if (scanningPanel != null)
            scanningPanel.SetActive(false);

        if (introPanel != null)
            introPanel.SetActive(false);

        if (persistentUI != null)
            persistentUI.SetActive(false);
    }

    public void ShowScanningUI()
    {
        HideAllUI();

        if (scanningPanel != null)
            scanningPanel.SetActive(true);
    }

    public void ShowPageUI(Page page)
    {
        HideAllUI();

        switch (page.uiPageType)
        {
            case UIPageType.Intro:
                if (introPanel != null)
                    introPanel.SetActive(true);
                break;

            case UIPageType.Scanning:
                if (scanningPanel != null)
                    scanningPanel.SetActive(true);
                break;
        }

        if (persistentUI != null)
            persistentUI.SetActive(true);

        if (continueButton != null)
            continueButton.SetActive(page.showContinueButton);

        if (followupTextLabel != null)
            followupTextLabel.text = "";
    }

    public void ShowFollowupText(Page page)
    {
        if (followupTextLabel != null)
            followupTextLabel.text = page.followupText;
    }
}