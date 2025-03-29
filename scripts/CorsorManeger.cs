using UnityEngine;

public class CursorManager : MonoBehaviour
{
    // Con trỏ (cursor) mặc định khi không có thao tác chuột.
    [SerializeField] private Texture2D cursorNormal;
    // Con trỏ hiển thị khi nhấn chuột trái (bắn, hoặc hành động "shoot").
    [SerializeField] private Texture2D cursorShoot;
    // Con trỏ hiển thị khi nhấn chuột phải (có thể là hành động "hand" hoặc tương tác khác).
    [SerializeField] private Texture2D cursorHand;

    // Điểm nóng (hotSpot) xác định vị trí thật của con trỏ bên trong hình ảnh. 
    // Ví dụ: (16, 16) là tâm của ảnh con trỏ 32x32.
    private Vector2 hotSpot = new Vector2(16, 16);

    void Start()
    {
        // Đặt con trỏ mặc định khi bắt đầu game.
        Cursor.SetCursor(cursorNormal, hotSpot, CursorMode.Auto);
    }

    void Update()
    {
        // Nếu người chơi nhấn chuột trái xuống (MouseButtonDown(0)), 
        // chuyển con trỏ sang hình "cursorShoot".
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(cursorShoot, hotSpot, CursorMode.Auto);
        }
        // Khi người chơi nhả chuột trái (MouseButtonUp(0)), 
        // chuyển con trỏ trở lại "cursorNormal".
        else if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(cursorNormal, hotSpot, CursorMode.Auto);
        }

        // Nếu người chơi nhấn chuột phải xuống (MouseButtonDown(1)), 
        // chuyển con trỏ sang hình "cursorHand".
        if (Input.GetMouseButtonDown(1))
        {
            Cursor.SetCursor(cursorHand, hotSpot, CursorMode.Auto);
        }
        // Khi người chơi nhả chuột phải (MouseButtonUp(1)), 
        // chuyển con trỏ trở lại "cursorNormal".
        else if (Input.GetMouseButtonUp(1))
        {
            Cursor.SetCursor(cursorNormal, hotSpot, CursorMode.Auto);
        }
    }
}
