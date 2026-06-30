using UnityEngine;
using TMPro;

public class BowlingGameManager : MonoBehaviour
{
    [Header("References")]
    public BowlingPin[] pins;
    public Rigidbody ballRigidbody;
    public Transform ballStartPoint;
    public TMP_Text scoreText;

    [Header("Timing")]
    public float scoreDelay = 3f;

    private Vector3 ballStartPosition;
    private Quaternion ballStartRotation;
    private bool scoreChecked = false;

    private void Start()
    {
        if (ballRigidbody != null)
        {
            ballStartPosition = ballRigidbody.transform.position;
            ballStartRotation = ballRigidbody.transform.rotation;
        }

        UpdateScoreText(0);
    }

    public void CheckScore()
    {
        int score = 0;

        foreach (BowlingPin pin in pins)
        {
            if (pin != null && pin.IsKnockedDown)
            {
                score++;
            }
        }
	Debug.Log("Score checked: " + score + " / " + pins.Length);

        UpdateScoreText(score);
    }

    public void CheckScoreDelayed()
    {
        if (!scoreChecked)
        {
            scoreChecked = true;
            Invoke(nameof(CheckScore), scoreDelay);
        }
    }

    public void ResetRound()
    {
        scoreChecked = false;

        foreach (BowlingPin pin in pins)
        {
            if (pin != null)
            {
                pin.ResetPin();
            }
        }

        if (ballRigidbody != null)
        {
            if (ballStartPoint != null)
            {
                ballRigidbody.transform.position = ballStartPoint.position;
                ballRigidbody.transform.rotation = ballStartPoint.rotation;
            }
            else
            {
                ballRigidbody.transform.position = ballStartPosition;
                ballRigidbody.transform.rotation = ballStartRotation;
            }

            ballRigidbody.linearVelocity = Vector3.zero;
            ballRigidbody.angularVelocity = Vector3.zero;
            ballRigidbody.Sleep();
        }

        UpdateScoreText(0);
    }

    private void UpdateScoreText(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score + " / " + pins.Length;
        }
    }
}