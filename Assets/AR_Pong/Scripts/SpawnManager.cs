using System.Collections;
using UnityEngine;

namespace Battlerock
{
    public class SpawnManager : MonoBehaviour
    {
        #region Public Variables
        public Transform[] prefabs;
        #endregion

        #region Unity Methods
        /// <summary>
        /// Unity's built-in method (Called right after Awake)
        /// </summary>
        private void Start()
        {
            // in case we started this demo with the wrong scene being active, simply load the menu scene
            if (!PhotonNetwork.connected)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(LEVEL.PongGame.ToString());
                return;
            }

            _GameManager.Instance.gameMode = GameMode.Preparation;

            for (int i = 0; i < prefabs.Length; i++)
            {
                //if (MultiplayerManager.Instance.anchorPoint != null)
                //{
                //    // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                //    var obj = PhotonNetwork.Instantiate(prefabs[i].name, MultiplayerManager.Instance.anchorPoint.position, Quaternion.identity, 0);
                //    obj.transform.parent = MultiplayerManager.Instance.anchorPoint;
                //    obj.transform.localPosition = Vector3.zero;
                //    Debug.Log(obj.name);
                //}

                Vector3 position = Vector3.zero;
                if(MultiplayerManager.Instance.anchor != null)
                {
                    position = MultiplayerManager.Instance.anchor.transform.position;
                }

                PhotonNetwork.Instantiate(prefabs[i].name, position, Quaternion.identity, 0);

            }
        }
    }
    #endregion
}