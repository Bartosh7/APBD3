namespace APBD3;

public abstract class Container(
    double loadWeight,
    double height,
    double containerWeight,
    double depth,
    double maxLoadWeight,
    char sign)
{
    private static int _nextId = 1;
    public int Id { get; } = _nextId++;

    public double LoadWeight { get; set; } = loadWeight;
    public double Height { get; set; } = height;
    public double ContainerWeight { get; set; } = containerWeight;
    public double Depth { get; set; } = depth;
    public double MaxLoadWeight { get; set; } = maxLoadWeight;
    private char Sign { get; set; } = sign;


    public string SerialNumber
    {
        get { return $"KON-{Sign}-{Id}"; }
    }


    public virtual void EmptyContainer()
    {
        LoadWeight = 0;
    }

    public virtual void AddToContainer(double weight, bool? b = null)
    {
        if (LoadWeight + weight <= MaxLoadWeight)
        {
            LoadWeight += weight;
        }
        else
        {
            throw new OverfillException("Dopuszczalna masa została przekroczona [OverfillException]");
        }
    }


    protected class OverfillException : Exception
    {
        public OverfillException(string message) : base(message)
        {
        }
    }

    public override string ToString()
    {
        return
            $"Kontener {SerialNumber}(height={Height}, depth={Depth}, actualProductWeight={LoadWeight}, " +
            $"maxLoadWeight={MaxLoadWeight}, containerWeight={containerWeight})";
    }
}