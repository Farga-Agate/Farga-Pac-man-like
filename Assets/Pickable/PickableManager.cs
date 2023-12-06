using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableManager : MonoBehaviour
{
    private List<Pickable> _pickableList = new List<Pickable>();
    [SerializeField] private Player player;

    private void Start()
    {
        InitPickableList();
    }

    private void InitPickableList()
    {
        Pickable[] pickableObjects = GameObject.FindObjectsOfType<Pickable>();

        for (int i = 0; i < pickableObjects.Length; i++)
        {
            pickableObjects[i].OnPicked += OnPickablePicked;
            _pickableList.Add(pickableObjects[i]);
        }

        Debug.Log("Pickable List: "+_pickableList.Count);
    }

    private void OnPickablePicked(Pickable pickable)
    {
        _pickableList.Remove(pickable);
        pickable.OnPicked -= OnPickablePicked;

        if (pickable.PickableType == PickableType.PowerUp)
        {
            player?.PickPowerUp();
        }

        if (_pickableList.Count <= 0)
        {
            Debug.Log("Win");
        }
    }
}
