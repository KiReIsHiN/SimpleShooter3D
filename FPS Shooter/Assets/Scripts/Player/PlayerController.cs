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

        GunManager _gm;


        //________________________________________________//

        void Start()
        {
            _controller = GetComponent<CharacterController>();
            LockCursor();
            _gm = GetComponentInChildren<GunManager>();
        }

        void Update()
        {
            UpdateMouseLook();
            UpdateMovement();
            Shoot();
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

        void Shoot()
        {
            if (_gm.pistol && Input.GetButtonDown("Fire1"))
            {
                RaycastHit _hit;
                if (Physics.Raycast(_playerCamera.transform.position, _playerCamera.transform.forward, out _hit, _gm._range))
                {

                    //Если у нашего объекта, с которым столкнулись есть компонент здоровья, то отнимаем урон
                    DamageHealth _giveDamage = _hit.transform.GetComponent<DamageHealth>();
                    if (_giveDamage != null)
                    {
                        _giveDamage.ApplyDamage(_gm._damage);
                        Debug.Log(_giveDamage.healthCount);
                    }

                    //Если у врага есть риджидбоди
                    Rigidbody _rb = _hit.transform.GetComponent<Rigidbody>();
                    if (_rb != null && _giveDamage.healthCount <= 0)
                    {
                        //откидываем его перед смертью
                        _rb.AddForce(-_hit.normal * _gm._impactForce);
                    }

                }

                _gm.Shoot();
            }

            if (_gm.rifle && Input.GetButton("Fire1"))
            {
                RaycastHit _hit;
                if (Physics.Raycast(_playerCamera.transform.position, _playerCamera.transform.forward, out _hit, _gm._range))
                {

                    //Если у нашего объекта, с которым столкнулись есть компонент здоровья, то отнимаем урон
                    DamageHealth _giveDamage = _hit.transform.GetComponent<DamageHealth>();
                    if (_giveDamage != null)
                    {
                        _giveDamage.ApplyDamage(_gm._damage);
                        Debug.Log(_giveDamage.healthCount);
                    }

                    //Если у врага есть риджидбоди
                    Rigidbody _rb = _hit.transform.GetComponent<Rigidbody>();
                    if (_rb != null && _giveDamage.healthCount <= 0)
                    {
                        //откидываем его перед смертью
                        _rb.AddForce(-_hit.normal * _gm._impactForce);
                    }

                }

                _gm.RifletShoot();
            }

            if (_gm.shootgun && Input.GetButton("Fire1"))
            {
                Vector3 dir = transform.forward + new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));

                RaycastHit _hit;
                if (Physics.Raycast(_playerCamera.transform.position, dir, out _hit, _gm._range))
                {

                    //Если у нашего объекта, с которым столкнулись есть компонент здоровья, то отнимаем урон
                    DamageHealth _giveDamage = _hit.transform.GetComponent<DamageHealth>();
                    if (_giveDamage != null)
                    {
                        _giveDamage.ApplyDamage(_gm._damage);
                        Debug.Log(_giveDamage.healthCount);
                    }

                    //Если у врага есть риджидбоди
                    Rigidbody _rb = _hit.transform.GetComponent<Rigidbody>();
                    if (_rb != null && _giveDamage.healthCount <= 0)
                    {
                        //откидываем его перед смертью
                        _rb.AddForce(-_hit.normal * _gm._impactForce);
                    }

                }

                _gm.ShootgunShoot();
            }

           

        }

    }//End class
}//End namespace
