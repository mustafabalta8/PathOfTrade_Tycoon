using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;



namespace PathOfTrade
{
public class Store : MonoBehaviour
{
   /* public delegate void ManagerUnlockedDel();               //delegate event
    public static event ManagerUnlockedDel OnManagerUnlocked;*/

    //public classes
    public float BaseStoreCost=0.0f;
    public float NextStoreCost = 0.0f;
    public float BaseStoreProfit = 0.0f;
    public int StoreCount=0;
    public float StoreMultiplier=1;
    public bool ManagerUnlocked=false;
    public bool storeUnlocked=false;
    public int StoreTimerDivision=10;

    public float StoreTimer = 0.0f;
    float CurrentTimer=0f;
    public bool StartTimer=false;


    //Load GameData related
    public string StoreName;
    public float ManagerCost;
    public int StoreID;

        //public float UpgradeCost;
        //public bool UpgradeUnlocked = false;

    public Image StoreImage;
    private void Awake()
    {
        NextStoreCost = BaseStoreCost;
        //StoreCount = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartTimer = false;
        
    }

    // Update is called once per frame
    void Update()
    {
            if (StartTimer)
            {
                CurrentTimer += Time.deltaTime;
                if (CurrentTimer > StoreTimer)
                {
                    if (!ManagerUnlocked)
                    {
                        StartTimer = false;
                    }
                    GameManager.instance.AddToBalance(BaseStoreProfit * StoreCount);
                    CurrentTimer = 0;
                }

            }

    }
    public void BuyStore()
    {    
        StoreCount += 1;
        // StoreCountText.text = StoreCount.ToString();
        GameManager.instance.AddToBalance(-NextStoreCost);
        NextStoreCost = (BaseStoreCost * Mathf.Pow(StoreMultiplier, StoreCount));
        // UpdateBuyButton();
        if (StoreCount % StoreTimerDivision == 0)
            StoreTimer = StoreTimer / 2;
    }
    public void MakeProfit()
    {
        if (!StartTimer && StoreCount > 0)
        {
            StartTimer = true;
        }
    }
    /*void UpdateBuyButton()
    {
        StoreCostText.text = "Buy $" + NextStoreCost.ToString();
    }*/
    public float GetCurrentTimer()
    {
        return CurrentTimer;
    }
    public float GetStoreTimer()
    {
        return StoreTimer;
    }

    public void UnlockManager()
    {
        if (!ManagerUnlocked && GameManager.instance.CanBuy(ManagerCost) )
        {
            GameManager.instance.AddToBalance(-ManagerCost);
            ManagerUnlocked = true;
            transform.GetComponent<UIStore>().ManagerUnlocked();
            StartTimer = true;
        }
    }
    /*public void UnlockUpgrade()
    {
        if (!UpgradeUnlocked && GameManager.instance.CanBuy(UpgradeCost))
        {
            GameManager.instance.AddToBalance(-UpgradeCost);
            UpgradeUnlocked = true;
            BaseStoreProfit = BaseStoreProfit + BaseStoreProfit;
            transform.GetComponent<UIStore>().UpgradeUnlocked();
        }
    }*/
    public void UnlockAchievement()
        {
            if (StoreCount >= 10)
            {

                //you unlock first achievement.
                //Your sell process time dicreased by half
            }
        }
}
}

