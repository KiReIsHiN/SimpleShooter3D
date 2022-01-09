using UnityEngine;

namespace PlayerControll
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] Transform _playerCamera = null;
        [SerializeField] float _mouseSensitivity = 3.5f;
        [SerializeField] [Range(0.0f, 0.5f)] float _moveSmoothTime = 0.3f;
        [SerializeField] [Range(0.0f, 0.5f)] float _mouseSmoothTime = 0.03f;

        [SerializeField] float _currentSpeed;
        float _walkSpeed = 6.0f;
        float _runSpeed = 12.0f;

        float _gravity = -13.0f;
        float _velocityY = 0.0f;

        [SerializeField] bool _lockCursor = true;

        float _cameraPitch = 0.0f; //keep track camera current x rotation

        CharacterController _controller = null;

        Vector2 _currentDir = Vector2.zero;
        Vector2 _currentDirVelocity = Vector2.zero;

        Vector2 _currentMouseDelta = Vector2.zero;
        Vector2 _currentMouseDeltaVelocity = Vector2.zero;


        //________________________________________________//

        void Start()
        {
            _controller = GetComponent<CharacterController>();
            LockCursor();
        }

        void Update()
        {
            UpdateMouseLook();
            UpdateMovement();
        }

        //________________________________________________//


        void LockCursor()
        {
            if (_lockCursor)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        void UpdateMouseLook()
        {
            //Get the target rotation of our camera
            Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            //Smoothly rotate our camera
            _currentMouseDelta = Vector2.SmoothDamp(_currentMouseDelta, targetMouseDelta, ref _currentMouseDeltaVelocity, _mouseSmoothTime);

            _cameraPitch -= _currentMouseDelta.y * _mouseSensitivity;//Rotate camera at x axis (look up and down)

            _cameraPitch = Mathf.Clamp(_cameraPitch, -70.0f, 90.0f); //Limit the camera rotation at x axis

            _playerCamera.localEulerAngles = Vector3.right * _cameraPitch;

            transform.Rotate(Vector3.up * _currentMouseDelta.x * _mouseSensitivity);//Rotate camera at y axis (look around)
        }

        void UpdateMovement()
        {
            //Get the target vector where we go
            Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            targetDir.Normalize();

            //make our movement smooth
            _currentDir = Vector2.SmoothDamp(_currentDir, targetDir, ref _currentDirVelocity, _moveSmoothTime);

            Vector3 velocity = (transform.forward * _currentDir.y + transform.right * _currentDir.x) * _currentSpeed + Vector3.up * _velocityY;

            _controller.Move(velocity * Time.deltaTime);

            //Check if grounded - reset value of Y axis to 0
            if (_controller.isGrounded)
                _velocityY = 0.0f;
            _velocityY += _gravity * Time.deltaTime;


            Run();
        }

        void Run()
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _currentSpeed = _runSpeed;
            }
            else
            {
                _currentSpeed = _walkSpeed;
            }
        }
    }//End class
}//End namespace
