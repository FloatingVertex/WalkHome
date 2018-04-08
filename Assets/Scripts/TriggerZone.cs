using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    [SerializeField]
    private string requiredFlag = "None";

    [SerializeField]
    private string requiredTag = "None";

    [SerializeField]
    private EventTrigger attachedEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (requiredFlag == "None" || (GameplayFlags.GetManager().Contains(requiredFlag) && GameplayFlags.GetManager().GetFlag(requiredFlag)))
        {
            if(requiredTag == "None" || collision.gameObject.tag == requiredTag)
            {
                attachedEvent.RunTrigger();
            }
        }
    }

}
