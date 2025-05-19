using UnityEngine;
// Lớp BasicEnemy kế thừa từ Enemy, đại diện cho một loại quái đơn giản.
public class Boss: Enemy
{
    private static Boss instance;
    [SerializeField] private GameObject bulletboss;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float tocdodan= 5f;
    [SerializeField] private float speedDanVongTron=10f;
    [SerializeField] private float hpvalue=100f;
    [SerializeField] private GameObject miniEnemy;
    [SerializeField] private float skillcooldown=2f;
    [SerializeField] private GameObject usb;
    private float nextskill=0f;
    private void Awake()
{
    if (instance == null)
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    else
    {
        Destroy(gameObject); // Nếu đã có boss rồi thì hủy bản mới
    }
}
    protected override void Update()
    {
        base.Update();
        if(Time.time>=nextskill)
        {
           SuDungSkill();
        }
    }
    protected override void Die()
    {   
        Instantiate(usb,transform.position,Quaternion.identity);
        base.Die();
    }
    // Xử lý khi enemy chạm vào một collider khác có trigger.
    private void OnTriggerEnter2D(Collider2D collision)
    {
            if(collision.CompareTag("Player"))
            {
                if(player!=null)
                {
                    player.TakeDamage(enterDamage);
                }
            }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
            if(collision.CompareTag("Player"))
            {
                if(player!=null)
                {
                    player.TakeDamage(stayDamage);
                }
            }
    }
    private void bandanthg()
    {
        if(player!=null)
        {
            Vector3 directionToPlayer=player.transform.position-firePoint.position;
            directionToPlayer.Normalize();
            GameObject bullet=Instantiate(bulletboss,firePoint.position,Quaternion.identity);
            EnemyBullet enemyBullet = bullet.AddComponent<EnemyBullet>();
            enemyBullet.SetMovementDirection(directionToPlayer * tocdodan);
        }
    }
        private void BanDanVongTron()
    {
        const int bulletCount = 12; // Số lượng đạn bắn ra theo vòng tròn
        float angleStep = 360f / bulletCount; // Góc giữa các viên đạn

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * angleStep; // Tính góc cho từng viên đạn

            // Tính toán hướng di chuyển dựa trên góc
            Vector3 bulletDirection = new Vector3(
                Mathf.Cos(Mathf.Deg2Rad * angle), 
                Mathf.Sin(Mathf.Deg2Rad * angle), 
                0
            );

            // Tạo một viên đạn từ Prefab tại vị trí hiện tại của đối tượng
            GameObject bullet = Instantiate(bulletboss, transform.position, Quaternion.identity);

            // Thêm script điều khiển đạn (EnemyBullet)
            EnemyBullet enemyBullet = bullet.AddComponent<EnemyBullet>();

            // Thiết lập hướng di chuyển cho viên đạn
            enemyBullet.SetMovementDirection(bulletDirection * speedDanVongTron);
        }
    }

    private void HoiMau(float hpAmount)
    {
        // Hồi máu nhưng không vượt quá `maxHp`
        currentHp = Mathf.Min(currentHp + hpAmount, maxHp);

        // Cập nhật thanh máu trên UI
        UpdateHpBar();
    }
    private void SinhMiniEnemy()
    {
        Instantiate(miniEnemy,transform.position,Quaternion.identity);
    }
    private void DichChuyen()
    {
        if(player!=null)
        {
            transform.position=player.transform.position;
        }
    }
    private void ChonSkillNgauNhien()
    {
        int randomSkill = Random.Range(0, 5); // Chọn một kỹ năng ngẫu nhiên từ 0 đến 4

        switch (randomSkill)
        {
            case 0:
                bandanthg(); // Bắn đạn bình thường
                break;
            case 1:
                BanDanVongTron(); // Bắn đạn theo vòng tròn
                break;
            case 2:
                HoiMau(hpvalue); // Hồi máu cho Boss
                break;
            case 3:
                SinhMiniEnemy(); // Sinh ra MiniEnemy
                break;
            case 4:
                DichChuyen(); // Di chuyển sang vị trí khác
                break;
        }
    }

    private void SuDungSkill()
    {
        nextskill = Time.time + skillcooldown; // Thiết lập thời gian hồi chiêu
        ChonSkillNgauNhien(); // Gọi kỹ năng ngẫu nhiên
    }


}
