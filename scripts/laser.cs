using UnityEngine;

public class Laser : MonoBehaviour
{
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
             if (player.transform.position.x < transform.position.x)
            {
                angle += 180f; // Điều chỉnh góc xoay để không bị bắn ngược
            }
            transform.rotation = Quaternion.Euler(0, 0, angle);  
        }
        Destroy(gameObject, 1.5f);
    }

    private void Update()
    {
        // Đạn bay theo hướng player
        
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
            
        }
    }
}
