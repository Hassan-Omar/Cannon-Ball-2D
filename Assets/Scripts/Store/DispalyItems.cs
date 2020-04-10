using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class DispalyItems : MonoBehaviour
{ 

    [SerializeField] private int yOffset;
    [SerializeField] private GameObject themePrefab;
    [SerializeField] private Transform themeHolder;
    [SerializeField] private Transform startPos;
    void Start()
    {
        foreach (ItemEntry item in XMLManager.instance.itemDB.list)
        {
            Display(item);
        }
    }

    public void upload()
    {
        foreach (ItemEntry item in XMLManager.instance.itemDB.list)
        {
            Display(item);
        }
    }

    // function to craete 
    private void Display(ItemEntry item)
    {
        var instItem = Instantiate(themePrefab, startPos.position, Quaternion.identity, themeHolder);
        Debug.Log(Resources.Load<Sprite>("themes/" + item.themeID + "/viewImg"));
        instItem.GetComponent<Image>().sprite = Resources.Load<Sprite>("themes/" + item.themeID+"/viewImg");
        instItem.GetComponentInChildren<TextMeshProUGUI>().text = "Price "+item.price+"$";
        this.startPos.position -= new Vector3(0, yOffset, 0);
    }
}
