using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPositionManager : MonoBehaviour
{
    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player"); // tìm Player tồn tại
        GameObject spawnPoint = GameObject.FindWithTag("Spawn"); // tìm spawn point theo tag

        if(player != null && spawnPoint != null)
        {
            player.transform.position = spawnPoint.transform.position;
        }
    }
}
