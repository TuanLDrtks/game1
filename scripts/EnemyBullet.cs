using UnityEngine;

// Lớp EnemyBullet đại diện cho đạn của Enemy, di chuyển theo một hướng và tự hủy sau 5 giây.
public class EnemyBullet : MonoBehaviour
{
    private Vector3 movementDirection; // Hướng di chuyển của viên đạn

    void Start()
    {
        // Hủy viên đạn sau 5 giây để tránh làm đầy bộ nhớ
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        // Nếu hướng di chuyển là Vector3.zero, không làm gì cả
        if (movementDirection == Vector3.zero) return;

        // Di chuyển viên đạn theo hướng được thiết lập
        transform.position += movementDirection * Time.deltaTime;
    }

    // Hàm thiết lập hướng di chuyển cho viên đạn
    public void SetMovementDirection(Vector3 direction)
    {
        movementDirection = direction;
    }
}
