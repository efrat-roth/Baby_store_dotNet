namespace DO
/// <summary>
/// struct for enums of the store.
/// </summary>


{
    /// <summary>
    /// Enum for the options of the orders
    /// </summary>
    public enum  OrderEnum {  Adding=1, PrintById, PrintAll, Update, Delete, Exit }
    /// <summary>
    /// Enum for the options of the items in the orders
    /// </summary>
    public enum OrderItemEnum { Adding=1, PrintById, PrintAll, PrintByTwoId, PrintAllByOrder, Update, Delete, Exit }
    /// <summary>
    /// Enum for the options of the products
    /// </summary>
    public  enum ProductEnum { Adding=1, PrintById, PrintAll, Update, Delete, Exit }
    /// <summary>
    /// Enum for the categories of the products
    /// </summary>
    public enum Category { Clothes, Bottles, Toys, Socks, Accessories, BabyCarriages }
    /// <summary>
    /// Enum for the types of the clothes
    /// </summary>
    public enum ClothesType { PinkFloralBabygro,BlueBabygro, BasicBabygro, Pajamas,ThreeBabygro }
    /// <summary>
    /// Enum for the types of the bottles
    /// </summary>
    public enum BottlesType { LargeBottle, MediumBottle, SmallBottle, ThreeBottle, DysneyBottle }
    /// <summary>
    /// Enum for the types of the toys
    /// </summary>
    public enum ToysType { DisneyDall, BarbieDall, Pillow, Biter, Lego }
    /// <summary>
    /// Enum for the types of the socks
    /// </summary>
    public enum SocksType { PurpleSocks, WarmSocks, CottonSocks, ThreeSocks, PinkSocks }
    /// <summary>
    /// Enum for the types of the accessories
    /// </summary>
    public enum AccessoriesType { Trampoline, BabyUniversitie, CarryCot, Cribe, Pacifier }
    /// <summary>
    /// Enum for the types of the carriages
    /// </summary>
    public enum BabyCarriagesType { BrownCarriage, NewBornCarriage, Stroller, Carriage, PinkCarriage }




};



