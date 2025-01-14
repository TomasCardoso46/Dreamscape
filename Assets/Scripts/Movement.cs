using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;  // Needed to handle quitting in the editor
using UnityEngine.SceneManagement;  // Needed for scene management

public class Movement : MonoBehaviour
{
    [SerializeField] private float _maxForwardSpeed;
    [SerializeField] private float _maxBackwardSpeed;
    [SerializeField] private float _maxStrafeSpeed;
    [SerializeField] private float _maxLookUpAngle;
    [SerializeField] private float _maxLookDownAngle;
    private bool                   _isMoving = false;
    private bool                   _isSoundOn = false;

    private CharacterController _controller;
    private Transform           _head;
    private Vector3             _headRotation;
    private Vector3             _velocity;
    private Vector3             _motion;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _head       = GetComponentInChildren<Camera>().transform;

        HideCursor();
    }

    private void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Time.timeScale == 0)
            return;
        
        UpdateRotation();
        UpdateHead();

        // Check if the 'R' key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Quit();
        }

        // Check if the 'Esc' key is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadScene();
        }
    }

    private void UpdateRotation()
    {
        float rotation = Input.GetAxis("Mouse X");

        transform.Rotate(0f, rotation, 0f);
    }

    private void UpdateHead()
    {
        _headRotation = _head.localEulerAngles;
        
        _headRotation.x -= Input.GetAxis("Mouse Y");

        if (_headRotation.x > 180f)
            _headRotation.x = Mathf.Max(_maxLookUpAngle, _headRotation.x);
        else
            _headRotation.x = Mathf.Min(_maxLookDownAngle, _headRotation.x);

        _head.localEulerAngles = _headRotation;
    }

    private void FixedUpdate()
    {

        if (Time.timeScale == 0)
            return;

        UpdateVelocity();
        UpdatePosition();
    }

    private void UpdateVelocity()
    {
        float forwardAxis   = Input.GetAxis("Forward");
        float strafeAxis    = Input.GetAxis("Strafe");

        if (_isSoundOn && _isMoving)
        {
            AudioManager.Instance.PlaySFX(5);
            _isSoundOn = false;
        }

        if (forwardAxis >= 0f)
        {
            _velocity.z = forwardAxis * _maxForwardSpeed;
            _isMoving = true;
        }
        else
        {
            _velocity.z = forwardAxis * _maxBackwardSpeed;
            _isMoving = true;
        }

        _velocity.x = strafeAxis * _maxStrafeSpeed;

        if (_velocity.magnitude > _maxForwardSpeed)
        {
            _velocity = _velocity.normalized * (forwardAxis > 0 ? _maxForwardSpeed : _maxBackwardSpeed);
            _isMoving = true;

        }

        if (forwardAxis == 0f)
        {
            AudioManager.Instance.StopSFX(5);
            _isSoundOn = true;
            _isMoving = false;
        }
    }

    private void UpdatePosition()
    {
        _motion = transform.TransformVector(_velocity * Time.fixedDeltaTime);

        _controller.Move(_motion);
    }

    public void Quit()
    {
        // Quit the application
        Application.Quit();

#if UNITY_EDITOR
        // If in the editor, stop play mode
        EditorApplication.isPlaying = false;
#endif
    }

    public void ReloadScene()
    {
        // Reload the current scene
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
