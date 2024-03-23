namespace APBD3;

public class CoolingContainer(
    double loadWeight,
    double height,
    double containerWeight,
    double depth,
    double maxLoadWeight,
    char sign,
    Product productType,
    double temperatureInContainer) : Container(loadWeight, height, containerWeight, depth, maxLoadWeight, sign: 'C')
{
    private static Dictionary<Product, double> productsList = new Dictionary<Product, double>()
    {
        { Product.Bananas, 18.3 },
        { Product.Chocolate, 18 },
        { Product.Fish, 2 },
        { Product.Meat, -15 },
        { Product.IceCream, -18 },
        { Product.FrozenPizza, -30 },
        { Product.Cheese, 7.2 },
        { Product.Sausages, 5 },
        { Product.Butter, 20.5 },
        { Product.Eggs, 19 }
    };

    public Product ProductType { get; } = productType;
    public double TemperatureInContainer { get; set; } = temperatureInContainer;

    public override void AddToContainer(double weight, bool? b = null)
    {
        Console.WriteLine("Musisz podać jaki rodzaj produktu chcesz dodac:" +
                          "AddToContainer(double weight, Product product)");
    }

    public void AddToContainer(double weight, Product product)
    {
        if (product.Equals(ProductType) && TemperatureInContainer >= productsList[product])
        {
            base.AddToContainer(weight);
        }
        else
        {
            Console.WriteLine("Temperatura lub rodzaj produktu niezgodny z wymaganiami");
        }
    }
}