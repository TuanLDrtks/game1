using UnityEngine;

public class BulletBoss2 : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 25f;
    [SerializeField] private float timeDestroy = 0.5f;
    [SerializeField] private float damage = 10f;
    [SerializeField] GameObject bloodFrefabs;

    private Vector3 moveDirection;

    private void Start()
    {
        // Tìm player trong scene
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // Tính hướng từ đạn đến player
            Vector3 dir = player.transform.position - transform.position;
            moveDirection = dir.normalized;

            // Xoay viên đạn hướng về phía player
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        Destroy(gameObject, timeDestroy);
    }

    private void Update()
    {
        // Đạn bay theo hướng player
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player enemy = collision.GetComponent<Player>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                GameObject blood = Instantiate(bloodFrefabs, transform.position, Quaternion.identity);
                Destroy(blood, 1f);
            }
            Destroy(gameObject);
        }
    }
}
