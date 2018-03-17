using Vuforia;
using UnityEngine;

public class CustomTrackableEventHandler : MonoBehaviour,
                                           ITrackableEventHandler
{
    private TrackableBehaviour mTrackableBehaviour;
    public UnityEngine.UI.Text debugText;

    public GameObject uiControls;

    public GameObject UIControls
    {
        get
        {
            if (uiControls == null)
            {
                try
                {
                    uiControls = GameObject.FindWithTag("UI");
                    return uiControls;
                }
                catch (System.Exception)
                {

                    throw new System.Exception("NO UIControls Object assigned in inspector.");
                }
            }
            else
            {
                return uiControls;
            }
        }
    }

    void Start()
    {
        uiControls.SetActive(false);
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
        Debug.Log("<Color=green>FOUND!</Color>");
        debugText.text = "<Color=green>FOUND!</Color>";

        if(Battlerock.MultiplayerManager.Instance.isReady == false)
        {
            if (Battlerock.MultiplayerManager.Instance.NumberOfPlayers > 1)
            {
                Battlerock.SpawnManager.Instance.SpawnPuck();
            }
            Battlerock.SpawnManager.Instance.SpawnPlayer(transform);
        }        

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
        Debug.Log("<Color=red>Scan AR Marker</Color>");
        debugText.text = "<Color=red>Scan AR Marker</Color>";

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