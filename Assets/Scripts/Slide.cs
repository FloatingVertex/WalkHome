using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Character>())
        {
            collision.GetComponent<Character>().tempMoveSpeedMultiple = 0f;
            collision.GetComponent<Character>().tempDragMultiple = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Character>())
        {
            collision.GetComponent<Character>().tempMoveSpeedMultiple = 1f;
            collision.GetComponent<Character>().tempDragMultiple = 1f;
        }
    }
}
