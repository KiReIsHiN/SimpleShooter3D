using UnityEngine;

namespace PlayerControll
{
    public class ChengeWeapon : MonoBehaviour
    {
        [SerializeField] GameObject _pistol;
        [SerializeField] GameObject _rifle;
        [SerializeField] GameObject _shootgun;

        void Update()
        {
            if (Input.GetKey(KeyCode.Alpha1))
            {
                _pistol.SetActive(true);
                _rifle.SetActive(false);
                _shootgun.SetActive(false);
            }

            if (Input.GetKey(KeyCode.Alpha2))
            {
                _pistol.SetActive(false);
                _rifle.SetActive(true);
                _shootgun.SetActive(false);
            }

            if (Input.GetKey(KeyCode.Alpha3))
            {
                _pistol.SetActive(false);
                _rifle.SetActive(false);
                _shootgun.SetActive(true);
            }
        }
    }
}
