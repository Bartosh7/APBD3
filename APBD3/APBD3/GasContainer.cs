namespace APBD3;

public class GasContainer(
    double loadWeight,
    double height,
    double containerWeight,
    double depth,
    double maxLoadWeight,
    char sign,
    double pressure) : Container(loadWeight, height, containerWeight, depth, maxLoadWeight, sign: 'G'), IHazardNotifier
{
    public double Pressure { get; set; } = pressure;

    public override void EmptyContainer()
    {
        LoadWeight *= 0.05;
    }

    public void DangerousSituation(string serialNumber, string messege)
    {
        Console.WriteLine($"Potencjalne zagrozenie w kontenerze gazowym {serialNumber}: {messege}");
    }
}