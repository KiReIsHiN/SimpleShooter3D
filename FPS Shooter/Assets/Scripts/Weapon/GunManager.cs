using UnityEngine;

public class GunManager : MonoBehaviour
{
    //урон
    public float _damage;
    //Дальность стрельбы
    public float _range;
    //Создаем луч
    RaycastHit _hit;
    //Сила,с которой отбросит врага
    public float _impactForce = 300.0f;

    //Вспышка выстрела
    [SerializeField] ParticleSystem _muzzleFlash;

    //звуки стрельбы и перезарядки 
    [SerializeField] AudioClip _shot;
    [SerializeField] AudioClip _reload;
    //компонент воспроизведения  
    [SerializeField] AudioSource _audioShot;

    //Всего патронов
    public int allBullets;
    //Патронов в обойме
    public int setNumberOfBullets;
    [HideInInspector] public int _bulletsInWeapon;
    //Время перезарядки
    [SerializeField] float _timeLeft;
    float _reloadTime;


    public bool pistol;
    public bool rifle;
    public bool shootgun;


    //_____________________________________________________________\\

    private void Start()
    {
        _bulletsInWeapon = setNumberOfBullets;
    }

    void Update()
    {
        ReloadWeapon();
    }

    //_____________________________________________________________\\

    public void Shoot()
    {
        //Если есть патроны в обойме
        if (_bulletsInWeapon > 0)
        {

            //Играем вспышку
            _muzzleFlash.Play();
            //Играем выстрел
            _audioShot.PlayOneShot(_shot);        
            //Так как выстрелили, то минус пуля
            _bulletsInWeapon--;

        }//If ammo > 0
    }


    public void RifletShoot()
    {
        float _fireRate = 15.0f;
        float _nextTimeToFire;
        _nextTimeToFire = Time.time + 1f / _fireRate;

        if (Time.time >= _nextTimeToFire)
        {

            //Если есть патроны в обойме
            if (_bulletsInWeapon > 0)
            {
                //Играем вспышку
                _muzzleFlash.Play();
                //Играем выстрел
                _audioShot.PlayOneShot(_shot);
                //Так как выстрелили, то минус пуля
                _bulletsInWeapon--;

            }//If ammo > 0
        }

    }

    public void ShootgunShoot()
    {
        //Если есть патроны в обойме
        if (_bulletsInWeapon > 0)
        {
            //Играем вспышку
            _muzzleFlash.Play();
            //Играем выстрел
            _audioShot.PlayOneShot(_shot);
            //Так как выстрелили, то минус 2 пули
            _bulletsInWeapon -= 2;
        }

    }



    void ReloadWeapon()
    {
        //Если у нас патронов в обойме 0 и есть в запасе патроны
        if (_bulletsInWeapon <= 0 && allBullets > 0)
        {
            //Играем перезарядку
            _audioShot.PlayOneShot(_reload);
            //Отнимаем от счетчика время на перезарядку
            _reloadTime -= Time.deltaTime;
            //Если у нас счетчик 0
            if (_reloadTime < 0)
            {
                //Ставим патронов в обойме сколько запланировано
                _bulletsInWeapon = setNumberOfBullets;
                //Из запаса удаляем те, что перенесли в обойму
                allBullets -= setNumberOfBullets;
                //Ставим обратно счетчик на заданное время
                _reloadTime = _timeLeft;
            }
        }
    }

}