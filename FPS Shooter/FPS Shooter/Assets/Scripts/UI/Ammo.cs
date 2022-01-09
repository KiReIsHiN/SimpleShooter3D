using UnityEngine;
using UnityEngine.UI;

public class Ammo : MonoBehaviour
{
    //Текст патронов в обойме и всего
    Text _ammo;
    SC_Weapon playerWeapon;


    // Start is called before the first frame update
    void Start()
    {
        _ammo = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        playerWeapon = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SC_Weapon>();
        _ammo.text = playerWeapon.bulletsPerMagazine.ToString() + "/" + playerWeapon.bulletsPerGun.ToString();
    }



}
