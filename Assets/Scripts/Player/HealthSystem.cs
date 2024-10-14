using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    private int health = 100;

    public static  Action Death;
    public static  Action Damage;

    [SerializeField] TMP_Text hpText;

    public void GetDamage(int damage)
    {
        if (CheckDeath(damage))
        {
            health -= damage;
            UpdateTextHP();
            Damage?.Invoke();
        }
    }

    private bool CheckDeath(int damage)
    {
        if (health - damage > 0)
        {
            return true;
        }
        else
        {
            Death?.Invoke();
            return false;
        }
    }

    public void UpdateTextHP()
    {
        hpText.SetText(health.ToString());
    }
}
