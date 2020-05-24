using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StoreHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] allItemsComponent;
    [SerializeField] private GameObject[] selectionImage;
    [SerializeField] private Text text;
    [SerializeField] private GameObject panel;
    public int coins; 
    private void Start()
    {
        coins = PlayerPrefs.GetInt("Coins");
        updateTxt(coins);
        var avalabileComps = getAllAailableItems(); 
        // check on The Items that is already Have 
        for(int i=0; i<5; i++)
        {
            if(avalabileComps.Contains(allItemsComponent[i].gameObject.name.ToString()))
            {
                // this Item Is Available Now 
                // Remove Price , Description 
                var compInChilderen = allItemsComponent[i].GetComponentsInChildren<TextMeshProUGUI>();
                compInChilderen[0].gameObject.SetActive(false);
                compInChilderen[1].gameObject.SetActive(false);
                // Remove Buy Btn 
                allItemsComponent[i].GetComponentInChildren<Button>().gameObject.SetActive(false);
            }
        }
    }
    public void updateTxt(int val)
    {
        coins = val;
        text.text = "Coins\r\n" + coins; 
    }
    public void loadGame()
    {
        SceneManager.LoadSceneAsync("Start");
    }

    public void payItem(string ItemId)
    {
        //EchItem -> ID , Price  So WE can Store In Player Pref as Key -> Id , VAl -> Price 
       
        /**
         * Player Pref for Items 
         *      1 -> 500 
         *      2 -> 1000
         */

        // check if coins > item's price 
        if(coins > PlayerPrefs.GetInt(ItemId))
        {
            var availabileItems = getAllAailableItems();
            // check if Item Is not exist before 
            if (!availabileItems.Contains(ItemId.ToString()))
            {
                PlayerPrefs.SetString("AvailableItems", availabileItems + "-" + ItemId);
                // Update  Coins 
                coins -= PlayerPrefs.GetInt(ItemId);
                PlayerPrefs.SetInt("Coins", coins);
                updateTxt(coins);
                Debug.Log("Item " + ItemId + "added to List "+coins +"  " + PlayerPrefs.GetInt(ItemId));

            }

        }
        else
        {
            loadNOTEngphCoins();
        }
    }

    /// <summary>
    /// Store Active Theme To Load when Play 
    /// </summary>
    /// <param name="themeId">ID For Theme We Need To Set Active </param>
    public void selectActiveTheme(string themeId)
    {
        if (getAllAailableItems().Contains(themeId))
        {
            PlayerPrefs.SetString("ActiveTheme", themeId);
        }
    }
    /// <summary>
    /// Same Previous But for Tools
    /// </summary>
    /// <param name="toolId"></param>
    public void selectActiveTool(string toolId)
    {
        if(getAllAailableItems().Contains(toolId))
        {
            PlayerPrefs.SetString("ActiveTool", toolId);
        }
    }

    /// <summary>
    /// Function Give me List of Items that User ALready Have 
    /// </summary>
    /// <returns>String Contins ids separated with - </returns>
    private string getAllAailableItems()
    {
        return PlayerPrefs.GetString("AvailableItems"); 
    }

    public void loadNOTEngphCoins()
    {
        panel.SetActive(true);
    }

    public void updateAfterSelect(string id)
    {
        if (getAllAailableItems().Contains(id))
        { 
            for (int i=0; i< 3; i++)
            {
                selectionImage[i].SetActive(false);
            }

            if (id == "0")
            {
                selectionImage[0].SetActive(true);
            }
            else if (id == "1")
            {
                selectionImage[1].SetActive(true);
            }
            else if (id == "2")
            {
                selectionImage[2].SetActive(true);
            }
        }
    }

    public void updateAfterSelect_Tool(string id)
    {
        if (getAllAailableItems().Contains(id))
        {
            for (int i = 3; i < 7; i++)
            {
                selectionImage[i].SetActive(false);
            }
            if (id == "a")
            {
                selectionImage[3].SetActive(true);
            }
            else if (id == "b")
            {
                selectionImage[4].SetActive(true);
            }
            else if (id == "c")
            {
                selectionImage[5].SetActive(true);
            }
            else if (id == "d")
            {
                selectionImage[6].SetActive(true);
            }
        }
    }

    public void updateAfterBuy(GameObject button)
    {
        // check if coins > item's price 
        if (coins > PlayerPrefs.GetInt(button.transform.parent.name))
        {
            button.SetActive(false);
        }
    }
}
