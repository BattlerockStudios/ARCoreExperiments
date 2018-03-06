using System.Collections;
using UnityEngine;

/// <summary>
/// This class should create a lightning effect between two points
/// </summary>
public class LightningBolt : MonoBehaviour {

    public Transform startPos;
    public Transform endPos;

    private LineRenderer lRend;

    private Vector3[] points; 
    private readonly int point_Begin = 0;
    private int point_End;

    //public Transform line;
    private float randomPosOffset;
    private float randomWithOffsetMax;
    private float randomWithOffsetMin;

    private readonly WaitForSeconds customFrame = new WaitForSeconds(0.05f);

    void Start()
    {
        lRend = GetComponent<LineRenderer>();

        //set the offset width based on the original width multiplyer
        randomPosOffset = lRend.widthMultiplier;
        randomWithOffsetMax = lRend.widthMultiplier * 2;
        randomWithOffsetMin = lRend.widthMultiplier / 10;

        //Get the number of points
        points = new Vector3[lRend.positionCount];
        point_End = lRend.positionCount -1;

        StartCoroutine(Beam());
    }

    private IEnumerator Beam()
    {
        yield return customFrame;

        points[point_Begin] = startPos.localPosition;
        points[point_End] = endPos.localPosition;
        CalculateMiddle();
        lRend.SetPositions(points);
        lRend.startWidth = RandomWidthOffset();
        lRend.endWidth = RandomWidthOffset();
        StartCoroutine(Beam());
    }

    private float RandomWidthOffset()
    {
        return Random.Range(randomWithOffsetMin, randomWithOffsetMax);
    }

    /// <summary>
    /// Go through each of the points between the start and end and set their random position
    /// </summary>
    private void CalculateMiddle()
    {
        for(int i = 1; i < point_End; i++)
        {
            float percent = (float) i / (float) point_End;
            points[i] = GetMiddleWithRandomness(startPos.localPosition, endPos.localPosition, percent);
        }
        
    }

    /// <summary>
    /// Get a vector of a point between two points
    /// </summary>
    /// <param name="point1"></param> start point
    /// <param name="point2"></param> end point
    /// <param name="percent"></param> percentage between the start and end
    /// <returns></returns>
    private Vector3 GetMiddleWithRandomness(Vector3 point1, Vector3 point2, float percent)
    {
        Vector3 midPoint = Vector3.Lerp(point1, point2, percent);
        float fX = Random.Range(midPoint.x - randomPosOffset, midPoint.x + randomPosOffset);
        float fY = Random.Range(midPoint.y - randomPosOffset, midPoint.y + randomPosOffset);
        float fZ = Random.Range(midPoint.z - randomPosOffset, midPoint.z + randomPosOffset);

        return new Vector3(fX, fY, fZ);
    }
}
