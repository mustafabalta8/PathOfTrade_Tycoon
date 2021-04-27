using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using TMPro;
using UnityEngine.UI;


namespace PathOfTrade
{
public class LoadGameData : MonoBehaviour
{
    public delegate void LoadDataComplete();
    public static event LoadDataComplete OnLoadDataComplete;



    public TextAsset GameData;
    //UI
    public GameObject StorePanel;
    public GameObject StorePrefab;

    public GameObject ManagerPrefab;
    public GameObject ManagerPanel;

    public GameObject UpgradePrefab;
    public GameObject UpgradePanel;

    public GameObject AchievementPanel;
    public GameObject AchievementPrefab;

    [SerializeField] TextMeshProUGUI CompanyNameText;


    // Start is called before the first frame update
    void Start()
    {
        LoadData();

        if (OnLoadDataComplete != null)
            OnLoadDataComplete();
    }
    public void LoadData()
    {   //create XML doc
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(GameData.text);


        // to determine the starting balance
        XmlNodeList GameBalanceObj = xmlDoc.GetElementsByTagName("CurrentBalance"); 
        GameManager.instance.AddToBalance(float.Parse(GameBalanceObj[0].InnerText));

        // company name text loading
        XmlNodeList CompanyNameNode = xmlDoc.GetElementsByTagName("companyName");
        string CompanyName = CompanyNameNode[0].InnerText;
        CompanyNameText.text = CompanyName;


        //loading of stores 
        XmlNodeList StoreList = xmlDoc.GetElementsByTagName("store");
        GetStoreInfo(StoreList);
    }


