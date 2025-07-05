using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int maxHP = 3;
    [SerializeField] float fallSpeed = 3f;

    int currentHP;
    bool landed;

    void OnEnable()
    {
        currentHP = maxHP;
    }

    void Update()
    {
        if (!landed)
            transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Land on a bridge
        if (!landed && other.CompareTag("Bridge"))
        {
            landed = true;

            // Snap so pirate stands on plank
            float halfHeight = GetComponent<SpriteRenderer>().bounds.extents.y;
            transform.position = new Vector3(
                other.transform.position.x,
                other.transform.position.y + halfHeight,
                0);
        }
    }

    // Damage entry-point called by TapDetector
    public void TakeDamage(int dmg)
    {
        currentHP -= dmg;
        if (currentHP <= 0) Destroy(gameObject);
    }
}
