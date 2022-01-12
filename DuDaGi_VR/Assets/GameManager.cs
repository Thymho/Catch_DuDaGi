using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum GameState
{
    Ready,
    Play,
    End
}

public class GameManager : MonoBehaviour
{
    public GameState Gs;
    public AudioClip readySound;
    public AudioClip goSound;
    public int score;
    public float limitTime;
    public TextMeshPro timeText;
    public TextMeshPro scoreText;
    public GameObject black;
    public TextMeshPro endscore;
    public TextMeshPro highScore;
    public GameObject finalWindow;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = readySound;
        audioSource.Play();
    }

    public void GO()
    {
        Gs = GameState.Play;
        audioSource.clip = goSound;
        audioSource.Play();
    }

    void Update()
    {
        if (Gs == GameState.Play)
        {
            limitTime -= Time.deltaTime;
            if (limitTime <= 0)
            {
                limitTime = 0;
                GameOver();
            }
            timeText.text = limitTime.ToString("N2");
            scoreText.text = score.ToString();
        }
    }

    public void GameOver()
    {
        iTween.FadeTo(black, iTween.Hash("alpha", 180, "delay", 0.1f, "time", 0.5f));
        Gs = GameState.End;

        iTween.MoveTo(finalWindow, iTween.Hash("y", 0, "delay", 0.5f, "time", 0.5f));

        if (score > PlayerPrefs.GetInt("Bs"))
        {
            PlayerPrefs.SetInt("Bs", score);
        }
        endscore.text = score.ToString();
        highScore.text = PlayerPrefs.GetInt("Bs").ToString();
    }
}
