using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public float moveSpeed = 2.5f;
    public Sprite[] sprites;
    public SpriteRenderer objectRenderer;
    public PolygonCollider2D polygonCollider;

    // Start is called before the first frame update
    void Start()
    {
       objectRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        if (gameObject.name == "Obstacles(Clone)")
        {
            Debug.Log("created polygon");
            polygonCollider = GetComponent<PolygonCollider2D>();
        // sprite = GetComponent<SpriteRenderer>().sprite;

        /* I believe the original code uses two for loops to check for multiple paths in a Polygon Collider
         * and then to run through the array of paths creating the points for each path.
         * Removing for loops as each sprite only has one path.*/

        /*
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
            objectRenderer.sprite.GetPhysicsShape(0, path);
            polygonCollider.SetPath(0, path.ToArray());
        }
        else if(gameObject.name == "Doctor(Clone)")
        {
            var boxCollider = GetComponent<BoxCollider2D>();

            Vector2 newSize = objectRenderer.sprite.bounds.size;
            boxCollider.size = newSize;

            boxCollider.offset = new Vector2(0, 0);
            /* spriteBox.size = new Vector3(backgroundRenderer.sprite.bounds.size.x / transform.lossyScale.x,
                                         backgroundRenderer.sprite.bounds.size.y / transform.lossyScale.y,
                                         backgroundRenderer.sprite.bounds.size.z / transform.lossyScale.z);
            */
        }
    }

    // Update is called once per frame
    void Update()
    {
      transform.position -= transform.right * moveSpeed * Time.deltaTime;
    }
}
