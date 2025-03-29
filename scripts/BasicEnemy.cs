using UnityEngine;

// Lớp BasicEnemy kế thừa từ Enemy, đại diện cho một loại quái đơn giản.
public class BasicEnemy : Enemy
{
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
}
