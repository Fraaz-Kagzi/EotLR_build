using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{

    public static ShopItemSO[] purchasedItems;
    public string equipedWeapon;
    public string armour;
    public InventoryItem[] itemPanels;
    public Button[] equipButtons;
    public bool[] isEquipped;
    public GameObject alertMessage;
    private WeaponManager weaponManagerInstance;

    // Start is called before the first frame update
    void Start(){
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        isEquipped = new bool[equipButtons.Length];
        weaponManagerInstance = WeaponManager.Instance;
        if (purchasedItems != null) {
        for (int i = 0; i < purchasedItems.Length; i++) {
            itemPanels[i].gameObject.SetActive(true);
        }
        showItems();
        displayEquipState();
      }
    }

    public void EquipButton(int btnNum){

      TMP_Text buttonText = equipButtons[btnNum].GetComponentInChildren<TMP_Text>();

      TMP_Text alertMessageTextComponent = alertMessage.GetComponent<TMP_Text>();

      bool hasEmptySlot = false;

      if (isEquipped[btnNum] != false){
        buttonText.text = "Equip";
        
        if (purchasedItems[btnNum].type == "gun"){
          equipedWeapon = null;
        };

        if (purchasedItems[btnNum].type == "armour"){
          armour = null;
        };

        isEquipped[btnNum] = false;

      }else{

        if (purchasedItems[btnNum].type == "gun"){

          if(equipedWeapon == null){
            buttonText.text = "Unequip";
            equipedWeapon = purchasedItems[btnNum].title;
            isEquipped[btnNum] = true; 
          }else{
            alertMessageTextComponent.text = "You cannot equip more than 1 weapons";
            alertMessage.SetActive(true);
          }       
        } 

        if (purchasedItems[btnNum].type == "armour"){
          Debug.Log(armour);
          if(armour == null || armour == ""){
            buttonText.text = "Unequip";
            armour = purchasedItems[btnNum].title;
            isEquipped[btnNum] = true;
          }else{
            alertMessageTextComponent.text = "You can equip only 1 armour suit";
            alertMessage.SetActive(true);
          }
        }
      }
    }

    public void displayEquipState(){
      // for (int i =0; i < itemPanels.Length; i++){
      //   for(int j=0; j < equipedWeapons.Length; j++){
      //     if (equipedWeapons[j] == itemPanels[i].name.text){
      //       TMP_Text buttonText = equipButtons[i].GetComponentInChildren<TMP_Text>();
      //       buttonText.text = "Unequip";
      //       isEquipped[i] = true;
      //     }
      //   }
      // }
      armour = weaponManagerInstance.getArmour();
      for (int i =0; i < itemPanels.Length; i++){
          if (weaponManagerInstance.currentGun == itemPanels[i].name.text || armour == itemPanels[i].name.text){
            TMP_Text buttonText = equipButtons[i].GetComponentInChildren<TMP_Text>();
            buttonText.text = "Unequip";
            isEquipped[i] = true;
            equipedWeapon = weaponManagerInstance.currentGun;
          }
        }
      }

    public void ShopScene() {
      SceneManager.LoadScene("Shop");
    }

    public void GameScene() {

      if(equipedWeapon != null){
        //Load the weapons and the armour
        // GameManager.Save();
        weaponManagerInstance.LoadWeapon(equipedWeapon);
        weaponManagerInstance.ApplyArmour(armour);
      }else{
        alertMessage.SetActive(true);
      }

    }

    public void showItems(){
      for (int i = 0; i < purchasedItems.Length; i++){
        itemPanels[i].name.text = purchasedItems[i].title;
      }
    }

   public static bool IncludesItem(ShopItemSO shopItem){
      if (purchasedItems != null)
      {
          return (Array.IndexOf(purchasedItems, shopItem) != -1);
      }

      return false; 
    }

    public static void addItems(ShopItemSO newItem){
      if (purchasedItems == null)
      {
          purchasedItems = new ShopItemSO[] { newItem };
      }
      else
      {
          ShopItemSO[] newArray = new ShopItemSO[purchasedItems.Length + 1];

          for (int i = 0; i < purchasedItems.Length; i++)
          {
              newArray[i] = purchasedItems[i];
          }

          newArray[newArray.Length - 1] = newItem;
          purchasedItems = newArray;
      }
    }

    public static void removeItem(ShopItemSO item){
      int indexOfItemToRemove = -1;

      for (int i = 0; i < purchasedItems.Length; i++)
      {
          if (purchasedItems[i] == item)
          {
              indexOfItemToRemove = i;
              break;
          }
      }

      if (indexOfItemToRemove != -1)
        {
          ShopItemSO[] newArray = new ShopItemSO[purchasedItems.Length - 1];

          for (int i = 0, j = 0; i < purchasedItems.Length; i++)
          {
              if (i != indexOfItemToRemove)
              {
                  newArray[j] = purchasedItems[i];
                  j++;
              }
          }

          purchasedItems = newArray;
      }
    }

    public static void resetPurchaseItems(){
      purchasedItems = new ShopItemSO[0]; 
    }

}
