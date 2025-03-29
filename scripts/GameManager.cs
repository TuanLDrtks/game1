using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int currentEnergy;
    [SerializeField] private int energyThreshold=10;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemyspaner;
    private bool bossCalled=false;
    [SerializeField] private Image energyBar;
    [SerializeField] private Image KN;
    [SerializeField] GameObject ThanhBOSS;
    [SerializeField] GameObject gameui;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject item;
     [SerializeField] private AudioManager audioManager;

    [SerializeField] private GameObject winMenu;

    void Awake()
    {
        if (FindObjectsOfType<GameManager>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
      

   
    
    void Start()
    {
        currentEnergy=0;
        UpdateEnergy();
        UpdateKN();
       boss.SetActive(false); 
       MainMenu();
       audioManager.StopAudioGame();
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddEnergy()
    {

         currentEnergy+=1;  
         UpdateEnergy();
         UpdateKN();
         if(currentEnergy==energyThreshold)
         {
            CallBoss();
         }
         if(currentEnergy==4)
         {
            Item();
            currentEnergy=0;
         } 
    }
    private void CallBoss()
    {
        bossCalled=true;
        boss.SetActive(true);
        enemyspaner.SetActive(false);
        
        ThanhBOSS.SetActive(false);
        audioManager.PlayBossAudio();
    }
    private void UpdateKN()
    {
        if(KN!=null)
        {
            float fillAmount=Mathf.Clamp01((float)currentEnergy/(float)4);
        KN.fillAmount=fillAmount;
        }
    }
    private void UpdateEnergy()
    {
        if(energyBar!=null)
        {
            float fillAmount=Mathf.Clamp01((float)currentEnergy/(float)energyThreshold);
        energyBar.fillAmount=fillAmount;
        }
    }
    public void MainMenu(){
        mainMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        item.SetActive(false);
        Time.timeScale=0f;
        audioManager.StopAudioGame();
  
    }
    public void GameOverMenu(){
        gameOverMenu.SetActive(true);
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        Time.timeScale=0f;
        item.SetActive(false);
    }
    public void PauseGameMenu(){
        pauseMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        mainMenu.SetActive(false);
        winMenu.SetActive(false);
        Time.timeScale=0f;
        item.SetActive(false);
    }
    public void StartGame()
    {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        item.SetActive(false);
        Time.timeScale = 1f;
           // 🔁 RESET trạng thái
        currentEnergy = 0;
        bossCalled = false;
        UpdateEnergy();
        UpdateKN();
        
        // Ẩn boss và bật lại enemy spawner nếu cần
        boss.SetActive(false);
        ThanhBOSS.SetActive(true);
        enemyspaner.SetActive(true);
        audioManager.PlayDefaultAudio();
    }
    public void ResumeGame()
    {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        item.SetActive(false);
        Time.timeScale=1f;
    }
    public void WinGame()
    {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
        winMenu.SetActive(true);
        item.SetActive(false);
        Time.timeScale=0f;
    }
     public void Item()
    {
        item.SetActive(true);
        Time.timeScale=0f; 
    }
}