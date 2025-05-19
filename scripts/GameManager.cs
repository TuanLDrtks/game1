using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; 

    
    private int currentEnergy;
    [SerializeField] private int energyThreshold = 4;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemySpawner;
    private bool bossCalled = false;
    [SerializeField] private Image energyBar;
    [SerializeField] private Image KN;
    [SerializeField] private GameObject ThanhBOSS;
    [SerializeField] private GameObject gameUI;
    [SerializeField] public GameObject mainMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] public GameObject pauseMenu;
    [SerializeField] public GameObject item;
    [SerializeField] public GameObject Htrang;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private GameObject huongDanPanel;
    private Player playerScript;
    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
     

    void Start()
    {
        
        
        currentEnergy = 0;
        UpdateEnergy();
        UpdateKN();
        boss.SetActive(false);
        MainMenu();
        audioManager.StopAudioGame();
    }
    private void Update()
    {
        
    }
       
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Tìm lại các object trong scene mới
        boss = GameObject.FindWithTag("Enemy");
        enemySpawner = GameObject.FindWithTag("EnemySpawner");
        

        player = GameObject.FindWithTag("Player");
        if (player != null)
            playerScript = player.GetComponent<Player>();
         // Reset trạng thái boss
    bossCalled = false;

    if ( scene.name == "Map1"&&boss != null)
    {
        boss.SetActive(false); // Ẩn boss khi mới load
    }

    if (enemySpawner != null)
    {
        enemySpawner.SetActive(true);
    }

    if (ThanhBOSS != null)
    {
        ThanhBOSS.SetActive(true);
    }

    // Reset năng lượng
    currentEnergy = 0;
    UpdateEnergy();
    UpdateKN();
       
    }
    public bool IsUIActive(GameObject ui)
    {
        return ui != null && ui.activeSelf;
    }

    public void AddEnergy()
    {
        currentEnergy++;
        UpdateEnergy();
        UpdateKN();

        if (currentEnergy >= energyThreshold)
        {
            CallBoss();
        }
        if (currentEnergy == 4 ) // Mỗi lần đạt bội số của 4 thì hiện Item
        {
            Item();
            currentEnergy = 0;
        }
    }

    private void CallBoss()
    {
        // Đảm bảo boss không bị gọi nhiều lần
        if(bossCalled)return ;
        bossCalled = true;
        if (boss != null) boss.SetActive(true);
    if (enemySpawner != null) enemySpawner.SetActive(false);
    if (ThanhBOSS != null) ThanhBOSS.SetActive(false);
    if (audioManager != null) audioManager.PlayBossAudio();
    }

    private void UpdateKN()
    {
        if (KN != null)
        {
            KN.fillAmount = Mathf.Clamp01((float)currentEnergy / 4);
        }
    }

    private void UpdateEnergy()
    {
        if (energyBar != null)
        {
            energyBar.fillAmount = Mathf.Clamp01((float)currentEnergy / energyThreshold);
        }
    }

    public void MainMenu()
    {
        ShowOnly(mainMenu);
        Time.timeScale = 0f;
        audioManager.StopAudioGame();
    }

    public void GameOverMenu()
    {
        ShowOnly(gameOverMenu);
        Time.timeScale = 0f;
    }

    public void PauseGameMenu()
    {
        ShowOnly(pauseMenu);
        Time.timeScale = 0f;
    }

    public void StartGame()
{
    ShowOnly(null);
    Time.timeScale = 1f;

    currentEnergy = 0;
    bossCalled = false;
    UpdateEnergy();
    UpdateKN();

    if (playerScript != null)
    {
        playerScript.ResetPlayer();
    }

    if (boss != null) boss.SetActive(false);
    if (enemySpawner != null) enemySpawner.SetActive(true);
    if (ThanhBOSS != null) ThanhBOSS.SetActive(true);
    
    if (audioManager != null) audioManager.PlayDefaultAudio();
}

   
      

    public void ResumeGame()
    {
        ShowOnly(null);
        Time.timeScale = 1f;
    }

    public void WinGame()
    {
        ShowOnly(winMenu);
        Time.timeScale = 0f;
    }

    public void Item()
    {
        ShowOnly(item);
        Time.timeScale = 0f;
    }

    public void HanhTrang()
    {
        ShowOnly(Htrang);
        Time.timeScale = 0f;
      
    }
    public void HuongDan()
{
    ShowOnly(huongDanPanel);
    Time.timeScale = 0f;
}


    // ✅ Hàm dùng để chỉ hiển thị một menu duy nhất
   private void ShowOnly(GameObject menuToShow)
{
    mainMenu.SetActive(menuToShow == mainMenu);
    gameOverMenu.SetActive(menuToShow == gameOverMenu);
    pauseMenu.SetActive(menuToShow == pauseMenu);
    winMenu.SetActive(menuToShow == winMenu);
    item.SetActive(menuToShow == item);
    Htrang.SetActive(menuToShow == Htrang);
    huongDanPanel.SetActive(menuToShow == huongDanPanel);
}
} 