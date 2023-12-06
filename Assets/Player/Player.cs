using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform camera;
    [SerializeField] private float powerUpDuration;
    private Rigidbody _rigidbody;
    private Coroutine _powerUpCoroutine;

    public Action OnPowerUpStart;
    public Action OnPowerUpStop;
    void Awake()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
    }

    void Start()
    {
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
        OnPowerUpStart?.Invoke();
        yield return new WaitForSeconds(powerUpDuration);
        OnPowerUpStop?.Invoke();
    }
}
