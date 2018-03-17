using Vuforia;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ImageTargetPlayAudio : MonoBehaviour,
                                            ITrackableEventHandler
{
    private TrackableBehaviour mTrackableBehaviour;
    private AudioSource m_audioSource;

    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }

        m_audioSource = GetComponent<AudioSource>();
    }

    public void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            // Play audio when target is found
            m_audioSource.Play();
        }
        else
        {
            m_audioSource = GetComponent<AudioSource>();
            // Stop audio when target is lost
            m_audioSource.Stop();
        }
    }
}