using UnityEngine;

// Lớp BasicEnemy kế thừa từ Enemy, đại diện cho một loại quái đơn giản.
public class HPEnemy : Enemy
{
    [SerializeField] private float hpvalue=20f;
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
     protected override void Die()
    {
        hpplayer();
        base.Die();
    }
    private void hpplayer()
    {
        if(player!=null)
        {
            player.Heal(hpvalue);
        }
    }
}
