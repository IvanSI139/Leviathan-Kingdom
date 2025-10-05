using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateList : MonoBehaviour
{
    public bool jumping;

    public static Vector3 SavedPosition { get; private set; }
    public static Quaternion SavedRotation { get; private set; }
    public static bool HasCheckpoint { get; private set; }
    void Start()
    {

    }

    void Update()
    {
        
    }

    public static void Save(Transform player)
    {
        SavedPosition = player.position;
        SavedRotation = player.rotation;
        HasCheckpoint = true;
        //Debug.Log($"Checkpoint saved at {SavedPosition}");
    }
}
