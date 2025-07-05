using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject piratePrefab;
    [SerializeField] float spawnInterval = 3f;
    [SerializeField] float xRange = 0.5f;          // horizontal randomness

    float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0;
            SpawnPirate();
        }
    }

    void SpawnPirate()
    {
        // Spawn just above top of camera view
        Camera cam = Camera.main;
        float topY = cam.transform.position.y + cam.orthographicSize + 1f;
        float x = Random.Range(-xRange, xRange);
        Vector3 pos = new(x, topY, 0);
        Instantiate(piratePrefab, pos, Quaternion.identity);
    }
}
