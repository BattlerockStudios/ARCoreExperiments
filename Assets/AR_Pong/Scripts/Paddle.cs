using Photon;
using UnityEngine;

namespace Battlerock
{
    public class Paddle : PunBehaviour
    {
        Vector3 tempColor;
        Color syncColor;
        private void Start()
        {
            Physics.IgnoreLayerCollision(9, 9, true);
            DisableLocalPlayerRenderer();
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

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.isWriting)
            {
                //send color
                tempColor = new Vector3(gameObject.GetComponent<Renderer>().material.color.r, gameObject.GetComponent<Renderer>().material.color.g, gameObject.GetComponent<Renderer>().material.color.b);

                stream.Serialize(ref tempColor);
            }
            else
            if (transform.parent.GetComponent<PhotonView>().isMine)
            {
                //get color
                stream.Serialize(ref tempColor);
                var renderers = GetComponentsInChildren<Renderer>();

                syncColor = new Color(tempColor.x, tempColor.y, tempColor.z, 1.0f);
                for (int i = 0; i < renderers.Length; i++)
                {
                    renderers[i].enabled = false;
                }

            }
        }
    }
}