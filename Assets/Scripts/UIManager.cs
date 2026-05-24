using System.Collections;
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
    [SerializeField] private CanvasGroup sttCanvasGroup;
    [SerializeField] private float sttFadeDuration = 4f;
    private Coroutine _sttFadeCoroutine;

    [Header("Info Button")]
    [SerializeField] private GameObject infoTextObject;
    private bool _infoTextVisible = false;

    [Header("Hint System")]
    [SerializeField] private GameObject hintPanel;



    public void ToggleInfoText()
    {
        if (infoTextObject == null) return;

        _infoTextVisible = !_infoTextVisible;
        infoTextObject.SetActive(_infoTextVisible);
    }

    public void ShowHint()
    {
        if (hintPanel != null)
            hintPanel.SetActive(true);
    }

    public void HideHint()
    {
        if (hintPanel != null)
            hintPanel.SetActive(false);
    }

    public void HideAllUI()
    {
        if (scanningPanel != null)
            scanningPanel.SetActive(false);

        if (introPanel != null)
            introPanel.SetActive(false);

        if (persistentUI != null)
            persistentUI.SetActive(false);

        if (sttPanel != null)
        {
            if (_sttFadeCoroutine != null)
            {
                StopCoroutine(_sttFadeCoroutine);
                _sttFadeCoroutine = null;
            }
            if (sttCanvasGroup != null)
                sttCanvasGroup.alpha = 1f;
            sttPanel.SetActive(false);
        }

        if (followupTextObject != null)
            followupTextObject.SetActive(false);

        HideHint();
    }

    public void ShowSTTPanel()
    {
        HideAllUI();

        if (sttCanvasGroup != null)
            sttCanvasGroup.alpha = 1f;

        if (sttPanel != null)
            sttPanel.SetActive(true);
    }

    private IEnumerator FadeOutSTTPanel()
    {
        float elapsed = 0f;

        while (elapsed < sttFadeDuration)
        {
            elapsed += Time.deltaTime;
            if (sttCanvasGroup != null)
                sttCanvasGroup.alpha = 1f - (elapsed / sttFadeDuration);
            yield return null;
        }

        if (sttPanel != null)
            sttPanel.SetActive(false);

        if (sttCanvasGroup != null)
            sttCanvasGroup.alpha = 1f;

        _sttFadeCoroutine = null;
    }

    public void SendLetter()
    {
        if (sttPanel != null)
        {
            if (_sttFadeCoroutine != null)
                StopCoroutine(_sttFadeCoroutine);
            _sttFadeCoroutine = StartCoroutine(FadeOutSTTPanel());
        }

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