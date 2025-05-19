using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    [SerializeField] protected float maxHp = 100f; 
    protected float currentHp;
    [SerializeField] private Image hpBar;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI amoText;

public void IncreaseMaxHP(float extraHP)
{
    maxHp += extraHP;
    currentHp += extraHP;
    UpdateHpBar();
}
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        if (FindObjectsOfType<GameManager>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        currentHp=maxHp;
        UpdateHpBar();
        UpdateAmoText();
    }

    private void Update()
    {
        MovePlayer();
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (gameManager.IsUIActive(gameManager.pauseMenu))
            {
                gameManager.ResumeGame(); 
            }
            else
            {
                gameManager.PauseGameMenu(); 
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (gameManager.IsUIActive(gameManager.Htrang))
            {
                gameManager.ResumeGame(); // tắt hành trang
            }
            else
            {
                gameManager.HanhTrang(); // bật hành trang
            }
        }
        
    }

    private void MovePlayer()
    {
        Vector2 playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.velocity = playerInput.normalized * moveSpeed;

        if (playerInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (playerInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        if (playerInput != Vector2.zero)
        {
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;

        // Đảm bảo máu không xuống dưới 0 để tránh giá trị âm
        currentHp = Mathf.Max(currentHp, 0);

        // Cập nhật thanh máu để phản ánh sự thay đổi HP
        UpdateHpBar();
        UpdateAmoText();
        // Nếu máu giảm xuống 0 hoặc thấp hơn, enemy sẽ chết
        if (currentHp <= 0)
        {
            Die();
        }
    }
    protected virtual void Die()
    {
        gameManager.GameOverMenu();
        
         // Xóa enemy khỏi Scene
         //Destroy(gameObject);  
    }
     public void UpdateHpBar()
    {
        if (hpBar != null)
        {
            // fillAmount là giá trị từ 0 đến 1, phản ánh % máu còn lại
            hpBar.fillAmount = currentHp / maxHp;
        }
    }
    public void Heal(float hpvalue)
    {
        if(currentHp<maxHp)
        {
            currentHp+=hpvalue;
            currentHp=Mathf.Min(currentHp,maxHp);
            UpdateHpBar();
        }
    }
        private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Tự động gán lại GameManager khi sang map mới
        gameManager = FindObjectOfType<GameManager>();
    }
     public void UpdateAmoText()
    {
      if(amoText!=null)
      {
        if(currentHp>=0)
        {
            amoText.text=currentHp.ToString();
        }
        else
        {
            amoText.text="DIE";
        }
      }  
    }
    public void ResetPlayer()
    {
         maxHp=200f;
        currentHp=maxHp;
        UpdateHpBar();
        UpdateAmoText();
    }

}
