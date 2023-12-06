using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    [SerializeField] private PickableType _pickableType;
    public PickableType PickableType => _pickableType;

    public Action<Pickable> OnPicked = null;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPicked?.Invoke(this);
            Debug.Log($"Pickup: {_pickableType}");
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {

    }
}
