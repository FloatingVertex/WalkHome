using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayFlags : MonoBehaviour
{
    [SerializeField]
    private List<string> flag;
    [SerializeField]
    private List<bool> status;

    private Dictionary<string, bool> flagTracker;
	// Use this for initialization
	void Start ()
    {
        flagTracker = new Dictionary<string, bool>();
        for (int i = 0; i < flag.Count; i++)
        {
            flagTracker.Add(flag[i], status[i]);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    public void ToggleFlag(string flg)
    {
        flagTracker[flg] = !flagTracker[flg];
    }

    public bool GetFlag(string flg)
    {
        return flagTracker[flg];
    }

    public bool Contains(string flg)
    {
        return flagTracker.ContainsKey(flg);
    }

    public static GameplayFlags GetManager()
    {
        return GameObject.Find("EventSystem").GetComponent<GameplayFlags>();
    }
}
