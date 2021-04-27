using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace PathOfTrade
{
public class LoadData : MonoBehaviour
{
        public static LoadData instance;

        public delegate void UpdateUIforLoadData();               //delegate event
        public static event UpdateUIforLoadData OnUpdateUIforLoadData;
        public void LoadSavedGame()
        {
        GameManager.instance.CurrentBalance = 0.0f;
        GameManager.instance.CurrentBalance = PlayerPrefs.GetFloat("CurrentBalance");
        
       // Debug.Log("CurrentBalance: "+GameManager.instance.CurrentBalance);


        LoadSavedStoresData();

        LoadStoreUpgrades();

        if (OnUpdateUIforLoadData != null)
            OnUpdateUIforLoadData();
        }


        // Load the store data that has been saved in the playerpref file
        private static void LoadSavedStoresData()
        {
            // the counter is used to load the unique key for each store from the playerprefs as it doesn't handle arrays
            int counter = 1;
            foreach (Store StoreObj in GameManager.instance.StoreList)
            {
                // Get the number of stores the player owns
                int StoreCount = PlayerPrefs.GetInt("storecount_" + counter);
               // Debug.Log("store" + counter + " : " + StoreCount);
                


                if (StoreCount > 0)
                {
                    StoreObj.NextStoreCost = PlayerPrefs.GetFloat("storecost_" + counter);
                    // Store the # of stores in the store object
                    StoreObj.StoreCount = StoreCount;

                    // Check to see if the store is unlocked for the player
                    int Unlocked = PlayerPrefs.GetInt("storeunlocked_" + counter);
                    if (Unlocked == 1)
                    {
                        StoreObj.storeUnlocked = true;

                    }
                    else
                    {
                        StoreObj.storeUnlocked = false;
                    }

                    // Load if the timer was active for the store when they quit the game
                    Unlocked = PlayerPrefs.GetInt("storetimeractive_" + counter);
                    if (Unlocked == 1)
                    {
                        //.Log(StoreObj.StoreName + " timer was active... restart it");
                        StoreObj.StartTimer = true;
                    }
                    else
                        StoreObj.StartTimer = false;


                    //BaseStore profit
                    StoreObj.BaseStoreProfit = PlayerPrefs.GetFloat("BaseStoreProfit_" + counter);


                    StoreObj.StoreTimer = PlayerPrefs.GetFloat("storetimer_" + counter);

                    Unlocked = PlayerPrefs.GetInt("storemanagerunlocked_" + counter);
                    if (Unlocked == 1)
                    {
                        StoreObj.ManagerUnlocked = true;
                        StoreObj.StartTimer = true;

                    }

                }
                else
                {
                    StoreObj.StoreCount = 0;
                }
                counter++;
               
            }
            
        }

        private static void LoadStoreUpgrades()
        {
            
            // We need this counter to keep a unique key for each upgrade
            int counter = 1;
            foreach (StoreUpgrade StoreUpgrade in GameManager.instance.StoreUpgrades)
            {
                string stringKeyName = "storeupgradeunlocked_" + counter.ToString();
                int Unlocked = PlayerPrefs.GetInt(stringKeyName);
                // Debug.Log("Get StoreUpgrade for key-" + stringKeyName + " - " + StoreUpgrade.Store.StoreName + " - " + StoreUpgrade.UpgradeName + " Unlock Value=" + Unlocked.ToString());

                if (Unlocked == 1)
                {

                    StoreUpgrade.UpgradeUnlocked = true;
                }
                else
                    StoreUpgrade.UpgradeUnlocked = false;

                counter++;
            }
            
        }
        public void LoadGameFromMenu()
        {
            SceneManager.LoadScene(1);
            Invoke("LoadSavedGame",2f);
           // LoadSavedGame();
        }













    }

}

