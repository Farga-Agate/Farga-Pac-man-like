using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickableManager : MonoBehaviour
{
    private List<Pickable> _pickableList = new List<Pickable>();
    [SerializeField] private Player player;
    [SerializeField] private ScoreManager scoreManager;
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

        scoreManager.SetMaxScore(_pickableList.Count);
    }

    private void OnPickablePicked(Pickable pickable)
    {
        _pickableList.Remove(pickable);

        if(scoreManager != null)
            scoreManager.AddScore(1);

        pickable.OnPicked -= OnPickablePicked;

        if (pickable.PickableType == PickableType.PowerUp)
        {
            player?.PickPowerUp();
        }

        if (_pickableList.Count <= 0)
        {
            SceneManager.LoadScene("WinScreen");
        }
    }
}
