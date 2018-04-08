using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    [SerializeField]
    private string requiredFlag = "None";

    [SerializeField]
    private bool requiredFlagState = true;

    [SerializeField]
    private string requiredTag = "None";

    [SerializeField]
    private TriggerType triggerType = TriggerType.None;

    [SerializeField]
    private EventTrigger attachedEvent;

    [SerializeField]
    private EventTrigger[] optionalEvents;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (requiredFlag == "None" || (GameplayFlags.GetManager().Contains(requiredFlag) && GameplayFlags.GetManager().GetFlag(requiredFlag) == requiredFlagState))
        {
            if(requiredTag == "None" || collision.gameObject.tag == requiredTag || CorrectType(collision.gameObject))
            {
                attachedEvent.RunTrigger();
                foreach(var attachedEvent in optionalEvents)
                {
                    attachedEvent.RunTrigger();
                }
                CheckpointManager.GetManager().RegisterObject(gameObject);
                gameObject.SetActive(false);
            }
        }
    }

    private bool CorrectType(GameObject go)
    {
        if(triggerType == TriggerType.None)
        {
            return true;
        }
        if(triggerType == TriggerType.Child && go.GetComponent<AiController>() != null)
        {
            return true;
        }
        if(triggerType == TriggerType.Player && go.GetComponent<PlayerController>() != null)
        {
            return true;
        }
        return false;
    }

}

public enum TriggerType{
    None,
    Player,
    Child
}
