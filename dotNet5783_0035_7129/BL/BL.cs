using BlImplementation;
using BlApi;


namespace BlApi
{


    sealed public class BL :IBl 
    {
        public ICart Cart => new Cart();

        public IProduct Product => new Product();
        public IOrder Order => new Order();



    }
    
}