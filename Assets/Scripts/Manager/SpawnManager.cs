using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject SpawnFieldLeft;
    public GameObject SpawnFieldRight;
    public GameObject puckPrefab;

    [Header("PowerUp Spawning")]
    public GameObject[] powerUpPrefabs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(puckPrefab, new Vector2(0,0), Quaternion.identity);
        Instantiate(puckPrefab, new Vector2(0,3), Quaternion.identity);
        Instantiate(puckPrefab, new Vector2(0,-3), Quaternion.identity);
    }

    //Method to use to spawn a new puck within the spawn field
    public void SpawnPuckLeft()
    {
        Vector3 fieldPosition = SpawnFieldLeft.transform.position;
        Vector3 fieldScale = SpawnFieldLeft.transform.localScale;
        float randomX = Random.Range(fieldPosition.x - fieldScale.x / 2, fieldPosition.x + fieldScale.x / 2);
        float randomY = Random.Range(fieldPosition.y - fieldScale.y / 2, fieldPosition.y + fieldScale.y / 2);
        Vector3 spawnPosition = new Vector3(randomX, randomY, fieldPosition.z);
        Instantiate(puckPrefab, spawnPosition, Quaternion.identity);
    }
    public void SpawnPuckRight()
    {
        Vector3 fieldPosition = SpawnFieldRight.transform.position;
        Vector3 fieldScale = SpawnFieldRight.transform.localScale;
        float randomX = Random.Range(fieldPosition.x - fieldScale.x / 2, fieldPosition.x + fieldScale.x / 2);
        float randomY = Random.Range(fieldPosition.y - fieldScale.y / 2, fieldPosition.y + fieldScale.y / 2);
        Vector3 spawnPosition = new Vector3(randomX, randomY, fieldPosition.z);
        Instantiate(puckPrefab, spawnPosition, Quaternion.identity);
    }

    public void SpawnPowerUpLeft()
    {
        SpawnPowerUpInField(SpawnFieldLeft);
    }

    public void SpawnPowerUpRight()
    {
        SpawnPowerUpInField(SpawnFieldRight);
    }

    private void SpawnPowerUpInField(GameObject spawnField)
    {
        if (spawnField == null) return;

        if (powerUpPrefabs == null || powerUpPrefabs.Length == 0)
        {
            Debug.LogWarning("No powerUpPrefabs assigned on SpawnManager.", this);
            return;
        }

        // pick a random prefab from the provided list (designer should assign 3)
        int index = Random.Range(0, powerUpPrefabs.Length);
        GameObject prefabToSpawn = powerUpPrefabs[index];
        if (prefabToSpawn == null)
        {
            Debug.LogWarning($"powerUpPrefabs[{index}] is null.", this);
            return;
        }

        Vector3 fieldPosition = spawnField.transform.position;
        Vector3 fieldScale = spawnField.transform.localScale;
        float randomX = Random.Range(fieldPosition.x - fieldScale.x / 2, fieldPosition.x + fieldScale.x / 2);
        float randomY = Random.Range(fieldPosition.y - fieldScale.y / 2, fieldPosition.y + fieldScale.y / 2);
        Vector3 spawnPosition = new Vector3(randomX, randomY, fieldPosition.z);

        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (SpawnFieldLeft != null)
            Gizmos.DrawWireCube(SpawnFieldLeft.transform.position, SpawnFieldLeft.transform.localScale);
        if (SpawnFieldRight != null)
            Gizmos.DrawWireCube(SpawnFieldRight.transform.position, SpawnFieldRight.transform.localScale);
    }
}
