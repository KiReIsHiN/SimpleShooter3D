using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    //Наш игрок
    Transform _player;

    //Указывааем дистанцию, на которой заметят враги
    [SerializeField] float _distanceToSee;
    //Дистанция, при которой начинается преследование
    [SerializeField] float _distanceToFollow;
    //Подсчитаем дистанцию до игрока
    float _playerDistance;

    GunManager _gm;

    [SerializeField] float _walkSpeed;


    //Скорость поворота врага
    float _rotationSpeed = 10.0f;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _gm = GetComponentInChildren<GunManager>();
    }

    private void Update()
    {
        CalculateDistance();
        LookAtPlayer();
        ShootAtPlayer();
    }

    void CalculateDistance()
    {
        _playerDistance = Vector3.Distance(_player.position,transform.position);
    }

    void LookAtPlayer()
    {
        //Увидели игрока
        if(_playerDistance <= _distanceToSee)
        {
            Quaternion _rotation = Quaternion.LookRotation(_player.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, _rotation, _rotationSpeed * Time.deltaTime);
        }

        //Начинаем преследовать
        if(_playerDistance <= _distanceToFollow && _playerDistance > 2f)
        {
            transform.Translate(Vector3.forward * _walkSpeed * Time.deltaTime);
        }
    }

    void ShootAtPlayer()
    {
        if (_playerDistance <= _distanceToSee)
        {
            RaycastHit _hit;
            if (Physics.Raycast(transform.position, transform.forward, out _hit, _distanceToSee))
            {
                //Если у нашего объекта, с которым столкнулись есть компонент здоровья, то отнимаем урон
                DamageHealth _giveDamage = _hit.transform.GetComponentInChildren<DamageHealth>();
                if (_giveDamage != null)
                {
                    _giveDamage.ApplyDamage(_gm._damage);
                }

                if (_gm.pistol)
                    _gm.Shoot();
                if (_gm.rifle)
                    _gm.RifletShoot();
                if (_gm.shootgun)
                    _gm.ShootgunShoot();
            }
        }
    }





}
