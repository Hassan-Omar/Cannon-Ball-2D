using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // file managment 
using System.Xml; // basic xml 
using System.Xml.Serialization; // access to serializable 

public class XMLManager : MonoBehaviour
{
    public static XMLManager instance;
    private void Awake()
    {
        instance = this;
    }

    public ItemDataBase itemDB;

    // to save into Data base 
    public void saveToDB()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ItemDataBase));
        FileStream stream = new FileStream(Application.dataPath + "/Resources/xml/db.xml",FileMode.Create);
        serializer.Serialize(stream, itemDB);
        stream.Close();
    }

    // to load from data base 
    public void loadFromDB()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ItemDataBase));
        FileStream stream = new FileStream(Application.dataPath + "/Resources/xml/db.xml", FileMode.Open);
        itemDB = serializer.Deserialize(stream) as ItemDataBase;
        stream.Close();


    }



}

// class to represente a theme
[System.Serializable]
public class ItemEntry
{
    public int themeID;
    public int price;

}

[System.Serializable]
public class ItemDataBase
{
    public List<ItemEntry> list = new List<ItemEntry>();
}