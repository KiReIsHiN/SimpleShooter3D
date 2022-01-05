using UnityEngine;
using UnityEngine.UI;

public class Ammo : MonoBehaviour
{
    //Текст патронов в обойме и всего
    [SerializeField] Text _bulletsText;
    [SerializeField] Text _bulletsAllText;

    GunManager _gm;


    // Start is called before the first frame update
    void Start()
    {
        _gm = GameObject.FindObjectOfType<GunManager>().GetComponent<GunManager>();   
    }

    // Update is called once per frame
    void Update()
    {
        ShowBulletsUI();
    }

    void ShowBulletsUI()
    {
        //Показываем на экране сколько пуль в обойме и в запасе
        _bulletsText.text = _gm._bulletsInWeapon.ToString();
        _bulletsAllText.text = _gm.allBullets.ToString();
    }


}
