using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerousPlatform : MonoBehaviour
{

    public enum TrapStates
    {
        Sleeping,
        Peparation,
        Activated,
        Recharge
    };

    private TrapStates currentState;
    private bool startCycle = false;

    [SerializeField] private float rechargeTime;
    [SerializeField] private float peparationTime;
    [SerializeField] private float activatedTime;

    [SerializeField] private GameObject redMaterial;
    [SerializeField] private GameObject orangeMaterial;
    [SerializeField] private GameObject baseMaterial;

    [SerializeField] private int damage = 20;

    private List<GameObject> colliders = new List<GameObject>();

    private float realRechargeTime;
    private float realPeparationTime;
    private float realActivatedTime;

    private void Start()
    {
        realPeparationTime = peparationTime;
        realRechargeTime = rechargeTime;
        currentState = TrapStates.Sleeping;
    }

    private void Update()
    {
        if (startCycle && currentState == TrapStates.Sleeping)
        {
            currentState = TrapStates.Peparation;
            baseMaterial.SetActive(false);
            orangeMaterial.SetActive(true);
            return;
        }

        switch (currentState)
        {
            case TrapStates.Peparation:
                realPeparationTime -= Time.deltaTime;
                if (realPeparationTime <= 0)
                {
                    Peparation();
                }
                break;
            case TrapStates.Activated:
                realActivatedTime -= Time.deltaTime;
                if (realActivatedTime <= 0)
                {
                    Activated();
                }
                break;
            case TrapStates.Recharge:
                realRechargeTime -= Time.deltaTime;
                if (realRechargeTime <= 0)
                {
                    Recharge();
                }
                break;
        }



    }
    private void OnTriggerEnter(Collider collider)
    {


        if (!startCycle)
        {
            startCycle = true;
        }

        colliders.Add(collider.gameObject);
    }

    private void OnTriggerExit(Collider collider)
    {
        colliders.Remove(collider.gameObject);

    }

    private void Peparation()
    {
        realPeparationTime = peparationTime;
        currentState = TrapStates.Activated;
        orangeMaterial.SetActive(false);
        redMaterial.SetActive(true);
    }

    private void Activated()
    {
        realActivatedTime = activatedTime;
        currentState = TrapStates.Recharge;
        Damage();
        redMaterial.SetActive(false);
        baseMaterial.SetActive(true);
    }
    private void Recharge()
    {
        realRechargeTime = rechargeTime;
        currentState = TrapStates.Recharge;
        redMaterial.SetActive(false);
        baseMaterial.SetActive(true);
        currentState = TrapStates.Sleeping;
        startCycle = false;
    }

    private void Damage()
    {
        foreach (var collider in colliders)
        {
            if (collider == null) continue;
            collider.gameObject.GetComponent<HealthSystem>().GetDamage(damage);
        }
    }
}
