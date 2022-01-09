using UnityEngine;

public class SC_WeaponManager : MonoBehaviour
{
    public Camera playerCamera;
    public SC_Weapon primaryWeapon;
    public SC_Weapon secondaryWeapon;
    public SC_Weapon thirdWeapon;

    [HideInInspector]
    public SC_Weapon selectedWeapon;

    // Start is called before the first frame update
    void Start()
    {
        //At the start we enable the primary weapon and disable the secondary
        primaryWeapon.ActivateWeapon(true);
        secondaryWeapon.ActivateWeapon(false);
        thirdWeapon.ActivateWeapon(false);
        selectedWeapon = primaryWeapon;
        primaryWeapon.manager = this;
        secondaryWeapon.manager = this;
    }

    // Update is called once per frame
    void Update()
    {
        //Select secondary weapon when pressing 1
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            primaryWeapon.ActivateWeapon(false);
            secondaryWeapon.ActivateWeapon(true);
            thirdWeapon.ActivateWeapon(false);
            selectedWeapon = secondaryWeapon;
        }

        //Select primary weapon when pressing 2
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            primaryWeapon.ActivateWeapon(true);
            secondaryWeapon.ActivateWeapon(false);
            thirdWeapon.ActivateWeapon(false);
            selectedWeapon = primaryWeapon;
        }
        //Select primary weapon when pressing 3
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            primaryWeapon.ActivateWeapon(false);
            secondaryWeapon.ActivateWeapon(false);
            thirdWeapon.ActivateWeapon(true);
            selectedWeapon = thirdWeapon;
        }

    }
}