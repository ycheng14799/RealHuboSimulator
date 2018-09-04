using System.Collections;
using UnityEngine;

public class ItemLoader : MonoBehaviour
{

    public const string path = "items";


    // Use this for initialization
    void Start()
    {
        ItemContainer ic = ItemContainer.Load(path);

        foreach(Item item in ic.items)
        {
            print(item.name);
        }
    }

}
