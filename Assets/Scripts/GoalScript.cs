using UnityEngine;

public enum GoalSide
{
    Left,
    Right
}

public class GoalScript : MonoBehaviour
{
    [SerializeField] private GoalSide goalSide;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Puck"))
            return;

        // Event of goal being scored, which team scored can be determined by goalSide
        switch (goalSide)
        {
            case GoalSide.Left:
                Debug.Log("Goal Scored by Right Team!");
                GameManager.Instance.AddScore(GoalSide.Right);
                break;
            case GoalSide.Right:
                Debug.Log("Goal Scored by Left Team!");
                GameManager.Instance.AddScore(GoalSide.Left);
                break;
        }
        // Access the SpawnManager to spawn a new puck
        SpawnManager spawnManager = FindFirstObjectByType<SpawnManager>();

        if (spawnManager != null)
        {
            spawnManager.SpawnPuck();
        }

        Destroy(collision.gameObject);
    }
}
