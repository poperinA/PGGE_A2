using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, IDamageable
{
    public void TakeDamage()
    {
        Debug.Log("Box: I am hit by a bullet!");
    }
}