        public void GetStoreInfo(XmlNodeList StoreList)
        {
            foreach (XmlNode StoreInfo in StoreList)
            {
                GameObject NewStore = Instantiate(StorePrefab);
                Store StoreObj = NewStore.GetComponent<Store>();
                NewStore.transform.SetParent(StorePanel.transform);
                GameManager.instance.AddStore(StoreObj);

                GameObject NewAchievement = Instantiate(AchievementPrefab);
                NewAchievement.transform.SetParent(AchievementPanel.transform);
                
             

                //Load store nodes
                XmlNodeList StoreNodes = StoreInfo.ChildNodes;
                foreach (XmlNode StoreNode in StoreNodes)
                {
                    //ID
                    if (StoreNode.Name == "id")
                    {
                        int StoreID = int.Parse(StoreNode.InnerText);
                        StoreObj.StoreID = StoreID;
                          /*for (int i = 1; i < 20; i++)
                               {
                                   if (StoreID == i)
                                   {
                                 Debug.Log("StoreID:" + StoreID + " i:" + i);
                                    float multiplier = i;
                                       StoreObj.BaseStoreCost = StoreObj.BaseStoreCost * multiplier;
                                       StoreObj.NextStoreCost = StoreObj.NextStoreCost * multiplier;
                                   }
                               }*/
                    }
                    if (StoreNode.Name == "name")
                    {
                        string SetName = StoreNode.InnerText;
                        TextMeshProUGUI StoreNameText = NewStore.transform.Find("StoreName").GetComponent<TextMeshProUGUI>();
                        StoreNameText.text = SetName; // SetName yerine direk StoreNode.InnerText; da yazabilirdim. 
                        StoreObj.StoreName = SetName;
                    }
                    if (StoreNode.Name == "image")
                    {
                        Sprite SetImage = Resources.Load<Sprite>(StoreNode.InnerText);
                        Image StoreImage = NewStore.transform.Find("Image").GetComponent<Image>();
                        StoreImage.sprite = SetImage;
                        StoreObj.StoreImage = StoreImage;
                    }
                    if (StoreNode.Name == "BaseStoreCost")
                    {
                        StoreObj.BaseStoreCost = float.Parse(StoreNode.InnerText) *(StoreObj.StoreID * StoreObj.StoreID * 2);   // Use " , " in XML for float.
                    }
                    if (StoreNode.Name == "BaseStoreProfit")
                    {
                        StoreObj.BaseStoreProfit = float.Parse(StoreNode.InnerText) * (StoreObj.StoreID * StoreObj.StoreID*2);
                    }
                    if (StoreNode.Name == "StoreTimer")
                    {
                        StoreObj.StoreTimer = float.Parse(StoreNode.InnerText) * (StoreObj.StoreID);
                    }
                    if (StoreNode.Name == "StoreCount")
                    {
                        StoreObj.StoreCount = int.Parse(StoreNode.InnerText);
                    }
                    if (StoreNode.Name == "NextStoreCost")
                    {
                        StoreObj.NextStoreCost = float.Parse(StoreNode.InnerText) * (StoreObj.StoreID * StoreObj.StoreID * 2);
                    }
                    //Create Manager
                    if (StoreNode.Name == "ManagerCost")
                    {
                        CreateManager(StoreNode, StoreObj);
                    }
                    //Create Upgradesss
                    if (StoreNode.Name == "upgrades")
                    {
                        CreateUpgrades(StoreNode, StoreObj);
                        
                    }
                

                }
                //achievements have to created below "foreach (XmlNode StoreNode in StoreNodes)" for obvious reasons
                CreateAchievement(StoreObj, NewAchievement);
            }
        }
    void CreateUpgrades(XmlNode StoreNode,Store StoreObj)
    {
        foreach(XmlNode UpgradeNode in StoreNode)
        {
            CreateUpgrade( UpgradeNode,  StoreObj);
            
            }
    }
    void CreateManager(XmlNode StoreNode, Store StoreObj)
    {
        GameObject NewManager = Instantiate(ManagerPrefab);
        NewManager.transform.SetParent(ManagerPanel.transform);

        TextMeshProUGUI FieldOfManager = NewManager.transform.Find("FieldOfManager").GetComponent<TextMeshProUGUI>();
        FieldOfManager.text = StoreObj.StoreName;
        StoreObj.ManagerCost = float.Parse(StoreNode.InnerText) * (StoreObj.StoreID * StoreObj.StoreID * 2);

            Image StoreImage = NewManager.transform.Find("Image").GetComponent<Image>();
            StoreImage.sprite = StoreObj.StoreImage.sprite;


        Button ManagerButton = NewManager.transform.Find("ManagerButton").GetComponent<Button>();
        TextMeshProUGUI UnlockManagerText = ManagerButton.transform.Find("UnlockManagerText").GetComponent<TextMeshProUGUI>();
        UnlockManagerText.text ="Hire $" + StoreObj.ManagerCost.ToString();

        UIStore UIstore = StoreObj.GetComponent<UIStore>();
        UIstore.ManagerButton = ManagerButton;

        ManagerButton.onClick.AddListener(StoreObj.UnlockManager);

    }
    void CreateUpgrade(XmlNode StoreNode, Store StoreObj)
    {
        
        GameObject NewUpgrade = Instantiate(UpgradePrefab);
        NewUpgrade.transform.SetParent(UpgradePanel.transform);

        TextMeshProUGUI UpgradeText = NewUpgrade.transform.Find("UpgradeText").GetComponent<TextMeshProUGUI>();
        UpgradeText.text = StoreObj.StoreName;
       // StoreObj.UpgradeCost = float.Parse(StoreNode.InnerText);

        Button UpgradeUnlockButton = NewUpgrade.transform.Find("UpgradeUnlockButton").GetComponent<Button>();
        TextMeshProUGUI UnlockText = UpgradeUnlockButton.transform.Find("UnlockText").GetComponent<TextMeshProUGUI>();

            Image StoreImage = NewUpgrade.transform.Find("Image").GetComponent<Image>();
            StoreImage.sprite = StoreObj.StoreImage.sprite;

            StoreUpgrade storeUpgrade = NewUpgrade.GetComponent<StoreUpgrade>();
          storeUpgrade.UpgradeCost = float.Parse(StoreNode.InnerText) * (StoreObj.StoreID * StoreObj.StoreID * 2);
/*Önemli*/storeUpgrade.store = StoreObj;      //  *** to determine which store we refer in storeUpgrade class****

            UnlockText.text = "Unlock $" + storeUpgrade.UpgradeCost.ToString();

        //UIStore UIstore = StoreObj.GetComponent<UIStore>();
        storeUpgrade.UpgradeButton = UpgradeUnlockButton;
        UpgradeUnlockButton.onClick.AddListener(storeUpgrade.UnlockUpgrade);


            //Save Data Related
            //storeUpgrade.CreateStoreUpgrade(StoreObj);
        GameManager.instance.AddUpgrade(storeUpgrade);
        }
        void CreateAchievement(Store StoreObj, GameObject NewAchievement)
        {
            TextMeshProUGUI StoreText = NewAchievement.transform.Find("UpgradeName").GetComponent<TextMeshProUGUI>();
            StoreText.text = StoreObj.StoreName;

            Image StoreImage = NewAchievement.transform.Find("StoreImage").GetComponent<Image>();
            StoreImage.sprite = StoreObj.StoreImage.sprite;

            Image LockImage = NewAchievement.transform.Find("LockPanel").GetComponent<Image>();
            UIStore UIstore = StoreObj.GetComponent<UIStore>();
            UIstore.UnlockAchievement = LockImage;
        }

}
}

