using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float jumpPower = 5;
    public Rigidbody2D rb;
    public Transform playerFeet;
    public LayerMask groundLayer;
    public HUD hud;
    [HideInInspector]
    public float distance = 0f;

    private float moveSpeed = 2f;  // Does this need to be a float or int?
    private float playerFeetRadius = 0.1f;
    private int score = 0;
    private int bonus = 0;
    private bool isGrounded = true;
    // private bool hasDoubleJumped = false;

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public int GetScore()
    {
        return score;
    }

    // Put moveSpeed in Player Script to have one variable for all scripts.
    public float GetSpeed()
    {
        return moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       // Obstacle obstacle = other.gameObject.GetComponent<Obstacle>();
        
        // Debug.Log(other.gameObject.name);
        
        // Check name of object the player hits to either end the game or get bonus points

        if (other.gameObject.name == "Obstacles(Clone)")
        {
            hud.EndGame(true);
            Time.timeScale = 0;
        }
        else if (other.gameObject.name == "Doctor(Clone)")
        {
            bonus += 1000;
            moveSpeed += 0.5f;
            Debug.Log("1000 points!");
            
        }
    }

    void Update()
    {
        /* Different way of checking if player is grounded WITHOUT using two colliders.
         * Important because using two colliders was causing the player to get double bonus
         * points when they collided with the Doctor object!
         */

        isGrounded = Physics2D.OverlapCircle(playerFeet.position, playerFeetRadius, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded == true)) // || hasDoubleJumped == false))
        {

            rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
           
            /*if (isGrounded == false && hasDoubleJumped == false)
            {
                hasDoubleJumped = true;
            }*/

        }

        // Moved distance variable from FixedUpdate and used deltaTime to make it frame rate independent.
        distance += moveSpeed * Time.deltaTime;
        Debug.Log(distance);

        // Using RoundToInt to convert float distance to an Int and set score divisor from 50 to 10 as distance
        // travelled is slower in seconds than frame rate.
        score = bonus + Mathf.RoundToInt(distance / 10);
    }

    private void FixedUpdate()
    {

    }
}
