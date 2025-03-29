using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies; // Mảng chứa các loại enemy sẽ spawn
    [SerializeField] private Transform[] spawnPoints; // Mảng chứa các vị trí spawn
    [SerializeField] private float timeBetweenSpawns = 2f; // Thời gian giữa các lần spawn

    void Start()
    {
        // Bắt đầu Coroutine để spawn enemy theo thời gian
        StartCoroutine(SpawnEnemyCoroutine());
    }

    private IEnumerator SpawnEnemyCoroutine()
    {
        while (true) // Lặp vô hạn để liên tục spawn enemy
        {
            yield return new WaitForSeconds(timeBetweenSpawns); // Chờ một khoảng thời gian trước khi spawn tiếp

            // Chọn ngẫu nhiên một enemy từ danh sách
            GameObject enemy = enemies[Random.Range(0, enemies.Length)];

            // Chọn ngẫu nhiên một vị trí spawn từ danh sách
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Tạo một enemy tại vị trí spawn
            Instantiate(enemy, spawnPoint.position, Quaternion.identity);
        }
    }
}
