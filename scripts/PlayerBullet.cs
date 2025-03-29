using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Biến này cho phép bạn tùy chỉnh tốc độ di chuyển của đạn trong Inspector.
    [SerializeField] private float moveSpeed = 25f;
    
    // Biến này cho biết sau bao nhiêu giây thì đạn sẽ tự hủy.
    [SerializeField] private float timeDestroy = 0.5f;
    [SerializeField] public static float damage=10f;
    [SerializeField] GameObject bloodFrefabs;
    private void Start()
    {
        // Destroy(gameObject, timeDestroy) sẽ tự động hủy đối tượng (gameObject) 
        // sau 'timeDestroy' giây kể từ khi bắt đầu (Start).
        Destroy(gameObject, timeDestroy);
    }

    private void Update()
    {

        // transform.Translate(Vector3.right * moveSpeed * Time.deltaTime) 
        // sẽ di chuyển viên đạn về hướng bên phải (Vector3.right),
        // với vận tốc = moveSpeed, có tính đến thời gian giữa các frame (Time.deltaTime)
        // để chuyển động mượt mà và không phụ thuộc vào tốc độ khung hình.
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }
      private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra xem viên đạn có va chạm với Enemy không
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>(); // Lấy component Enemy từ đối tượng va chạm
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // Gây sát thương cho enemy
                GameObject blood= Instantiate(bloodFrefabs,transform.position, Quaternion.identity);
                Destroy(blood,1f);
            }

            Destroy(gameObject); // Hủy đạn ngay sau khi va chạm
        }
    }
}
