using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Character))]
public class EnergyManager : MonoBehaviour {

    public static EnergyManager singleton;

    [SerializeField]
    private float energy = 1f;
    [SerializeField]
    private float timeToDepleteEnergy = 300f;
    private float energyDepletionRate;

    public AnimationCurve dragVsEnergy;
    private float initialHorizontalDrag;

    private void Awake()
    {
        if(singleton != null)
        {
            Debug.LogError("Multiple energy managers");
        }
        singleton = this;
    }

    // Use this for initialization
    void Start () {
        initialHorizontalDrag = GetComponent<Character>().horizontalDrag;
        energyDepletionRate = 1f / timeToDepleteEnergy;
    }

    public void ChangeEnergy(float deltaEnergy)
    {
        energy += deltaEnergy;
    }

    private void FixedUpdate()
    {
        energy = energy - energyDepletionRate * Time.fixedDeltaTime;
        GetComponent<Character>().horizontalDrag = initialHorizontalDrag * dragVsEnergy.Evaluate(energy);
    }
}
