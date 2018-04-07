using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandTrap : MonoBehaviour
{
    [SerializeField]
    private float dragFactor = .3f;

    [SerializeField]
    private float massIncrease = 5f;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<Rigidbody2D>().drag = dragFactor;
        other.GetComponent<Rigidbody2D>().mass += massIncrease;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.GetComponent<Rigidbody2D>().drag = 0;
        collision.GetComponent<Rigidbody2D>().mass -= massIncrease;

    }
}
