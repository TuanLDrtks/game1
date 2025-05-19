using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private Sprite chestClosed;
    [SerializeField] private Sprite chestOpen;
    [SerializeField] private GameObject pressEText; // UI "Nhấn E" hiển thị phía trên rương

    private SpriteRenderer spriteRenderer;
    private bool isOpened = false;
    private bool playerNear = false;
    private GameManager gameManager;

    private void Start()
    {
        // Gán sprite mặc định
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = chestClosed;

        // Tìm GameManager nếu chưa gán
        if (gameManager == null)
        {
            GameObject gmObject = GameObject.FindWithTag("GameManager"); //  tùy tag  dùng
            if (gmObject != null)
                gameManager = gmObject.GetComponent<GameManager>();
        }

        // Ẩn chữ "E" lúc đầu
        if (pressEText != null)
            pressEText.SetActive(false);
    }

    private void Update()
    {
        if (!isOpened && playerNear && Input.GetKeyDown(KeyCode.E))
        {
            OpenChest();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNear = true;

            if (pressEText != null)
            {
                pressEText.SetActive(true);

                // Đặt chữ "E" trên rương
                Vector3 chestPos = transform.position;
                Vector3 offset = new Vector3(0, 1.0f, 0);
                pressEText.transform.position = chestPos + offset;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNear = false;

            if (pressEText != null)
                pressEText.SetActive(false);
        }
    }

    private void OpenChest()
    {
        isOpened = true;
        spriteRenderer.sprite = chestOpen;

        if (pressEText != null)
            pressEText.SetActive(false);

        if (gameManager != null)
            gameManager.Item();
        else
            Debug.LogWarning("GameManager not found!");
    }
}
