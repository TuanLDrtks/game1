using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healCountText; // UI Text để hiển thị số lần hồi máu còn lại
    [SerializeField] private int maxHealTimes = 3;
    private int currentHealTimes = 0;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float hpvalue=100f;
    [SerializeField] private Gun gun; 
    protected Player player;
    void Awake()
    {
        if (FindObjectsOfType<GameManager>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    void Update()
{
    // Kiểm tra nếu nhấn phím Q thì gọi hàm Healplayer()
    if (Input.GetKeyDown(KeyCode.Q))
    {
        Healplayer();
    }
}
    private void UpdateHealCountText()
{
    if (healCountText == null)
    {
        Debug.LogWarning("healCountText chưa được gán!");
        return;
    }

    int healRemaining = maxHealTimes - currentHealTimes;
    healCountText.text = healRemaining.ToString();
}

    
    public void StartGame()
    {
         currentHealTimes = 0;
        UpdateHealCountText();
        gameManager.StartGame();
        player = FindObjectOfType<Player>();
        gun.DamageGun=gun.Damage;
        gun.UpdateAmoText2();
    // Cập nhật camera theo nhân vật mới
            
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void HUONGDAN()
    {
        gameManager.HuongDan();
    }
    public void ContinueGame()
    {
        gameManager.ResumeGame();
        gun.DamageGun=gun.Damage;
    }

    public void MainMenu()
    {
   
    SceneManager.LoadScene("Map1"); // Load về Map1 (scene menu chính)
    gameManager.MainMenu(); // Gọi luôn UI main menu
    }
    public void Item()
{
    if (gun != null)
    {
        gun.DamageGun += 2;
        gun.UpdateAmoText2();
        gameManager.ResumeGame();
    }
    else
    {
        Debug.LogWarning("Chưa gán Gun trong GameUI!");
    }
}
    public void ItemIncreaseHealTimes()
    {
        maxHealTimes++;
        UpdateHealCountText();
        gameManager.ResumeGame();
    }

// Tăng máu tối đa của player thêm 20 (ví dụ)
public void ItemIncreaseMaxHP()
{
    if (player == null)
    {
        Debug.LogWarning("Player chưa được gán! Không thể tăng HP tối đa.");
        return;
    }

    player.IncreaseMaxHP(50f);
    gameManager.ResumeGame();
}
   
        public void Healplayer()
    {
        if (player == null)
        {
            Debug.LogWarning("Player chưa được gán! Không thể hồi máu.");
            return;
        }

        if (currentHealTimes >= maxHealTimes)
        {
            Debug.LogWarning("Đã đạt giới hạn số lần hồi máu.");
            return;
        }

        player.Heal(hpvalue);
        player.UpdateAmoText();

        currentHealTimes++;
        UpdateHealCountText(); // cập nhật lại UI sau mỗi lần hồi máu
    }
}