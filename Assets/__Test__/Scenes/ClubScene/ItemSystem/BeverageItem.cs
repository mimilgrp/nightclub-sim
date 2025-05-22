public class BeverageItem : TakeDropItem
{
    public Beverage beverage;
    public int quantity;
    public float price;

    public enum Beverage
    {
        Beer,
        Vodka,
        Tequila,
        Liquor
    }
}
