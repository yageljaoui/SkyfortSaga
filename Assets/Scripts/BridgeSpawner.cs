using UnityEngine;
using System.Collections.Generic;

public class BridgeSpawner : MonoBehaviour
{
    [SerializeField] GameObject bridgePrefab;
    [SerializeField] float verticalSpacing = 2f;          // distance between ledges
    [SerializeField] int initialCount = 10;               // how many to start with
    [SerializeField] float despawnDistance = 10f;         // how far below cam before recycle

    readonly Queue<GameObject> activeBridges = new();     // simple object pool
    float highestY;                                       // y-pos of topmost bridge

    void Start()
    {
        // Spawn initial column centred on X=0
        highestY = -verticalSpacing;                      // so first loop spawns at 0
        for (int i = 0; i < initialCount; i++)
            SpawnBridge();
    }

    void Update()
    {
        Camera cam = Camera.main;
        float camBottom = cam.transform.position.y - cam.orthographicSize;

        // Despawn & recycle bridges that are far below view
        while (activeBridges.Count > 0 &&
               activeBridges.Peek().transform.position.y < camBottom - despawnDistance)
        {
            GameObject old = activeBridges.Dequeue();
            RecycleBridge(old);
        }

        // Ensure we always have bridges ahead of the Hero/camera
        float camTop = cam.transform.position.y + cam.orthographicSize;
        while (highestY < camTop + despawnDistance)
            SpawnBridge();
    }

    void SpawnBridge()
    {
        highestY += verticalSpacing;
        Vector3 pos = new(0, highestY, 0);
        GameObject b = Instantiate(bridgePrefab, pos, Quaternion.identity, transform);
        activeBridges.Enqueue(b);
    }

    void RecycleBridge(GameObject bridge)
    {
        // Move it to the new top position instead of destroying/instantiating
        highestY += verticalSpacing;
        bridge.transform.position = new Vector3(0, highestY, 0);
        activeBridges.Enqueue(bridge);
    }
}
