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
    public enum Category { Clothes, Bottles, Toys, Socks, Accessories, BabyCarriages }
    public enum ClothesType { PinkFloralBabygro,BlueBabygro, BasicBabygro, Pajamas,ThreeBabygro }
    public enum BottlesType { LargeBottle, MediumBottle, SmaalBottle, ThreeBottle, DysneyBottle }
    public enum ToysType { DisneyDall, BarbieDall, Pillow, Biter, Lego }
    public enum SocksType { PurpleSocks, WarmSocks, CottonSocks, ThreeSocks, PinkSocks }
    public enum AccessoriesType { Trampoline, BabyUniversitie, CarryCot, Cribe, Pacifier }
    public enum BabyCarriagesType { BrownCarriage, NewBornCarriage, Stroller, Carriage, PinkCarriage }




};



