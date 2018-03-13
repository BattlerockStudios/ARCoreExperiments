using Photon;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Battlerock
{
    public class MultiplayerManager : PunBehaviour
    {
        #region Public Variables
        public static MultiplayerManager Instance;

        public bool isReady = false;

        public PhotonPlayer LocalPlayer
        {
            get
            {
                if (m_localPlayer == null)
                {
                    m_localPlayer = PhotonNetwork.player;
                    return m_localPlayer;
                }
                else
                {
                    return m_localPlayer;
                }
            }
        }

        public PhotonPlayer OtherPlayer
        {
            get
            {
                if (m_otherPlayer == null)
                {
                    if (PhotonNetwork.playerList.Length > 1)
                    {
                        m_otherPlayer = PhotonNetwork.player.GetNext();
                        return m_otherPlayer;
                    }
                    else
                    {
                        Debug.LogError("No OTHER player has joined the server yet.");
                        return null;
                    }
                }
                else
                {
                    return m_otherPlayer;
                }
            }
        }

        public int NumberOfPlayers
        {
            get { return PhotonNetwork.playerList.Length; }
        }

        public UnityEngine.UI.Text LocalPlayerText;
        public UnityEngine.UI.Text RemotePlayerText;

        public Color localColor;
        public Color otherColor;
        #endregion

        #region Private Variables
        private PhotonPlayer m_localPlayer;
        private PhotonPlayer m_otherPlayer;
        #endregion

        #region Unity Methods
        /// <summary>
        /// Unity's built-in method (Called before anything else)
        /// </summary>
        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            UpdatePlayerInfo();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Called after disconnecting from the Photon server.
        /// </summary>
        /// <remarks>
        /// In some cases, other callbacks are called before OnDisconnectedFromPhoton is called.
        /// Examples: OnConnectionFail() and OnFailedToConnectToPhoton().
        /// </remarks>
        public override void OnDisconnectedFromPhoton()
        {
            Debug.LogError(GetType() + ":Disconnected");
            SceneManager.LoadScene(LEVEL.PongLauncher.ToString());
        }

        private void UpdatePlayerInfo()
        {
            if (SceneManager.GetActiveScene().buildIndex != (int)NetworkSettings.Instance.level) return;

            m_otherPlayer = PhotonNetwork.player.GetNext();
            m_localPlayer = PhotonNetwork.player;

            if (m_localPlayer != null)
            {
                //player is initiliazed set starting score and color
                if (LocalPlayerText == null)
                {
                    LocalPlayerText = GameObject.FindWithTag("LocalText").GetComponent<UnityEngine.UI.Text>();
                    LocalPlayerText.color = new Color(m_localPlayer.GetColor().x, m_localPlayer.GetColor().y, m_localPlayer.GetColor().z);
                    m_localPlayer.SetScore(5);
                    localColor = new Color(m_localPlayer.GetColor().x, m_localPlayer.GetColor().y, m_localPlayer.GetColor().z);
                }

                //winning condition
                if (m_localPlayer.GetScore() < 1)
                {
                    GameObject.Find("Loser Panel").SetActive(true);
                }

                // should be this format: "YOU   00"
                this.LocalPlayerText.text = m_localPlayer.NickName + " -. " + m_localPlayer.GetScore().ToString() + " .-";

            }

            if (m_otherPlayer != null)
            {
                //player is initiliazed set starting score and color
                if (RemotePlayerText == null)
                {
                    RemotePlayerText = GameObject.FindWithTag("RemoteText").GetComponent<UnityEngine.UI.Text>();
                    RemotePlayerText.color = new Color(m_otherPlayer.GetColor().x, m_otherPlayer.GetColor().y, m_otherPlayer.GetColor().z);
                    m_otherPlayer.SetScore(5);
                    otherColor = new Color(m_otherPlayer.GetColor().x, m_otherPlayer.GetColor().y, m_otherPlayer.GetColor().z);
                }

                //winning condition
                if (m_otherPlayer.GetScore() < 1)
                {
                    GameObject.Find("Winner Panel").SetActive(true);
                }

                // should be this format: "name        00"
                this.RemotePlayerText.text = "-. " + m_otherPlayer.GetScore().ToString() + " .- " + m_otherPlayer.NickName;
            }


        }
        #endregion
    }
}