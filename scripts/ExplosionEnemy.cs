using UnityEngine;

// Lớp BasicEnemy kế thừa từ Enemy, đại diện cho một loại quái đơn giản.
public class ExplosionEnemy : Enemy
{
    [SerializeField] private GameObject explosionPrefabs;
    
    private void createExplosion()
    {
        if(explosionPrefabs!=null)
        {
            Instantiate(explosionPrefabs,transform.position,Quaternion.identity);
        }
    }
  
     protected override void Die()
    { 
        createExplosion();
       
        base.Die();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player"))
        {
            createExplosion();
        }
    }
    
}
