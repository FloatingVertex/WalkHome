using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : MonoBehaviour
{
    [SerializeField]
    private GameObject leader = null; // if the AI follows an individual, who does it follow?

    [SerializeField]
    private float safeDistance = 5.0f;

    private Vector2 distance = Vector2.zero;

    private Character charBody = null;

	// Use this for initialization
	void Start ()
    {
        charBody = GetComponent<Character>();
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), leader.GetComponent<BoxCollider2D>());
	}
	
	// Update is called once per frame
	void Update ()
    {
        distance = leader.transform.position - transform.position;
        if (distance.sqrMagnitude > Mathf.Pow(safeDistance, 2))
        {
            CalcSteer();
        }
    }

    // if NPC needs to move towards the player/its target, calculate which direction it should move
    void CalcSteer()
    {
        charBody.Movement = (int)Vector2.Dot(distance, Vector2.right);
    }
}
