using System.Collections;
using UnityEngine;

public class ARSceneSequence : MonoBehaviour
{
    [Header("Animators")]
    [SerializeField] private Animator girlAnimator;
    [SerializeField] private Animator boyAnimator;

    [Header("Optional VFX")]
    [SerializeField] private GameObject vfxObject;

    private void Start()
    {
        StartCoroutine(PlaySequence());
    }

    private IEnumerator PlaySequence()
    {
        // Step 1
        girlAnimator.SetTrigger("Lay");
        boyAnimator.SetTrigger("Sitting");

        yield return new WaitForSeconds(2f);
    }
}