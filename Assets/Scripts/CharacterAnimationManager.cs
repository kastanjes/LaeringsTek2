using UnityEngine;

public class CharacterAnimationManager : MonoBehaviour
{
    [SerializeField] private string girlName = "GirlWRig";
    [SerializeField] private string boyName = "Drengen";

    public void TriggerAnimation(GameObject spawnedObject, string triggerName)
    {
        if (spawnedObject == null)
        {
            Debug.LogWarning("CharacterAnimationManager: spawnedObject er null");
            return;
        }

        Debug.Log($"CharacterAnimationManager: Trigger '{triggerName}' på '{spawnedObject.name}'");

        SetTriggerByName(spawnedObject, girlName, triggerName);
        SetTriggerByName(spawnedObject, boyName, triggerName);
    }

    private void SetTriggerByName(GameObject root, string objectName, string triggerName)
    {
        foreach (Transform child in root.GetComponentsInChildren<Transform>())
        {
            if (child.name == objectName)
            {
                Animator anim = child.GetComponent<Animator>();
                if (anim != null)
                {
                    Debug.Log($"CharacterAnimationManager: Fandt '{objectName}', sætter trigger '{triggerName}'");
                    anim.SetTrigger(triggerName);
                }
                else
                {
                    Debug.LogWarning($"CharacterAnimationManager: Fandt '{objectName}' men ingen Animator!");
                }
                return;
            }
        }

        Debug.LogWarning($"CharacterAnimationManager: Kunne ikke finde '{objectName}' i '{root.name}'");
    }
}
