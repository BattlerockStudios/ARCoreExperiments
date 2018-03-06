using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColorPingPong : MonoBehaviour
{

    public Color colorStart = Color.white;
    public Color colorEnd = Color.red;
    public float duration = 1.0F;

    public bool useCurrentColorAsStartingColor = false;

    private void Start()
    {
        if (useCurrentColorAsStartingColor)
        {
            colorStart = this.GetComponent<Renderer>().material.color;
        }
    }

    void LateUpdate()
    {
        if (GetComponent<PhotonView>().isMine)
        {
            float lerp = Mathf.PingPong(Time.time, duration) / duration;
            if (this.GetComponent<Renderer>())
            {
                this.GetComponent<Renderer>().material.color = Color.Lerp(colorStart, colorEnd, lerp);
            }
            else if (this.GetComponent<Image>())
            {
                this.GetComponent<Image>().color = Color.Lerp(colorStart, colorEnd, lerp);
            }
            else if (this.GetComponent<Text>())
            {
                this.GetComponent<Text>().color = Color.Lerp(colorStart, colorEnd, lerp);
            }
        }
    }
}
