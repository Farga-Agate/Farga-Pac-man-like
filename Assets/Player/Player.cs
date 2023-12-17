using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform camera;

    [Header("Player Properties")]
    [SerializeField] private float powerUpDuration;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private int health;

    [Header("UI")]
    [SerializeField] private TMP_Text healthText;

    private Rigidbody _rigidbody;
    private Coroutine _powerUpCoroutine;
    private bool _isPowerUpActive;
    public Action OnPowerUpStart;
    public Action OnPowerUpStop;
    void Awake()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
    }

    void Start()
    {
        UpdateUI();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 horizontalDirection = horizontal * camera.right;
        Vector3 verticalDirection = vertical * camera.forward;
        Vector3 movementDirection = horizontalDirection + verticalDirection;

        verticalDirection.y = 0;
        horizontalDirection.y = 0;

        _rigidbody.velocity = movementDirection * speed * Time.fixedDeltaTime;
    }

    public void PickPowerUp()
    {
        if (_powerUpCoroutine != null)
        {
            StopCoroutine(_powerUpCoroutine);
        }

        _powerUpCoroutine = StartCoroutine(StartPowerUp());
    }

    private IEnumerator StartPowerUp()
    {
        _isPowerUpActive = true;
        OnPowerUpStart?.Invoke();
        yield return new WaitForSeconds(powerUpDuration);
        OnPowerUpStop?.Invoke();
        _isPowerUpActive = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_isPowerUpActive)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<Enemy>().Dead();
            }
        }
    }

    private void UpdateUI()
    {
        healthText.text = $"Health: {health}";
    }

    public void Dead()
    {
        health--;
        if (health > 0)
        {
            transform.position = respawnPoint.position;
        }
        else
        {
            health = 0;
            SceneManager.LoadScene("LoseScreen");
        }
        UpdateUI();
    }
}
