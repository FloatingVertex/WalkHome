using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private float moveFactor = 1;

    [SerializeField]
    private float horizontalDrag = .1f;

    [SerializeField]
    private float jumpForce = .1f;

    [SerializeField]
    private string groundTag = "TileMap";

    private int movingState = 0;
    [HideInInspector]
    public bool jump = false;
    private bool touchingGround;
    private Rigidbody2D rb = null;
	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == groundTag)
        {
            touchingGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == groundTag)
        {
            touchingGround = false;
        }
    }

    private void FixedUpdate()
    {
        float c = -1f / Mathf.Abs(rb.velocity.x);
        float finalVelocityX = -rb.mass / ((-horizontalDrag * Time.fixedDeltaTime) + (rb.mass * c));
        rb.velocity = new Vector2(finalVelocityX * Mathf.Sign(rb.velocity.x), rb.velocity.y);
        rb.AddForce(Vector2.right * moveFactor * movingState);
        if (jump && touchingGround) rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public int Movement
    {
        get { return movingState; }
        set
        {
            movingState = Mathf.Clamp(value, -1, 1);
        }
    }

}