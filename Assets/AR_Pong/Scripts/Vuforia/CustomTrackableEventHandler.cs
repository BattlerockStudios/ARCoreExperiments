using Vuforia;
using UnityEngine;

public class CustomTrackableEventHandler : MonoBehaviour,
                                           ITrackableEventHandler
{
    private TrackableBehaviour mTrackableBehaviour;
    public UnityEngine.UI.Text debugText;

    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    public void OnTrackableStateChanged(
                                TrackableBehaviour.Status previousStatus,
                                TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound();
            // **** Your own logic here ****
        }
        else
        {
            OnTrackingLost();
        }
    }

    protected virtual void OnTrackingFound()
    {
        Battlerock.MultiplayerManager.Instance.FindOrCreateAnchor();

        debugText.text = "<Color=green>FOUND!</Color>";

        // VuforiaBehaviour.Instance.enabled = false;

        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        // Enable rendering:
        foreach (var component in rendererComponents)
            component.enabled = true;

        // Enable colliders:
        foreach (var component in colliderComponents)
            component.enabled = true;

        // Enable canvas':
        foreach (var component in canvasComponents)
            component.enabled = true;
    }


    protected virtual void OnTrackingLost()
    {
        debugText.text = "<Color=red>Lost...</Color>";

        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        // Disable rendering:
        foreach (var component in rendererComponents)
            component.enabled = false;

        // Disable colliders:
        foreach (var component in colliderComponents)
            component.enabled = false;

        // Disable canvas':
        foreach (var component in canvasComponents)
            component.enabled = false;
    }
}