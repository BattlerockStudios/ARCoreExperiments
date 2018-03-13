using System.Collections;
using UnityEngine;

namespace Battlerock
{
    public class SpawnManager : MonoBehaviour
    {
        #region Public Variables
        public GameObject player;
        public GameObject goal;
        public GameObject puck;

        public SpawnPoint[] playerSpawnPoints;
        public Transform puckSpawnPoint;

        public static SpawnManager Instance;
        #endregion

        #region Private Variables
        private SpawnPoint m_currentSpawnPoint;
        private GameObject m_currentPuck;
        #endregion

        #region Unity Methods
        /// <summary>
        /// Unity's built-in method (Called before other methods)
        /// </summary>
        private void Awake()
        {
            Instance = this;
        }

        /// <summary>
        /// Unity's built-in method (Called right after Awake)
        /// </summary>
        private IEnumerator Start()
        {
            // in case we started this demo with the wrong scene being active, simply load the menu scene
            if (!PhotonNetwork.connected)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(NetworkSettings.Instance.level.ToString());

                yield return null;
            }

#if UNITY_EDITOR
            SpawnPlayer();
#endif
        }
        #endregion

        #region Public Methods
        public void SpawnPlayer(Transform parent = null)
        {
            ClosestSpawnPointCheck();

            GameObject obj = PhotonNetwork.Instantiate(player.name, m_currentSpawnPoint.location.position, m_currentSpawnPoint.location.rotation, 0);
            obj.transform.parent = parent;
            _GameManager.Instance.gameMode = GameMode.Preparation;
            MultiplayerManager.Instance.isReady = true;
        }

        private void ClosestSpawnPointCheck()
        {
            //float minDistance = Mathf.Infinity;
            if (MultiplayerManager.Instance.LocalPlayer.IsMasterClient)
            {
                m_currentSpawnPoint = playerSpawnPoints[0];
            }
            else
            {

                m_currentSpawnPoint = playerSpawnPoints[1];
            }
            //for (int i = 0; i < playerSpawnPoints.Length; i++)
            //{
            //    if (m_currentSpawnPoint.isOccupied == true || m_currentSpawnPoint == null) continue;
            //    float distance = Vector3.Distance(Camera.main.transform.position, playerSpawnPoints[i].location.position);
            //    if (distance < minDistance)
            //    {
            //        m_currentSpawnPoint = playerSpawnPoints[i];
            //        minDistance = distance;
            //    }
            //}
            m_currentSpawnPoint.isOccupied = true;
        }

        public void SpawnGoal()
        {

        }

        public void SpawnPuck(Transform parent = null)
        {
            m_currentPuck = PhotonNetwork.Instantiate(puck.name, puckSpawnPoint.position, Quaternion.identity, 0);
            m_currentPuck.transform.parent = parent;
            _GameManager.Instance.gameMode = GameMode.Playing;
        }

        public void ResetPuck()
        {
            if (m_currentPuck != null)
            {
                m_currentPuck.transform.position = Vector3.zero;
                m_currentPuck.GetComponent<Rigidbody>().velocity = Vector3.zero;
                m_currentPuck.GetComponent<Puck>().AddRandomForce(Random.Range(0, 2));
            }
        }
        #endregion
    }

}