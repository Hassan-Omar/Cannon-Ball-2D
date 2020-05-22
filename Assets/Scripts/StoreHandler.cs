using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StoreHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] allItemsComponent;
    private int coins; 
    private void Start()
    {
        var avalabileComps = getAllAailableItems(); 
        // check on The Items that is already Have 
        for(int i=0; i<5; i++)
        {
            if(avalabileComps.Contains(allItemsComponent[i].gameObject.name.ToString()))
            {
                // this Item Is Available Now 
                // Remove Price , Description 
                var compInChilderen = allItemsComponent[i].GetComponentsInChildren<TextMeshPro>();
                compInChilderen[0].gameObject.SetActive(false);
                compInChilderen[1].gameObject.SetActive(false);
                // Remove Buy Btn 
                allItemsComponent[i].GetComponentInChildren<Button>().gameObject.SetActive(false);
            }
        }
    }
    public void loadGame()
    {
        SceneManager.LoadSceneAsync("CoreGame");
    }

    public void payItem(int ItemId)
    {
        //EchItem -> ID , Price  So WE can Store In Player Pref as Key -> Id , VAl -> Price 
       
        /**
         * Player Pref for Items 
         *      1 -> 500 
         *      2 -> 1000
         * 
         */

        // check if coins > item's price 
        if(coins >PlayerPrefs.GetInt(ItemId.ToString()))
        {
            var availabileItems = getAllAailableItems();
            // check if Item Is not exist before 
            if (!availabileItems.Contains(ItemId.ToString()))
            {
                Debug.Log("Item " + ItemId + "added to List");
                PlayerPrefs.SetString("AvailableItems", availabileItems + "-" + ItemId);
            }
        }
    }

    /// <summary>
    /// Store Active Theme To Load when Play 
    /// </summary>
    /// <param name="themeId">ID For Theme We Need To Set Active </param>
    public void selectActiveTheme(int themeId)
    {
        PlayerPrefs.SetInt("ActiveTheme", themeId);
    }
    /// <summary>
    /// Same Previous But for Tools
    /// </summary>
    /// <param name="toolId"></param>
    public void selectActiveTool(int toolId)
    {
        PlayerPrefs.SetInt("ActiveTheme", toolId);
    }

    /// <summary>
    /// Function Give me List of Items that User ALready Have 
    /// </summary>
    /// <returns>String Contins ids separated with - </returns>
    private string getAllAailableItems()
    {
        return PlayerPrefs.GetString("AvailableItems"); 
    }
}
