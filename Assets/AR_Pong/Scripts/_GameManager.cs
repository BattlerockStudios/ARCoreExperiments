using UnityEngine;

namespace Battlerock
{
    public enum GameMode
    {
        Lobby,
        Preparation,
        Playing,
        Results
    }

    public class _GameManager : Singleton<_GameManager>
    {
        #region Public Variables
        [Tooltip("Determines what the Game State is currently at.")]
        public GameMode gameMode = GameMode.Lobby;
        #endregion

        #region Unity Methods
        /// <summary>
        /// Unity's built-in method (Called before anything else)
        /// </summary>
        private void Awake()
        {
            InitializeSubSystems();
        }
        #endregion

        #region Public Methods

        #endregion

        #region Private Methods
        /// <summary>
        /// Creates any missing sub-systems if they are NOT already in the scene.
        /// Then makes them a child of the _GameManager object.
        /// </summary>
        private void InitializeSubSystems()
        {
            if (MultiplayerManager.Instance == null)
            {
                GameObject multiplayerManager = new GameObject();
                multiplayerManager.name = "_MultiplayerController_";
                multiplayerManager.AddComponent<MultiplayerManager>();
                multiplayerManager.transform.parent = this.transform;
            }
        }
        #endregion
    }
}