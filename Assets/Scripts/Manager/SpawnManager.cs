using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject SpawnField;
    public GameObject puckPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnPuck();
    }

    //Method to use to spawn a new puck within the spawn field
    public void SpawnPuck()
    {
        Vector3 fieldPosition = SpawnField.transform.position;
        Vector3 fieldScale = SpawnField.transform.localScale;
        float randomX = Random.Range(fieldPosition.x - fieldScale.x / 2, fieldPosition.x + fieldScale.x / 2);
        float randomY = Random.Range(fieldPosition.y - fieldScale.y / 2, fieldPosition.y + fieldScale.y / 2);
        Vector3 spawnPosition = new Vector3(randomX, randomY, fieldPosition.z);
        Instantiate(puckPrefab, spawnPosition, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(SpawnField.transform.position, SpawnField.transform.localScale);
    }
}
