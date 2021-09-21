using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    // Text support
    [SerializeField]
    Text scoreText;
    public int score = 0;
    const string ScorePrefix = "Score: ";

    // Timer support
    float elapsedSeconds = 0;
    bool running = true;

    // Start is called before the first frame update
    void Start()
    {
        // Set the text property of score text to display our score 
        scoreText.text = ScorePrefix + score.ToString(); 
    }

    // Update is called once per frame
    void Update()
    {
        // Only update score if timer is running
        if(running == true)
        {
            // Add points according to time
            elapsedSeconds += Time.deltaTime;
            score = (int)elapsedSeconds;

            // Update display text
            scoreText.text = ScorePrefix + score.ToString();
        }
    }

    // Stops game timer when called
    public void StopGameTimer()
    {
        running = false;
    }
}
