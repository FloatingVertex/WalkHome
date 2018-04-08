using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyPlatform : MonoBehaviour
{
    [SerializeField]
    private float launchForce = 10;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.layer == 8) || (collision.gameObject.layer == 9 || collision.gameObject.layer == 10))
        {
            Debug.Log(Vector2.Dot(collision.contacts[0].normal, Vector2.down));
            if (Vector2.Dot(collision.contacts[0].normal, Vector2.down) > 0)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, launchForce));
            }
        }
    }
}
