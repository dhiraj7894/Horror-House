using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "item", menuName = "Items/item")]
public class Item : ScriptableObject
{
    public int id;
    public string Name;
    public Image UI;
    public float attachmentTime;
}
