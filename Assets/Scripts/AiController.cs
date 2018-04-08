using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : MonoBehaviour
{
    [SerializeField]
    private GameObject leader = null; // if the AI follows an individual, who does it follow?

    [SerializeField]
    private float safeDistance = 5.0f;

    [SerializeField]
    private string groundTag = "TileMap";

    public int pathIndex = -1;
    public int positionIndex = -1;

    private Vector2 distance = Vector2.zero;

    private Character charBody = null;

	// Use this for initialization
	void Start ()
    {
        charBody = GetComponent<Character>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), leader.GetComponent<Collider2D>());
	}

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == groundTag)
        {
            bool climb_jump = false;
            foreach (var contact in collision.contacts)
            {
                if (Vector2.Dot(contact.normal, Vector2.up) < 0.2f)
                {
                    climb_jump = true;
                }
            }
            GetComponent<Character>().jump = climb_jump;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        distance = leader.transform.position - transform.position;
        CalcSteer();
        //charBody.Movement = -1;
    }

    // if NPC needs to move towards the player/its target, calculate which direction it should move
    void CalcSteer()
    {
        charBody.Movement = Mathf.RoundToInt(Vector2.Dot(distance, Vector2.right)/safeDistance);
    }
}
