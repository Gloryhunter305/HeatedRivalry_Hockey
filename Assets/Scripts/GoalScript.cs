using UnityEngine;

public enum GoalSide
{
    Left,
    Right
}

public class GoalScript : MonoBehaviour
{
    public funnyFace playerA;
    public funnyFace playerB;

    [SerializeField] private GoalSide goalSide;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Puck"))
            return;


        SpawnManager spawnManager = FindFirstObjectByType<SpawnManager>();
        // Event of goal being scored, which team scored can be determined by goalSide
        switch (goalSide)
        {

            case GoalSide.Left:
                Debug.Log("Goal Scored by Right Team!");
                GameManager.Instance.AddScore(GoalSide.Right);
                if (spawnManager != null)
                {
                    spawnManager.SpawnPuckRight();
                }
                playerA.StartCoroutine(playerA.Angry());
                playerB.StartCoroutine(playerB.Happy());
                break;
            case GoalSide.Right:
                Debug.Log("Goal Scored by Left Team!");
                GameManager.Instance.AddScore(GoalSide.Left);
                if (spawnManager != null)
                {
                    spawnManager.SpawnPuckLeft();
                }
                playerB.StartCoroutine(playerB.Angry());
                playerA.StartCoroutine(playerA.Happy());
                break;
        }
        // Access the SpawnManager to spawn a new puck

        

        Destroy(collision.gameObject);
    }
}
