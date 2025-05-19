using UnityEngine;

public class Boss2 : Enemy
{
    [SerializeField] private GameObject bulletboss;
    [SerializeField] private GameObject bulletboss2;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform firePoint2;
    [SerializeField] private Transform firePoint3;
    [SerializeField] private GameObject usb;
    private Animator animator;
    [SerializeField] private GameObject explosionPrefabs;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float laserSpeed = 20f;
    [SerializeField] private float healAmount = 200f;
   
    [SerializeField] private float skillCooldown = 2f;
    [SerializeField] private GameObject miniEnemy;

    private float nextSkillTime = 0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        if (Time.time >= nextSkillTime)
        {
            UseSmartSkill();
        }
        
    }

    private void Dieanimaton()
{
    animator.SetTrigger("death");
    Instantiate(usb, transform.position + Vector3.up * 1f, Quaternion.identity); // rơi ra USB cao hơn Boss chút
}

    private void UseSmartSkill()
    {
        nextSkillTime = Time.time + skillCooldown;

        int randomSkill = Random.Range(0, 5);

        // Nếu boss còn dưới 50% máu, ưu tiên hồi máu hoặc sinh mini enemy
        if (currentHp <= maxHp / 2)
        {   
            randomSkill = Random.Range(3, 5); // Chỉ chọn shield hoặc sinh mini enemy
        }
        switch (randomSkill)
        {
            case 0:
                ShootBullet();
                break;
            case 1:
                MeleeAttack();
                break;
            case 2:
                GlowBeforeLaser();
                break;
            case 3:
                ShieldHeal();
                break;
            case 4:
                SinhMiniEnemy();
                break;
        }
    }

    private void ShootBullet()
    {   
        if(player != null)
        {
        animator.SetTrigger("atack");
        }
    } private void ShootBullet2()
    {   
        if(player != null)
        {
            Instantiate(bulletboss, firePoint.position, firePoint.rotation);
            
        }
    }

    private void MeleeAttack()
    {
        animator.SetTrigger("atack2");
    }   
      private void MeleeAttack22()
    {
        
        Instantiate(explosionPrefabs,firePoint3.position,firePoint3.rotation, this.transform);
    }

    private void GlowBeforeLaser()
    {    if(player != null)
        {
        animator.SetTrigger("laser");
        }
        
    }private void GlowBeforeLaser2()
    {    if(player != null)
        {
            Instantiate(bulletboss2,firePoint2.position,firePoint2.rotation, this.transform);
        }
        
    }

    private void ShieldHeal()
    {
        animator.SetTrigger("tank");
        currentHp = Mathf.Min(currentHp + healAmount, maxHp);
        UpdateHpBar();   
    }

    private void SinhMiniEnemy()
    {
        animator.SetTrigger("immune");
        
    }
    private void SinhMiniEnemy2()
    {
        
        Instantiate(miniEnemy, transform.position, Quaternion.identity);
        
    }
    public void SetIsRunTrue()
    {
        animator.SetBool("isrun", true);
        
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {    if(player != null)
            {
            player.TakeDamage(enterDamage);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {    if(player != null)
            {   
            player.TakeDamage(stayDamage);
            }
        }
    }
    
protected override void Die()
{
    Dieanimaton(); // Gọi animation chết + rơi USB
    StopAllActions(); // Dừng di chuyển, tấn công...
    Destroy(gameObject, 2f); // Hủy sau vài giây để animation kịp chạy
}
private void StopAllActions()
{
    // 1. Ngừng di chuyển (nếu có)
    if (TryGetComponent<Rigidbody2D>(out var rb))
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true; // Ngừng chịu lực
    }

    // 2. Tắt AI / logic tấn công
    this.enabled = false; // Tắt script Boss2 (nếu toàn bộ AI trong đây)

    // 3. Tắt các trigger hoặc collider va chạm không cần thiết
    Collider2D col = GetComponent<Collider2D>();
    if (col != null) col.enabled = false;
}
}
