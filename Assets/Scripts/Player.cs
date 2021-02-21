using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    public Rigidbody2D rb;
    public Transform playerFeet;
    public LayerMask groundLayer;
    public HUD hud;
    [HideInInspector]
    public float distance = 0f;
    public float jumpPower = 5f;
    public float jumpLimit = 2f;
    //public float gravity;


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
            Time.timeScale += 0.5f;
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

            /* Jumping must be adjusted for moveSpeed otherwise the player will jump further and further as the
             * speed increases. We must adjust the jump speed and gravity to keep the same jump height and,
             * more importantly, keep the same horizontal jump distance as the objects in the scene move faster.
             * 
             * This takes more complex maths using gravity, height, distance and speed than I am willing to work
             * out, but someone else has done it for me...
             * 
             * j = s*4h/d  where j = jumpPower (speed); s = moveSpeed; h = jump height; d = horizontal distance
             * 
             * gravity = j²/2h or something.. I got lost here.. but the main take away is that jumpPower and 
             * gravity need two constants to define how they will change as the speed increases:
             * 
             * jConstant = 4 * maxJumpHeight / jumpDistance
             * gConstant = 0.5 / maxJumpHeight
             * 
             * So...
             * 
             * jumpPower = moveSpeed * jConstant
             * gravity = jumpPower * jumpPower * gConstant
             *
             * And after all that.. and playing around with maxHeight and maxDistance values below, it sort of works;
             * however, it turns out it's much easier to just adjust speed with Time.TimeScale instead!
             * 
             * See onTriggerEnter2D function above for that ONE line of code!
             * */

            //jumpPower = moveSpeed * (4 * 10 / 20.4f);
            //gravity = jumpPower * jumpPower * (0.5f / 10);

            //Physics2D.gravity = new Vector2(0, (-9.8f * gravity)); // Original gravity = Vector2(0, -9.8f);

           
            rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse); // The actual jump

            // transform.position.y = Mathf.Clamp(jumpLimit, )

            // This if statement SHOULD keep the jump height low whilst still getting distance
            //if (transform.position.y >= jumpLimit)
            //{
            //    rb.velocity = Vector3.zero;
            //}



            //Double jump code

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
