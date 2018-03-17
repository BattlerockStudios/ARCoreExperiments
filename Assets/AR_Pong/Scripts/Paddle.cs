using Photon;
using UnityEngine;

namespace Battlerock
{
    public class Paddle : PunBehaviour
    {
        private void Start()
        {
            Physics.IgnoreLayerCollision(9, 9, true);
            //DisableLocalPlayerRenderer();            
        }

        private void DisableLocalPlayerRenderer()
        {
            if (transform.parent.GetComponent<PhotonView>().isMine)
            {
                var renderers = GetComponentsInChildren<Renderer>();

                for (int i = 0; i < renderers.Length; i++)
                {
                    renderers[i].enabled = false;
                }

            }
        }     
    }
}