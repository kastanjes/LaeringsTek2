using UnityEngine;

public class ARSceneSequence : MonoBehaviour
{
    [Header("Animators")]
    [SerializeField] private Animator girlAnimator;
    [SerializeField] private Animator boyAnimator;

    [Header("Optional VFX")]
    [SerializeField] private GameObject vfxObject;

    [Header("Animations")]
    [SerializeField] private string girlTrigger;
    [SerializeField] private string boyTrigger;

    private void Start()
    {
        if (girlAnimator != null && !string.IsNullOrEmpty(girlTrigger))
            girlAnimator.SetTrigger(girlTrigger);

        if (boyAnimator != null && !string.IsNullOrEmpty(boyTrigger))
            boyAnimator.SetTrigger(boyTrigger);
    }
}
