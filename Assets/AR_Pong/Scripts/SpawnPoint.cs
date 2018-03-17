using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [HideInInspector]
    public Transform location;
    public bool isOccupied = false;

    private void Start()
    {
        location = transform;
    }
}
