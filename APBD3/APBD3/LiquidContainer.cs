namespace APBD3;

public class LiquidContainer(
    double loadWeight,
    double height,
    double containerWeight,
    double depth,
    double maxLoadWeight) : Container(loadWeight, height, containerWeight, depth, maxLoadWeight, sign:'L'), IHazardNotifier
{
    

    public override void AddToContainer(double weight, bool? isDangerousLoad)
    {
        if (isDangerousLoad == null)
        {
            return;
        }

        if (LoadWeight + weight > MaxLoadWeight)
        {
            new OverfillException("Dopuszczalna masa została przekroczona [OverfillException]");
        }
        

        if ((bool)isDangerousLoad)
        {
            double CurrentMaxLoad = MaxLoadWeight * 0.5;
            if (LoadWeight + weight <= CurrentMaxLoad)
            {
                LoadWeight += weight;
            }
            else
            {
                DangerousSituation(base.SerialNumber, "Probujesz dac za duzo niebezpiecznego towaru");
                
            }
        }
        else
        {
            double CurrentMaxLoad = MaxLoadWeight * 0.9;
            if (LoadWeight + weight <= CurrentMaxLoad)
            {
                LoadWeight += weight;
            }
            else
            {
                DangerousSituation(base.SerialNumber, "Probujesz dac za duzo towaru");
                
            }
        }
    }

    public void DangerousSituation(string serialNumber, string messege)
    {
        Console.WriteLine($"Blad dla {serialNumber}: {messege}");
    }
}