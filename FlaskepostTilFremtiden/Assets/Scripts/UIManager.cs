using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Name Input")]
    [SerializeField] private TMP_InputField nameInput1;
    [SerializeField] private TMP_InputField nameInput2;

    private void Start()
    {
        if (nameInput1 != null)
            nameInput1.onValueChanged.AddListener(val => PlayerData.Name1 = val);
        if (nameInput2 != null)
            nameInput2.onValueChanged.AddListener(val => PlayerData.Name2 = val);
    }

    [Header("Panels")]
    [SerializeField] private GameObject scanningPanel;
    [SerializeField] private GameObject introPanel;
    [SerializeField] private GameObject persistentUI;

    [Header("Optional UI")]
    [SerializeField] private TMP_Text followupTextLabel;
    [SerializeField] private GameObject continueButton;

    [SerializeField] private GameObject introTextObject;
    [SerializeField] private GameObject followupTextObject;

    [Header("STT Panel")]
    [SerializeField] private GameObject sttPanel;



    public void HideAllUI()
    {
        if (scanningPanel != null)
            scanningPanel.SetActive(false);

        if (introPanel != null)
            introPanel.SetActive(false);

        if (persistentUI != null)
            persistentUI.SetActive(false);

        if (sttPanel != null)
            sttPanel.SetActive(false);
    }

    public void ShowSTTPanel()
    {
        HideAllUI();

        if (sttPanel != null)
            sttPanel.SetActive(true);
    }

    public void SendLetter()
    {
        if (sttPanel != null)
            sttPanel.SetActive(false);

        if (followupTextObject != null)
            followupTextObject.SetActive(false);

        if (followupTextLabel != null)
            followupTextLabel.text = "";

        if (persistentUI != null)
            persistentUI.SetActive(true);

        if (continueButton != null)
            continueButton.SetActive(true);
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

    public void ShowCustomText(string text)
    {
        if (introTextObject != null)
            introTextObject.SetActive(false);

        if (followupTextObject != null)
            followupTextObject.SetActive(true);

        if (followupTextLabel != null)
            followupTextLabel.text = text;
    }
}