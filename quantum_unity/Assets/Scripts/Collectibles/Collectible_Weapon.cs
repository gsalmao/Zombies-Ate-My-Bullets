using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Collectible_Weapon : MonoBehaviour, ICollectible
{
    public void Collect()
    {
        Debug.Log("Collect item");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
            Collect();
    }
}
