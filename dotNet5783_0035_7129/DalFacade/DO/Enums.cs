namespace DO;
/// <summary>
/// struct for enums of the store.
/// </summary>

public struct Enums
{
    /// <summary>
    /// Enum for the options of the orders
    /// </summary>
    public enum  orderEnum {  Adding, PrintById, printAll, Update, Delete, Exit }
    /// <summary>
    /// Enum for the options of the items in the orders
    /// </summary>
    public enum orderItemEnum { Adding, PrintById, printAll, Update, Delete, Exit }
    /// <summary>
    /// Enum for the options of the products
    /// </summary>
    public  enum productEnum { Adding, PrintById, printAll, Update, Delete, Exit }
    /// <summary>
    /// Enum for the categories of the products
    /// </summary>
    public enum Category { Clothes, Bottles, Toys,Socks, Accessories, BabyCarriages }
};



