using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    //Singleton instance
    public static GameManager Instance { get; private set; }

    public int leftTeamScore = 0, rightTeamScore = 0;

    public event Action<int, int> OnScoreChanged;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        leftTeamScore = 0;
        rightTeamScore = 0;
        OnScoreChanged?.Invoke(leftTeamScore, rightTeamScore);
    }

    public void AddScore(GoalSide team)
    {
        if (team == GoalSide.Left)
        {
            leftTeamScore++;
        }
        else if (team == GoalSide.Right)
        {
            rightTeamScore++;
        }

        OnScoreChanged?.Invoke(leftTeamScore, rightTeamScore);
    }
}
