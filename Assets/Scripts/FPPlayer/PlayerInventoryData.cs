public static class PlayerInventoryData
{
    public static bool HasEngine = false;
    public static bool HasFireExt = false;
    public static bool HasFuel = false;
    public static bool HasHammer = false;

    public static bool HasAllItems =>
        HasEngine && HasFireExt && HasFuel && HasHammer;
}
