using UnityEngine;
using TMPro;
public class Gun : MonoBehaviour
{
    // Giá trị bù (offset) của góc quay. Ở đây là 180 độ để điều chỉnh hướng của súng.
    private float rotateOffset = 180f;

    // Transform xác định vị trí phát đạn (fire position).
    [SerializeField] private Transform firePos;
    // Prefab của viên đạn được khởi tạo khi bắn.
    [SerializeField] private GameObject bulletPrefabs;
    // Khoảng thời gian giữa các lần bắn (delay giữa các shot).
    [SerializeField] private float shotDelay = 0.15f;
    // Thời điểm cho phép bắn tiếp theo, dùng để so sánh với Time.time.
    private float nextShot;

    // Số đạn tối đa có thể có.
    [SerializeField] private int maxAmmo = 24;
    // Số đạn hiện có, công khai để có thể xem hoặc thay đổi từ bên ngoài (ví dụ UI).
    public int currentAmmo;
    [SerializeField] private TextMeshProUGUI amoText;
    [SerializeField] private AudioManager audioManager;
    void Start()
    {
        // Khi khởi chạy, gán số đạn hiện có bằng số đạn tối đa.
        currentAmmo = maxAmmo;
        UpdateAmoText();
    }

    void Update()
    {
        // Gọi hàm xoay súng theo vị trí chuột.
        RotateGun();
        // Gọi hàm bắn đạn khi có tương tác.
        Shoot();
        ReLoad();// ham nap dan.
    }

    // Hàm xoay súng theo vị trí của chuột.
    void RotateGun()
    {
        // Kiểm tra xem vị trí chuột có nằm ngoài màn hình hay không, nếu có thì không thực hiện xoay.
        if (Input.mousePosition.x < 0 || Input.mousePosition.x > Screen.width ||
            Input.mousePosition.y < 0 || Input.mousePosition.y > Screen.height)
        {
            return;
        }

        // Chuyển đổi vị trí chuột từ tọa độ màn hình sang tọa độ thế giới.
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Tính vector khoảng cách từ vị trí súng đến vị trí chuột.
        Vector3 displacement = transform.position - mouseWorldPos;
        // Tính góc quay dựa trên vector displacement. Mathf.Atan2 trả về giá trị tính bằng radian nên nhân với Mathf.Rad2Deg để chuyển về độ.
        float angle = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;
        // Xoay súng bằng cách tạo Quaternion mới với góc đã tính cộng thêm rotateOffset.
        transform.rotation = Quaternion.Euler(0, 0, angle + rotateOffset);

        // Điều chỉnh localScale để đảm bảo súng hiển thị đúng hướng.
        // Nếu góc nhỏ hơn -90 hoặc lớn hơn 90, giữ nguyên scale (không lật).
        if (angle < -90 || angle > 90)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        // Ngược lại, lật trục Y của súng (ví dụ: để hiển thị súng theo hướng bên kia).
        else
        {
            transform.localScale = new Vector3(1, -1, 1);
        }
    }

    // Hàm xử lý bắn đạn.
    void Shoot()
    {
        // Kiểm tra nếu nhấn chuột trái, có đạn và đã đủ thời gian giữa các lần bắn.
        if (Input.GetMouseButtonDown(0) && currentAmmo > 0 && Time.time > nextShot)
        {
            // Cập nhật thời điểm cho phép bắn lần tiếp theo.
            nextShot = Time.time + shotDelay;
            // Tạo (Instantiate) một viên đạn mới tại vị trí firePos với hướng quay firePos.
            Instantiate(bulletPrefabs, firePos.position, firePos.rotation);
            // Giảm số đạn hiện có đi 1.
            currentAmmo--;
            UpdateAmoText();
            audioManager.PlayShootSound();
        }
    }
    // Hàm ReLoad dùng để nạp lại đạn cho súng.
    // Hàm ReLoad dùng để nạp lại đạn cho súng.
    void ReLoad()
    {
    // Kiểm tra nếu người chơi nhấn chuột phải (Input.GetMouseButtonDown(1))
    // và số đạn hiện có (currentAmmo) nhỏ hơn số đạn tối đa (maxAmmo).
        if (Input.GetMouseButtonDown(1) && currentAmmo < maxAmmo)
        {
            // Nếu điều kiện đúng, gán currentAmmo bằng maxAmmo, nghĩa là nạp đầy đạn.
            currentAmmo = maxAmmo;
            UpdateAmoText();
            audioManager.PlayReloadSound();
        }
    }
    private void UpdateAmoText()
    {
      if(amoText!=null)
      {
        if(currentAmmo>0)
        {
            amoText.text=currentAmmo.ToString();
        }
        else
        {
            amoText.text="Empty";
        }
      }  
    }
}
