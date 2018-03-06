using System.Collections;
using UnityEngine;

public class CoroutineRunner
{

    #region Constructors

    public CoroutineRunner(MonoBehaviour behaviour)
    {
        m_behaviour = behaviour;
    }

    #endregion

    #region Public Properties

    public bool IsRunning
    {
        get { return m_coroutineReference != null; }
    }

    #endregion

    #region Private Variables

    private MonoBehaviour m_behaviour = null;
    private Coroutine m_coroutineReference = null;

    #endregion

    #region Public Methods

    public void Start(IEnumerator coroutine, bool stopIfAlreadyRunning)
    {
        if (IsRunning)
        {
            if (stopIfAlreadyRunning)
            {
                m_behaviour.StopCoroutine(m_coroutineReference);
                m_coroutineReference = null;
            }
            else
            {
                Debug.LogWarning("Failed to stop previous coroutine");
                return;
            }
        }

        m_coroutineReference = m_behaviour.StartCoroutine(WrapCoroutine(coroutine));
    }

    public void Stop()
    {
        if (m_coroutineReference == null)
        {
            Debug.LogWarning("Failed to stop coroutine because there is not one currently running");
            return;
        }

        m_behaviour.StopCoroutine(m_coroutineReference);
    }

    #endregion

    #region Private Methods

    private IEnumerator WrapCoroutine(IEnumerator coroutine)
    {
        yield return coroutine;

        // ZAS: ensure that our coroutine reference is cleared out when it ends
        m_coroutineReference = null;
    }

    #endregion

}
