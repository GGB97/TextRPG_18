internal static class InventoryHelpers
{



    public static List<Item> GetItemsByType(List<Item> items, ItemType itemType)
    {
        List<Item> filteredItems = new List<Item>();

        foreach (var item in items)
        {
            if (item.getType() == (int)itemType)
            {
                filteredItems.Add(item);
            }
        }

        return filteredItems;
    }
}