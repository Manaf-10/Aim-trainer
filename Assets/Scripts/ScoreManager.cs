using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public GameObject target;
    
    private int currentScore = 0;
    private int highScore = 0;

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateUI();
    }

    public void AddScore(int points)
    {
        currentScore += points;
        
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        UpdateUI();
    }

public void ResetGame()
{
    currentScore = 0;
    
    target.transform.position = new Vector3(26.36716f, 2.10815f, 3.78527f);
    
    Rigidbody rb = target.GetComponent<Rigidbody>();
    if (rb != null)
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero; 
    }

    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
    UpdateUI();
}

    void UpdateUI()
    {
        scoreText.text = "Score: " + currentScore;
        highScoreText.text = "Best: " + highScore;
    }
}