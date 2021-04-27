using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PathOfTrade
{
public class SaveData : MonoBehaviour
{

        public void Save()
        {
            PlayerPrefs.SetFloat("CurrentBalance", GameManager.instance.GetCurrentBalance());

            SaveStore();
            SaveUpgrades();
        }
        

        private void SaveStore()
        {
            int counter = 1;
            foreach (Store StoreObj in GameManager.instance.StoreList)
            {
                PlayerPrefs.SetInt("storecount_" + counter, StoreObj.StoreCount);
                PlayerPrefs.SetFloat("BaseStoreProfit_" + counter, StoreObj.BaseStoreProfit);  //upgrade related // instead of a multipilier
                PlayerPrefs.SetFloat("storetimer_" + counter, StoreObj.GetStoreTimer());
                PlayerPrefs.SetFloat("storecost_" + counter, StoreObj.NextStoreCost);

               // Debug.Log("next store cost:" + StoreObj.NextStoreCost);

                int Unlocked = 0;
                if (StoreObj.StartTimer)
                {
                    Unlocked = 1;

                }
                PlayerPrefs.SetInt("storetimeractive_" + counter, Unlocked);

                Unlocked = 0;
                if (StoreObj.ManagerUnlocked)
                    Unlocked = 1;
                PlayerPrefs.SetInt("storemanagerunlocked_" + counter, Unlocked);

                Unlocked = 0;
                if (StoreObj.storeUnlocked)
                    Unlocked = 1;
                PlayerPrefs.SetInt("storeunlocked_" + counter, Unlocked);

                counter++;
            }
        }

        // Saves the store upgrades in the playerprefs
        private static void SaveUpgrades()
        {
            int counter = 1;
            foreach (StoreUpgrade StoreUpgrade in GameManager.instance.StoreUpgrades)
            {
                // We only need to store if it is unlocked or not 
                // The game data knows which store it goes with
                string stringKeyName = "storeupgradeunlocked_" + counter.ToString();

                // Save upgrade unlock status
                int Unlocked = 0;
                if (StoreUpgrade.UpgradeUnlocked)
                    Unlocked = 1;
                PlayerPrefs.SetInt(stringKeyName, Unlocked);
                //Debug.Log("Save StoreUpgrade for key-" + stringKeyName + ":" + StoreUpgrade.UpgradeName + " Unlock Value=" + Unlocked.ToString());
                counter++;
            }
        }



    }
}


