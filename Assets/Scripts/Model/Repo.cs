using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Repo :IRepo
{
    public Bag GetInventData()
    {
        if  (LoadData("/Invent.json") != null)
        { return LoadData("/Invent.json"); }
        else
            return GenerateRandomItems(GetListItems()); 
    }
    public Bag GetStashData()
    {
        if (LoadData("/Stash.json") != null)
        { return LoadData("/Stash.json"); }
        else
            return new Bag();
    }

    public void SaveInvent(Bag invent)
    {
             SaveData("/Invent.json", invent);
    }
    public void SaveStash(Bag stash)
    {
        SaveData("/Stash.json", stash);
    }
    private List<Item> GetListItems()
    {
        List<Item> itemList = new List<Item>();
        string[] arrayItemString = { "icon_altar_left", "icon_altar_mid", "icon_altar_right",
            "icon_barrel_empty", "icon_barrel_honey", "icon_beer", "icon_bell",
        "icon_berry","icon_berry_drink","icon_berry_poison","icon_birch_board","icon_birch_wood"};
        foreach (var element in arrayItemString)
        {
            itemList.Add(new Item(element));
        }
        return itemList;
    }
    private Bag GenerateRandomItems(List<Item> items)
    {
        Bag bag = new Bag();
      
        for (int i = 0; i < 5; i++)
        {
            int rnd = Random.Range(0, items.Count);
            bag.Items.Add(items[rnd]);
        }
        return bag;
    }
    private Bag LoadData(string path)
    {if (JsonUtility.FromJson<Bag>(File.ReadAllText(Application.streamingAssetsPath + path)) != null)
            return JsonUtility.FromJson<Bag>(File.ReadAllText(Application.streamingAssetsPath + path));
        else return null;
    }
    private void SaveData(string path, Bag bag)
    {
        File.WriteAllText(Application.streamingAssetsPath + path, JsonUtility.ToJson(bag));
    }

}




