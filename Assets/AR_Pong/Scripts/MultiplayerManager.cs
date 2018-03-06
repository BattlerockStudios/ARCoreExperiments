using Photon;
using UnityEngine;

namespace Battlerock
{
    public class MultiplayerManager : PunBehaviour
    {
        #region Public Variables
        public static MultiplayerManager Instance;

        /// <summary>
        /// Anchor point used by the player.
        /// </summary>
        public GoogleARCore.Anchor anchor;
        #endregion

        #region Private Variables
        private GameObject m_placeHolder;
        #endregion

        #region Unity Methods
        /// <summary>
        /// Unity's built-in method (Called before anything else)
        /// </summary>
        private void Awake()
        {
            Instance = this;
        }
        #endregion

        #region Public Methods
        public void FindOrCreateAnchor()
        {
            if (PhotonNetwork.player.IsMasterClient == true)
            {
                if (m_placeHolder == null)
                {
                    m_placeHolder = PhotonNetwork.Instantiate("Anchor", Vector3.zero, Quaternion.identity, 0);
                    Debug.Log(m_placeHolder.transform.position); // all the location, localPosition, quaternion etc will be available to you
                    DontDestroyOnLoad(m_placeHolder);                   
                }               

                anchor = GoogleARCore.Session.CreateAnchor(new Pose());
                anchor.ThrowErrorIfNull();
                anchor.transform.position = m_placeHolder.transform.position;
            }
        }
        #endregion
    }
}