using UnityEngine;

public class GunManager : MonoBehaviour
{
    //����
    public float _damage;
    //��������� ��������
    public float _range;
    //������� ���
    RaycastHit _hit;
    //����,� ������� �������� �����
    public float _impactForce = 300.0f;

    //������� ��������
    [SerializeField] ParticleSystem _muzzleFlash;

    //����� �������� � ����������� 
    [SerializeField] AudioClip _shot;
    [SerializeField] AudioClip _reload;
    //��������� ���������������  
    [SerializeField] AudioSource _audioShot;

    //����� ��������
    public int allBullets;
    //�������� � ������
    public int setNumberOfBullets;
    [HideInInspector] public int _bulletsInWeapon;
    //����� �����������
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
        //���� ���� ������� � ������
        if (_bulletsInWeapon > 0)
        {

            //������ �������
            _muzzleFlash.Play();
            //������ �������
            _audioShot.PlayOneShot(_shot);        
            //��� ��� ����������, �� ����� ����
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

            //���� ���� ������� � ������
            if (_bulletsInWeapon > 0)
            {
                //������ �������
                _muzzleFlash.Play();
                //������ �������
                _audioShot.PlayOneShot(_shot);
                //��� ��� ����������, �� ����� ����
                _bulletsInWeapon--;

            }//If ammo > 0
        }

    }

    public void ShootgunShoot()
    {
        //���� ���� ������� � ������
        if (_bulletsInWeapon > 0)
        {
            //������ �������
            _muzzleFlash.Play();
            //������ �������
            _audioShot.PlayOneShot(_shot);
            //��� ��� ����������, �� ����� 2 ����
            _bulletsInWeapon -= 2;
        }

    }



    void ReloadWeapon()
    {
        //���� � ��� �������� � ������ 0 � ���� � ������ �������
        if (_bulletsInWeapon <= 0 && allBullets > 0)
        {
            //������ �����������
            _audioShot.PlayOneShot(_reload);
            //�������� �� �������� ����� �� �����������
            _reloadTime -= Time.deltaTime;
            //���� � ��� ������� 0
            if (_reloadTime < 0)
            {
                //������ �������� � ������ ������� �������������
                _bulletsInWeapon = setNumberOfBullets;
                //�� ������ ������� ��, ��� ��������� � ������
                allBullets -= setNumberOfBullets;
                //������ ������� ������� �� �������� �����
                _reloadTime = _timeLeft;
            }
        }
    }

}