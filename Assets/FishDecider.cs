using UnityEngine;

//code from https://medium.com/@kshesho/unity-how-to-create-a-weighted-loot-table-3bcbf478eaf9  
public class FishDecider : MonoBehaviour
{

    Item[] _items =
    {
        new Item("Sea Bass", 25),
        new Item("Salmon", 15),
        new Item("Tuna", 9),
        new Item("Orca", 1)
    };

    public string RandomFish()
    {
        int total = 0;
        foreach(var item in _items)
        {
            total += item.spawnWeight;
        }
        int randomWeight = Random.Range(1, total);

        for (int i = 0; i < _items.Length; i++)
        {
            if (randomWeight <= _items[i].spawnWeight)
            {
                return _items[i].name;
            }
            else randomWeight -= _items[i].spawnWeight;
        }

        return null;
    }
}

public class Item
{
    public string name;
    public int spawnWeight;

    public Item(string name_, int spawnWeight_)
    {
        name = name_;
        spawnWeight = spawnWeight_;
    }
}