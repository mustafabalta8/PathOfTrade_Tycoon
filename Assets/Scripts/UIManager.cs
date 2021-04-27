using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace PathOfTrade
{
    
    public class UIManager : MonoBehaviour
    {
        public delegate void UpdateUpgrade();               //delegate event
        public static event UpdateUpgrade OnUpdateUpgrade; //delegate event
        public enum State
        {
            Main,
            Manager,
            Upgrade,
            Achievement
        }
        public static UIManager instance;
        [SerializeField] TextMeshProUGUI BalanceText;

        public GameObject ManagerPanel;
        public GameObject StorePanel;
        public GameObject UpgradePanel;
        public GameObject AchievementPanel;
        public State CurrentState;

        public StoreUpgrade storeUpgrade;

        [SerializeField] GameObject EscPanel;

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }
        // Start is called before the first frame update
        void Start()
        {
            UpdateUI();
            CurrentState = State.Main;
            ScaleGrid();
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnEnable()
        {
            GameManager.OnUpdateBalcance += UpdateUI;
            LoadData.OnUpdateUIforLoadData += UpdateUI;
        }
        private void OnDisable()
        {
            GameManager.OnUpdateBalcance -= UpdateUI;
            LoadData.OnUpdateUIforLoadData -= UpdateUI;
        }
        public void UpdateUI()
        {
            BalanceText.text = String.Format("{0:0.00}", GameManager.instance.CurrentBalance);
           // BalanceText.text = GameManager.instance.GetCurrentBalance().ToString();
        }

        public void OnClickManagerButton()
        {
            if (CurrentState == State.Main)
            {
                CurrentState = State.Manager;
                ManagerPanel.SetActive(true);
                //StorePanel.SetActive(false);
            }
            else
            {
                CurrentState = State.Main;
                ManagerPanel.SetActive(false);
                UpgradePanel.SetActive(false);
                AchievementPanel.SetActive(false);
            }
        }
        public void OnClickUpgradeButton()
        {
            if (CurrentState == State.Main)
            {
                CurrentState = State.Upgrade;
                UpgradePanel.SetActive(true);
                if (OnUpdateUpgrade != null)
                    OnUpdateUpgrade();
            }
            else
            {
                CurrentState = State.Main;
                UpgradePanel.SetActive(false);
                ManagerPanel.SetActive(false);
                AchievementPanel.SetActive(false);
            }
        }
        public void OnClickAchievementButton()
        {
            if (CurrentState == State.Main)
            {
                CurrentState = State.Achievement;
                AchievementPanel.SetActive(true);
                if (OnUpdateUpgrade != null)
                    OnUpdateUpgrade();
            }
            else
            {
                CurrentState = State.Main;
                UpgradePanel.SetActive(false);
                ManagerPanel.SetActive(false);
                AchievementPanel.SetActive(false);

            }
        }
        void ScaleGrid()
        {
            if (Screen.width == 1920|| Screen.width == 1680|| Screen.width == 1600)
            {
                GridLayoutGroup esc= EscPanel.GetComponent<GridLayoutGroup>();
                esc.cellSize = new Vector2(475, 105);
            }
        }

    }
    

}

