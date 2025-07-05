using UnityEngine;

public class TapDetector : MonoBehaviour
{
    [SerializeField] int tapDamage = 1;
    Camera cam;

    void Awake() => cam = Camera.main;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            Vector2 screenPos = Input.touchCount > 0
                                ? Input.GetTouch(0).position
                                : (Vector2)Input.mousePosition;

            Vector2 worldPos = cam.ScreenToWorldPoint(screenPos);
            Collider2D hit = Physics2D.OverlapPoint(worldPos);

            if (hit && hit.TryGetComponent(out Enemy enemy))
                enemy.TakeDamage(tapDamage);
        }
    }
}
