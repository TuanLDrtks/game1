
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playervacham : MonoBehaviour
{
    [SerializeField] private GameManager gameManager; 
    

    [SerializeField] private AudioManager audioManager;
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("enemybullet"))
        {
            Player player=GetComponent<Player>();
            player.TakeDamage(10f);
        }
        else if (collision.CompareTag("Usb"))
        {
            gameManager.WinGame();
        }
        else if(collision.CompareTag("Energy"))
        {
            gameManager.AddEnergy();
            Destroy(collision.gameObject);
            audioManager.PlayEnergySound();
        }
         else if (collision.CompareTag("congdichchuyen"))
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1); 
            
        }
    }
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
