using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject[] guns;
    public string currentGun;
    public Armour ArmourScript;

    private static WeaponManager instance;

    // Ensure that only one instance of WeaponManager exists
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadWeapon("");
    }

    public static WeaponManager Instance
    {
        get
        {
            if (instance == null)
            {
                // This can happen if you try to access the instance before it's created.
                // You might want to handle this differently based on your requirements.
                Debug.LogError("WeaponManager instance is null");
            }
            return instance;
        }
    }

    public void LoadWeapon(string weaponName)
    {
        for (int i = 0; i < guns.Length; i++)
        {
            if (guns[i].name == weaponName)
            {
                currentGun = weaponName;
                guns[i].SetActive(true);
            }else{
                guns[i].SetActive(false);
            }
        }
    }

     public void ApplyArmour(string armourType)
    {
        ArmourScript.ApplyArmour(armourType);
    }

    public string getArmour(){
        return ArmourScript.armour;
    }

    public void resetGun(){
        currentGun = null;
        ArmourScript.noArmour();
    }

}
