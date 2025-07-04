using UnityEngine;

public class HeroAutoRunner : MonoBehaviour
{
    [SerializeField] float climbSpeed = 2f;   // units per second

    void Update()
    {
        // Move straight upward every frame
        transform.Translate(Vector2.up * climbSpeed * Time.deltaTime);
    }
}
