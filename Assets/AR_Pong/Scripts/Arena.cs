using System.Collections;
using UnityEngine;

namespace Battlerock {

    public class Arena : MonoBehaviour
    {
        // Use this for initialization
        private IEnumerator Start()
        {
            while (PhotonNetwork.playerList[0] == null)
            {
                yield return null;
            }

            if (PhotonNetwork.playerList[0] != null)
            {
                var color = MultiplayerManager.Instance.localColor;
                GetComponent<Renderer>().materials[0].color = color;
            }

            while (PhotonNetwork.playerList.Length <= 1)
            {
                yield return null;
            }

            if (PhotonNetwork.playerList[1] != null)
            {
                var color = MultiplayerManager.Instance.otherColor;
                GetComponent<Renderer>().materials[1].color = color;
            }
        }
    }
}