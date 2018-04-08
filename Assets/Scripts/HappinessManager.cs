using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappinessManager : MonoBehaviour {

    public static HappinessManager singleton;

    public int happyness = 2;

    private void Awake()
    {
        if(singleton != null)
        {
            Debug.LogError("Multiple happiness managers");
        }
        singleton = this;
    }

    public static void EventHappened(int deltaHappiness,float deltaEnergy)
    {
        singleton.happyness += deltaHappiness;
        EnergyManager.singleton.ChangeEnergy(deltaEnergy);
    }
}
