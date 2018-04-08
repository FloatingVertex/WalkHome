using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static bool inCutScene = false;

    [SerializeField]
    private float moveFactor = 1;

    [SerializeField]
    private float climbFactor = 1;

    [SerializeField]
    private string climbableTag = "ClimbableArea";

    [SerializeField]
    private float horizontalDrag = .1f;

    [SerializeField]
    private float jumpForce = .1f;

    //[SerializeField]
    //private string groundTag = "TileMap";

    private float fatigueLevel = 0;
    [SerializeField]
    private float fatigueTimer;

    public float Fatigue
    {
        get { return fatigueLevel; }
        set { fatigueLevel = value; }
    }

    private int movingState = 0;
    [HideInInspector]
    public bool jump = false;
    private bool touchingGround = false;

    private int climbState = 0;
    public int ClimbState
    {
        get { return climbState; }
        set
        {
            climbState = Mathf.Clamp(value, -1, 1);
        }
    }
    private bool inClimbableArea = false;

    private int lastJump = 0;
    private Rigidbody2D rb = null;

    [HideInInspector]
    public float tempDragMultiple = 1f;
    [HideInInspector]
    public float tempMoveSpeedMultiple = 1f;
    [HideInInspector]
    public float tempJumpForceMultiple = 1f;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnCollisionStay2D(Collision2D collision)
    {
        touchingGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        touchingGround = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == climbableTag)
        {
            inClimbableArea = true;
            rb.gravityScale = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == climbableTag)
        {
            inClimbableArea = false;
            rb.gravityScale = 1f;
        }
    }

    private void FixedUpdate()
    {
        if(inCutScene) // if in a cutscene/dialogue, don't move
        {
            return;
        }
        float c = -1f / Mathf.Abs(rb.velocity.x);
        float finalVelocityX = -rb.mass / ((-horizontalDrag * tempDragMultiple * Time.fixedDeltaTime) + (rb.mass * c));
        rb.velocity = new Vector2(finalVelocityX * Mathf.Sign(rb.velocity.x), rb.velocity.y);
        rb.AddForce(Vector2.right * moveFactor * tempMoveSpeedMultiple * movingState);

        //handle jumping
        if (jump && touchingGround && lastJump > 4)
        {
            rb.AddForce(Vector2.up * jumpForce * tempJumpForceMultiple, ForceMode2D.Impulse);
            lastJump = 0;
        }
        else
        {
            lastJump++;
        }

        //handle climbing
        if(inClimbableArea)
        {
            float cy = -1f / Mathf.Abs(rb.velocity.y);
            float finalVelocityY = -rb.mass / ((-horizontalDrag * tempDragMultiple * Time.fixedDeltaTime) + (rb.mass * cy));
            rb.velocity = new Vector2(rb.velocity.x,finalVelocityY * Mathf.Sign(rb.velocity.y));
            rb.AddForce(Vector2.up * climbFactor * climbState);
        }
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