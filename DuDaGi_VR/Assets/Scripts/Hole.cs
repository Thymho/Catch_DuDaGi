using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    Animator ani;
    bool touchPossible = false;
    AudioSource audioSource;
    public AudioClip openSound;
    public AudioClip catchSound;
    public GameManager GM;
    bool boom = false;

    void Start()
    {
        ani = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Open()
    {
        touchPossible = true;
        audioSource.clip = openSound;
        audioSource.Play();

        if (GM.Gs == GameState.Ready)
        {
            GM.GO();
        }
    }

    public void Close()
    {
        touchPossible = false;
    }

    private void OnMouseDown()
    {
        if (touchPossible)
        {
            touchPossible = false;
            ani.SetTrigger("isTouch");
            audioSource.clip = catchSound;
            audioSource.Play();

            if (!boom)
            {
                GM.score += 100;
            } else {
            GM.score -= 100;
        }
    }

    public IEnumerator End()
    {
        float randomTime = Random.Range(1.0f, 3.0f);
        float randomD = Random.Range(1.0f, 10.0f);
        yield return new WaitForSeconds(randomTime);

        if (GM.Gs != GameState.End)
        {
            if (randomD >= 5.0f)
            {
                boom = true;
                ani.SetTrigger("boom");
            }
            else
            {
                boom = false;
                ani.SetTrigger("Open");
            }
        }
    }
}
