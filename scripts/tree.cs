using UnityEngine;

public class SortingLayerController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D treeCollider;
    private Player player; // Tìm player tự động
    private BoxCollider2D playerCollider;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        treeCollider = GetComponent<BoxCollider2D>();

        // Tìm Player trong Scene
        player = FindObjectOfType<Player>();

        // Kiểm tra nếu tìm thấy Player, thì lấy BoxCollider2D
        if (player != null)
        {
            playerCollider = player.GetComponent<BoxCollider2D>();
        }
        else
        {
            Debug.LogError("Không tìm thấy Player trong Scene!");
        }
    }

    void Update()
    {
        if (player == null || playerCollider == null) return; // Nếu không có Player, không làm gì cả

        // Lấy vị trí điểm dưới cùng của Player
        float bottomPlayer = player.transform.position.y + playerCollider.offset.y - (playerCollider.size.y / 2);

        // Lấy vị trí điểm dưới cùng của cây (Tree)
        float bottomTree = transform.position.y + treeCollider.offset.y - (treeCollider.size.y / 2);

        // So sánh để thay đổi thứ tự hiển thị (Sorting Order)
        if (bottomPlayer <= bottomTree)
        {
            spriteRenderer.sortingOrder = player.GetComponent<SpriteRenderer>().sortingOrder - 1; // Player che cây
        }
        else
        {
            spriteRenderer.sortingOrder = player.GetComponent<SpriteRenderer>().sortingOrder + 1; // Cây che Player
        }
    }
}
