using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public Player player;
    public Sprite[] sprites;
    public SpriteRenderer obstacleRenderer;
    public PolygonCollider2D polygonCollider;

    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
 
        player = GameObject.Find("Player").GetComponent<Player>(); // Find and access Player Script during runtime
        
        obstacleRenderer.sprite = sprites[Random.Range(0, sprites.Length)]; // Random Obstacle sprite

        //if (gameObject.name == "Obstacles(Clone)")
        // {
            Debug.Log("created polygon");
            polygonCollider = GetComponent<PolygonCollider2D>();
        
        /* I believe the original code uses two for loops to check for multiple paths in a Polygon Collider
         * and then to run through the array of paths creating the points for each path. As below, but I'm
         * removing the 'for' loops as each sprite only has one path....
     
        for (int i = 0; i < polygonCollider.pathCount; i++) polygonCollider.SetPath(i, null);
        polygonCollider.pathCount = objectRenderer.sprite.GetPhysicsShapeCount();

        List<Vector2> path = new List<Vector2>();
        for (int i = 0; i < polygonCollider.pathCount; i++)
        {
            path.Clear();
            sprite.GetPhysicsShape(i, path);
            polygonCollider.SetPath(i, path.ToArray());
        } 

        */

            // Clear old path, get sprite Physics Shape, update collider path to match sprite.

            List<Vector2> path = new List<Vector2>();
            path.Clear();
            obstacleRenderer.sprite.GetPhysicsShape(0, path);
            polygonCollider.SetPath(0, path.ToArray());

        moveSpeed = player.GetSpeed();


        // }



        // Old code to change box collider for the Doctor that apparently also adjusts for changes in the
        // scale of the sprite. Scale is (1,1,1) so we don't need it. Code simplified and moved to BackgroundObjects.cs

        /*else if(gameObject.name == "Doctor(Clone)")
        {
            var boxCollider = GetComponent<BoxCollider2D>();

            Vector2 newSize = objectRenderer.sprite.bounds.size;
            boxCollider.size = newSize;

            boxCollider.offset = new Vector2(0, 0);
            // spriteBox.size = new Vector3(backgroundRenderer.sprite.bounds.size.x / transform.lossyScale.x,
            //                              backgroundRenderer.sprite.bounds.size.y / transform.lossyScale.y,
            //                              backgroundRenderer.sprite.bounds.size.z / transform.lossyScale.z);
            
        }*/


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // moveSpeed = player.GetSpeed();
        transform.position -= transform.right * moveSpeed * Time.deltaTime;
    }
}
