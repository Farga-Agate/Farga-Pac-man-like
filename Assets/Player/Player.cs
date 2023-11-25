using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _camera;
    
    private Rigidbody _rigidbody;
    
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

        Vector3 horizontalDirection = horizontal * _camera.right;
        Vector3 verticalDirection = vertical * _camera.forward;
        Vector3 movementDirection = horizontalDirection + verticalDirection;
        
        verticalDirection.y = 0;
        horizontalDirection.y = 0;
        
        _rigidbody.velocity = movementDirection * _speed * Time.fixedDeltaTime;
    }
}
