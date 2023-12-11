using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdJump : MonoBehaviour
{
    Rigidbody2D rigidBody;
    public float jumpPower;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) { 
            rigidBody.velocity = Vector2.up * jumpPower;
            GetComponent<AudioSource>().Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(Score.score > Score.bestScore)
        {
            Score.bestScore = Score.score;
        }
        SceneManager.LoadScene("GameOverScene");

    }
}
