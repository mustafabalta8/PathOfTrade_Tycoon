using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

namespace PathOfTrade
{
public class UIStore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI StoreCountText;
    [SerializeField] TextMeshProUGUI StoreCostText;
    [SerializeField] Slider ProgressSlider;
    [SerializeField] Button BuyButton;
    [SerializeField] TextMeshProUGUI StoreProfitText;

    public Store store;
    public StoreUpgrade storeUpgrade;

    public Button ManagerButton;

    public Image UnlockAchievement;
   // public Button UpgradeButton;
    private void Awake()
    {
        store = transform.GetComponent<Store>();
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();

            StoreCostText.text = "Buy $" + String.Format("{0:0.0}", store.NextStoreCost);
            StoreCountText.text = store.StoreCount.ToString();
        }

    // Update is called once per frame
    void Update()
    {
        ProgressSlider.value = store.GetCurrentTimer() / store.GetStoreTimer();
    }
    private void OnEnable()
    {
        GameManager.OnUpdateBalcance += UpdateUI;
        //LoadGameData.OnLoadDataComplete += UpdateUI;
        LoadData.OnUpdateUIforLoadData += UpdateUI;
    }
    private void OnDisable()
    {
        GameManager.OnUpdateBalcance -= UpdateUI;
        // LoadGameData.OnLoadDataComplete += UpdateUI;
        LoadData.OnUpdateUIforLoadData -= UpdateUI;
    }
    public void UpdateUI()
    {
        //update Store BuyButton if you can afford the store
        if (GameManager.instance.CanBuy(store.NextStoreCost))
            BuyButton.interactable = true;
        else
            BuyButton.interactable = false;

            //interactable settings on manager button
            if (GameManager.instance.CanBuy(store.ManagerCost) && !store.ManagerUnlocked)
            {
                ManagerButton.interactable = true;
            }
            else
            {
                ManagerButton.interactable = false; 
            }
            if (store.ManagerUnlocked)
            {
            ManagerUnlocked();
            }

        //interactable settings on upgrade button
      /*  if (GameManager.instance.CanBuy(storeUpgrade.UpgradeCost) && !storeUpgrade.UpgradeUnlocked)
            UpgradeButton.interactable = true;
        else
            UpgradeButton.interactable = false;*/


        //Hide panel until you can afford the store
        CanvasGroup CG = transform.GetComponent<CanvasGroup>();
        if (!store.storeUnlocked && !GameManager.instance.CanBuy(store.NextStoreCost))
        {
            CG.alpha = 0;
            CG.interactable = false;
        }
        else
        {
            CG.alpha = 1;
            CG.interactable = true;
            store.storeUnlocked = true;
        }
            StoreCostText.text = "Buy $" + String.Format("{0:0.0}", store.NextStoreCost);
            StoreCountText.text = store.StoreCount.ToString();
            StoreProfitText.text = "Profit Per Store: " + String.Format("{0:0.0}", store.BaseStoreProfit);
        }
    public void BuyStoreOnClick()
    {
        if (GameManager.instance.CanBuy(store.NextStoreCost))
        {
            store.BuyStore();
                StoreCostText.text = "Buy $" + String.Format("{0:0.0}", store.NextStoreCost);
                StoreCountText.text = store.StoreCount.ToString();
                OnAchievementUnlock();
        }
            
    }
    public void MakeProfitOnClick()
    {
        store.MakeProfit();
    }
    public void ManagerUnlocked()
    {
        TextMeshProUGUI UnlockManagerText = ManagerButton.transform.Find("UnlockManagerText").GetComponent<TextMeshProUGUI>();
        UnlockManagerText.text = "Purchased";
    }
    private void OnAchievementUnlock()
        {
            Image Lock = UnlockAchievement.GetComponent<Image>();
            if (store.StoreCount >= 10)
            {
                Lock.color = Color.green;
                TextMeshProUGUI LockText = UnlockAchievement.transform.Find("LockText").GetComponent<TextMeshProUGUI>();
                LockText.text = "UNLOCKED";

            }
        }




}
}
