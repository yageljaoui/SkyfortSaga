using UnityEngine;

public class HeroClimber : MonoBehaviour
{
    [SerializeField] float climbSpeed = 2f;     // Units / sec while moving
    bool isMoving = true;                       // True = moving up to next ledge

    void Update()
    {
        if (isMoving)
        {
            transform.Translate(Vector2.up * climbSpeed * Time.deltaTime);
        }
    }

    // Called when Hero’s trigger enters a Bridge collider
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Bridge")) return;

        // Snap Hero exactly onto the bridge’s Y, then pause briefly
        Vector3 pos = transform.position;
        pos.y = other.transform.position.y + 0.16f;   // 0.16 = half of 32-px PPU
        transform.position = pos;

        // Pause, then resume climbing
        isMoving = false;
        Invoke(nameof(ResumeClimb), 0.3f);            // 0.3-sec pause feels good
    }

    void ResumeClimb()
    {
        isMoving = true;
    }
}
