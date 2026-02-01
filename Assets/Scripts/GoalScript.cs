using System.Runtime.Serialization.Json;
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

    public AudioSource sound;


    [SerializeField] private GoalSide goalSide;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance.leftTeamScore < 10 && GameManager.Instance.rightTeamScore < 10)
        {
            if (!collision.gameObject.CompareTag("Puck")) return;
            SpawnManager spawnManager = FindObjectOfType<SpawnManager>();

            switch (goalSide)
            {
            case GoalSide.Left:
                Debug.Log("Goal Scored by Right Team!");
                GameManager.Instance.AddScore(GoalSide.Right);

                //PowerUp spawn on left side
                if (GameManager.Instance.rightTeamScore % 3 == 0)
                {
                    if (spawnManager != null)
                    {
                        spawnManager.SpawnPowerUpLeft();
                    }
                }

                //Scoring would spawn puck on right side
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

                //PowerUp spawn on right side
                if (GameManager.Instance.leftTeamScore % 3 == 0)
                {
                    if (spawnManager != null)
                    {
                        spawnManager.SpawnPowerUpRight();
                    }
                }

                //Scoring would spawn puck on right side
                if (spawnManager != null)
                {
                    spawnManager.SpawnPuckLeft();
                }
                playerB.StartCoroutine(playerB.Angry());
                playerA.StartCoroutine(playerA.Happy());
                break;
            }
            // Access the SpawnManager to spawn a new puck
            if (!sound.isPlaying)
                sound.Play();


            Destroy(collision.gameObject);
        }
    }
}
