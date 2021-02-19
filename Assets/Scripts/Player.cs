using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float jumpPower = 5;
    public Rigidbody2D rb;
    public Transform playerFeet;
    public float playerFeetRadius;
    public LayerMask groundLayer;
    public HUD hud;
    public int distance = 0;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
       Obstacle obstacle = other.gameObject.GetComponent<Obstacle>();
        
        // Debug.Log(other.gameObject.name);
        
        // Check name of object the player hits to either end the game or get bonus points

        if (other.gameObject.name == "Obstacles(Clone)")
        {
            hud.EndGame(true);
            Time.timeScale = 0;
        }
        else 
        {
            bonus = bonus + 1000;
            Debug.Log("1000 points!");
            // obstacle.moveSpeed = obstacle.moveSpeed + 10;
            
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
    }

    private void FixedUpdate()
    {
        distance++;
        score = bonus + distance / 50;
    }
}
