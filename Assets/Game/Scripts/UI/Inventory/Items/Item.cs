using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public RuntimeAnimatorController overrideAnimator = null;
    public bool isSold = false;
    public bool isOnInventory = false;
    public int price = 0;
    public string type = null;
}