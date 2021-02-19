using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundObjects : MonoBehaviour
{

    public Player player;
    public Sprite[] backgroundSprites;
    public SpriteRenderer backgroundRenderer;

    private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        // This line is required to access the Player Class functions because this script is attached to
        // a prefab that is not instantiated until runtime. It's used to the the moveSpeed variable from Player
        player = GameObject.Find("Player").GetComponent<Player>();
        

        /* The below code randomly generates a background sprite from the prefab. It then 
         * gets the prefab's BoxCollider2D object and the size of the new sprite ( bounds.size )
         * and resizes the box collider accordingly and centres it ( offset = Vector2(0,0); )
         */

        backgroundRenderer.sprite = backgroundSprites[Random.Range(0, backgroundSprites.Length)]; //Random Background Sprite

        var boxCollider = GetComponent<BoxCollider2D>(); // Assign the object's Box Collider to a variable

        Vector2 newSize = backgroundRenderer.sprite.bounds.size; // Get the size of the sprite and put it in a variable
        boxCollider.size = newSize; // Make the box collider size = the sprite size
        boxCollider.offset = new Vector2(0, 0); // Make sure the box collider is centred by resetting the offset to 0,0
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Update moveSpeed otherwise obstacles spawned just after leveling up will move faster than
        // the obstacles still on the screen from the previous level.
        moveSpeed = player.GetSpeed();

        // Move the background objects across the screen using deltaTime to make it move in x per second not x per frame rate
        transform.position -= transform.right * moveSpeed * Time.deltaTime;
    }
}
