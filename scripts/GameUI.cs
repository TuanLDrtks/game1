using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
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


    
    public void StartGame()
    {
        gameManager.StartGame();
          
    // Cập nhật camera theo nhân vật mới
            
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ContinueGame()
    {
        gameManager.ResumeGame();
        
    }

    public void MainMenu()
    {
   
    SceneManager.LoadScene("Map1"); // Load về Map1 (scene menu chính)
    gameManager.MainMenu(); // Gọi luôn UI main menu
    }
    public void Item()
    {
        Bullet.damage+=2;
        gameManager.ResumeGame();
    }
}