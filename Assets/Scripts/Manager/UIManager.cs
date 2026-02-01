using UnityEngine;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        GameManager.Instance.OnScoreChanged += UpdateScoreUI;
        scoreText.text = "0 : 0";
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnScoreChanged -= UpdateScoreUI;
        }
    }

    private void UpdateScoreUI(int leftScore, int rightScore)
    {
        scoreText.text = $"{leftScore} : {rightScore}";
    }
}
