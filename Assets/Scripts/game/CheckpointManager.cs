using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CheckpointManager : MonoBehaviour
{
    private static Transform respawnPoint;
    private static CheckpointManager instance;


    private void Awake()
    {
        // Ensure only one instance of the class exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("dont destroy");
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("destroy");
        }
    }

    public static void SetRespawnPoint(Transform checkpoint)
    {
        respawnPoint = checkpoint;
        Debug.Log("RESPAWN SET");
        Debug.Log(respawnPoint);
    }

    public static Transform GetRespawnPoint()
    {
        return respawnPoint;
    }
}
