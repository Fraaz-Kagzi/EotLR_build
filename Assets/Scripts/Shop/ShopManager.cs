using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class ShopManager : MonoBehaviour
{

    public TMP_Text coinsText;
    public ShopItemSO[] shopItemSO;
    public ShopTemplate[] shopPanels;
    public Button[] purchaseButtons;
    public GameObject alertMessage;
    private WeaponManager weaponManagerInstance;

    // Start is called before the first frame update
    void Start()
    {
      weaponManagerInstance = WeaponManager.Instance;
      for (int i = 0; i < shopItemSO.Length; i++) {
          shopPanels[i].gameObject.SetActive(true);
      }
      coinsText.text = "Coins: " + CoinManager.playerCoins.ToString();
      loadPanels();
      checkPurchasable();
    }

    public void ChangeScene() {
      SceneManager.LoadScene("Inventory");
    }

    public void loadPanels(){
      for (int i = 0; i < shopItemSO.Length; i++){
        shopPanels[i].title.text = shopItemSO[i].title;
        shopPanels[i].description.text = shopItemSO[i].description;
        shopPanels[i].priceTxt.text = "Coins: " + shopItemSO[i].price.ToString();
      }
    }

    public void PurchaseItem(int btnNo){

      if (InventoryManager.IncludesItem(shopItemSO[btnNo])){
        // Refund Item
        if(weaponManagerInstance.currentGun == shopItemSO[btnNo].title || weaponManagerInstance.getArmour() == shopItemSO[btnNo].title){
          Debug.Log("You need to unequip the item first");
        }else{
          CoinManager.addCoins(shopItemSO[btnNo].price);
          coinsText.text = "Coins: " + CoinManager.playerCoins.ToString();
          InventoryManager.removeItem(shopItemSO[btnNo]);
        }
        checkPurchasable();
      }else{
        // Purchase Item
        if (CoinManager.playerCoins >= shopItemSO[btnNo].price){
          CoinManager.addCoins(-shopItemSO[btnNo].price);
          coinsText.text = "Coins: " + CoinManager.playerCoins.ToString();
          InventoryManager.addItems(shopItemSO[btnNo]);
          alertMessage.SetActive(false);
          checkPurchasable();
        }else{
          TMP_Text alertMessageTextComponent = alertMessage.GetComponent<TMP_Text>();
          alertMessageTextComponent.text = "Not Enough Coins";
          alertMessage.SetActive(true);
        }
      }

    }

    public void checkPurchasable(){
      for (int i =0; i < shopItemSO.Length; i++){

          if (InventoryManager.IncludesItem(shopItemSO[i])) {
            TMP_Text buttonText = purchaseButtons[i].GetComponentInChildren<TMP_Text>();
            buttonText.text = "Refund " + shopItemSO[i].price;
          }else{
            TMP_Text buttonText = purchaseButtons[i].GetComponentInChildren<TMP_Text>();
            buttonText.text = "Purchase";
          }
  
      }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
