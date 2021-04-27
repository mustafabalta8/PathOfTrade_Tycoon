using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace PathOfTrade
{
public class GameManager : MonoBehaviour
{
    public static GameManager instance;         //...singleton design pattern


        public float CurrentBalance=0.0f;

    public delegate void UpdateBalance();               //delegate event
    public static event UpdateBalance OnUpdateBalcance; //delegate event

        //Save Data related
        //public bool DontSave = false;
        public ArrayList StoreList = new ArrayList();
        public ArrayList StoreUpgrades = new ArrayList();
        public void AddStore(Store NewStore)
        {

            StoreList.Add(NewStore);
            
          /*  if (NewStore == (object)StoreList[0])
            {
                NewStore.StoreCount = 1;
                if (1 > 0)
                    NewStore.storeUnlocked = true;
            }*/


        }
        public void AddUpgrade(StoreUpgrade NewStoreUpgrade)
        {

            StoreUpgrades.Add(NewStoreUpgrade);

        }


        private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
            
            if (OnUpdateBalcance != null)
            OnUpdateBalcance();
        // UIManager.instance.UIUpdate(); 
        // BalanceText.text = CurrentBalance.ToString();   First way of showing Current balance in UI
    }

    [SerializeField] GameObject EscapePanel;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscapePanel.SetActive(true);
               // Debug.Log(StoreList.Count);
        }
    }
    public void Resume()
    {
        EscapePanel.SetActive(false);
    }
    public void AddToBalance(float amt)
    {
        CurrentBalance += amt;
        if (OnUpdateBalcance != null)                   //     
            OnUpdateBalcance();                         //       Third- currently working way of showing Current balance in UI(check UIManager OnEnable Method) 

        //UIManager.instance.UIUpdate();                       // Singleton pattern
        // BalanceText.text = CurrentBalance.ToString();       // Current balance in UI
    }
    public bool CanBuy(float AmtToSpend)
    {
        if (CurrentBalance >= AmtToSpend)
       return true;
        else
       return false;
    }
    public float GetCurrentBalance()
    {
        return CurrentBalance;
    }
}
}

