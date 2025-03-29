using UnityEngine;
using Cinemachine;

public class CameraFollow : MonoBehaviour
{
    
    void Awake()
    {
        if (FindObjectsOfType<GameManager>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
