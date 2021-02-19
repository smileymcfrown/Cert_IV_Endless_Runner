using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundObjects : MonoBehaviour
{

    public float moveSpeed = 2.5f;
    public Sprite[] backgroundSprites;
    public SpriteRenderer backgroundRenderer;

    // Start is called before the first frame update
    void Start()
    {
        /* The below code randomly generates a background sprite from the prefab. It then 
         * gets the prefab's BoxCollider2D object and the size of the new sprite ( bounds.size )
         * and resizes the box collider accordingly and centres it ( offset = Vector2(0,0); )
         */

        backgroundRenderer.sprite = backgroundSprites[Random.Range(0, backgroundSprites.Length)];

        var boxCollider = GetComponent<BoxCollider2D>();
        Vector2 newSize = backgroundRenderer.sprite.bounds.size;
        boxCollider.size = newSize;
        boxCollider.offset = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // Move the background objects across the screen - maybe FixedUpdate() should be used?
        transform.position -= transform.right * moveSpeed * Time.deltaTime;
    }
}
