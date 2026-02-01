using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{

    public bool gameStart = false;
    public GameObject scoreText;
    public GameObject mainMenu;
    public GameObject MM;
    public GameObject controls;

    public funnyFace playerA;
    public funnyFace playerB;
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

    public void GameStart()
    {
        gameStart = true;
        scoreText.SetActive(true);
        mainMenu.SetActive(false);
        playerA.StartCoroutine(playerA.Kissy());
        playerB.StartCoroutine(playerB.Kissy());
    }
    public void Controls()
    {
        MM.SetActive(false);
        controls.SetActive(true);
    }
    public void Back()
    {
        controls.SetActive(false);
        MM.SetActive(true);

    }
}
