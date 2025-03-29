using UnityEngine;
using UnityEngine.UI; // Import thư viện để sử dụng Image cho thanh máu

// Lớp Enemy là lớp trừu tượng để các loại enemy khác có thể kế thừa và mở rộng.
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float enemyMoveSpeed = 2f; // Tốc độ di chuyển của enemy
    [SerializeField] private GameObject energyObject;
    protected Player player; // Tham chiếu đến đối tượng Player
    [SerializeField] protected float maxHp = 50f; // Máu tối đa của enemy
    protected float currentHp; // Lượng máu hiện tại của enemy
    [SerializeField] private Image hpBar; // Thanh máu hiển thị trên UI
    [SerializeField] protected float enterDamage = 10f;
    [SerializeField] protected float stayDamage = 1f;
    void Awake()
    {
        if (FindObjectsOfType<GameManager>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        
    }
    
    protected virtual void Start()
    {
        // Tìm Player trong Scene và gán vào biến player
        player = FindAnyObjectByType<Player>();

        // Thiết lập máu ban đầu bằng giá trị maxHp
        currentHp = maxHp;

        // Cập nhật thanh máu khi game bắt đầu
        UpdateHpBar();
    }

    protected virtual void Update()
    {
        MoveToPlayer();
    }

    protected void MoveToPlayer()
    {
        if (player != null)
        {
            // Enemy di chuyển về phía Player với tốc độ enemyMoveSpeed
            transform.position = Vector2.MoveTowards(
                transform.position,
                player.transform.position,
                enemyMoveSpeed * Time.deltaTime
            );

            FlipEnemy();
        }
    }

    protected void FlipEnemy()
    {
        if (player != null)
        {
            // Lật hình enemy để luôn hướng về Player
            transform.localScale = new Vector3(player.transform.position.x < transform.position.x ? -1 : 1, 1, 1);
        }
    }

    // Khi enemy nhận sát thương, phương thức này được gọi
    public virtual void TakeDamage(float damage)
    {
        // Giảm lượng máu theo lượng sát thương nhận vào
        currentHp -= damage;

        // Đảm bảo máu không xuống dưới 0 để tránh giá trị âm
        currentHp = Mathf.Max(currentHp, 0);

        // Cập nhật thanh máu để phản ánh sự thay đổi HP
        UpdateHpBar();

        // Nếu máu giảm xuống 0 hoặc thấp hơn, enemy sẽ chết
        if (currentHp <= 0)
        {
            Die();
        }
    }

    // Phương thức xử lý khi enemy bị tiêu diệt
    protected virtual void Die()
    {
        if(energyObject!=null)
        {
            GameObject energy= Instantiate(energyObject,transform.position, Quaternion.identity);
            
        }
        Destroy(gameObject); // Xóa enemy khỏi Scene
    }

    // Cập nhật thanh máu dựa trên lượng máu hiện tại
    protected void UpdateHpBar()
    {
        if (hpBar != null)
        {
            // fillAmount là giá trị từ 0 đến 1, phản ánh % máu còn lại
            hpBar.fillAmount = currentHp / maxHp;
        }
    }
}
