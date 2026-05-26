using UnityEngine;

public class ARContentManager : MonoBehaviour
{
    [SerializeField] private CharacterAnimationManager characterAnimationManager;

    private GameObject currentSpawnedObject;

    public void ShowPageAR(Page page, Transform imageTransform)
    {
        ClearContent();

        if (page.arPrefab == null || imageTransform == null)
            return;

        currentSpawnedObject = Instantiate(page.arPrefab, imageTransform);
        currentSpawnedObject.transform.localPosition = Vector3.zero;
        currentSpawnedObject.transform.localRotation = Quaternion.identity;

        if (characterAnimationManager != null)
            characterAnimationManager.TriggerAnimation(currentSpawnedObject, page.arPrefab.name);
    }

    public void ClearContent()
    {
        if (currentSpawnedObject != null)
        {
            Destroy(currentSpawnedObject);
            currentSpawnedObject = null;
        }
    }
}