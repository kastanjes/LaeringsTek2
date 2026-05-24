using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TrackedImageHandler : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager trackedImageManager;
    [SerializeField] private AppManager appManager;

    private void OnEnable()
    {
        Debug.Log("TrackedImageHandler enabled");

        if (trackedImageManager != null)
            trackedImageManager.trackablesChanged.AddListener(OnTrackedImagesChanged);
    }

    private void OnDisable()
    {
        if (trackedImageManager != null)
            trackedImageManager.trackablesChanged.RemoveListener(OnTrackedImagesChanged);
    }

    private void OnTrackedImagesChanged(ARTrackablesChangedEventArgs<ARTrackedImage> args)
    {
        Debug.Log($"Tracked images changed. Added: {args.added.Count}, Updated: {args.updated.Count}, Removed: {args.removed.Count}");
        
        foreach (ARTrackedImage trackedImage in args.added)
        {
            ProcessTrackedImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in args.updated)
        {
            ProcessTrackedImage(trackedImage);
        }
    }

    private void ProcessTrackedImage(ARTrackedImage trackedImage)
    {
        if (trackedImage.trackingState != TrackingState.Tracking)
            return;

        string imageName = trackedImage.referenceImage.name;
        appManager.HandleTrackedImage(imageName, trackedImage.transform);
    }
}