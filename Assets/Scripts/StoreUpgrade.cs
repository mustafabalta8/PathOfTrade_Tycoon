using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PathOfTrade
{
public class StoreUpgrade : MonoBehaviour
{
    public static StoreUpgrade instance;

    public float UpgradeCost;
    public string UpgradeName;
    public bool UpgradeUnlocked=false;

    public Store store;
    public Button UpgradeButton;

     

    private void Start()
    {
        if (instance == null)
            instance = this;

        UpdateUI();
    }
    public void UnlockUpgrade()
    {
        if (!UpgradeUnlocked && GameManager.instance.CanBuy(UpgradeCost))
        {
            GameManager.instance.AddToBalance(-UpgradeCost);
            UpgradeUnlocked = true;
            store.BaseStoreProfit = store.BaseStoreProfit * 4.0f;
            // transform.GetComponent<UIStore>().UpgradeUnlocked();

            Button UpgradeButton = transform.Find("UpgradeUnlockButton").GetComponent<Button>();
            TextMeshProUGUI UnlockUpgradeText = UpgradeButton.transform.Find("UnlockText").GetComponent<TextMeshProUGUI>();
            UnlockUpgradeText.text = "Purchased";
        }
    }
    private void OnEnable()
    {
        
        GameManager.OnUpdateBalcance += UpdateUI;
        UIManager.OnUpdateUpgrade += UpdateUI;

    }
    private void OnDisable()
    {
        GameManager.OnUpdateBalcance -= UpdateUI;
        
    }

    public void UpdateUI()
    {
        if (GameManager.instance.CanBuy(UpgradeCost) && !UpgradeUnlocked)
            UpgradeButton.interactable = true;
        else
            UpgradeButton.interactable = false;
    }


}
}

    
