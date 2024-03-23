namespace APBD3;

public class ContainerShip(double speed, int maxContainerAmount, double maxContainerWeight)
{
    private static int _nextId = 1;
    public int Id { get; } = _nextId++;


    public double Speed { get; } = speed;
    public int MaxContainerAmount { get; } = maxContainerAmount;
    public double MaxContainerWeight { get; } = maxContainerWeight * 1000;
    public double CurrentContainerWeight { get; set; } = 0;

    public List<Container> ContainersList = new List<Container>();

    public void AddContainer(Container container)
    {
        if (ContainersList.Count < MaxContainerAmount &&
            CurrentContainerWeight + container.LoadWeight + container.ContainerWeight < maxContainerWeight)
        {
            ContainersList.Add(container);
            CurrentContainerWeight += container.LoadWeight + container.ContainerWeight;
        }
        else
        {
            Console.WriteLine("Nie mozna dodac tego kontenera");
        }
    }


    public void AddContainer(Container[] containers)
    {
        double WeightToAdd = containers.Sum(c => c.ContainerWeight + c.LoadWeight);
        if (WeightToAdd + CurrentContainerWeight < MaxContainerWeight &&
            ContainersList.Count + containers.Length <= MaxContainerAmount)
        {
            foreach (Container c in containers)
            {
                AddContainer(c);
            }
        }
        else
        {
            Console.WriteLine("Suma wag tych kontnenrow przekracza dopuszczalna ladownosc");
        }
    }


    public void DeleteContainer(int index)
    {
        if (index >= 0 && index < ContainersList.Count)
        {
            CurrentContainerWeight -= (ContainersList[index].LoadWeight + ContainersList[index].ContainerWeight);
            ContainersList.RemoveAt(index);
        }
    }

    public void EmptyShip()
    {
        ContainersList.Clear();
        CurrentContainerWeight = 0;
    }

    public void ChangeContainer(String serialName, Container container)
    {
        {
            int index = 0;
            while (index < ContainersList.Count - 1 && ContainersList[index].SerialNumber != serialName)
            {
                index++;
            }

            if (index == ContainersList.Count)
            {
                Console.WriteLine("Nie ma takiego kontnenra");
            }
            else
            {
                if (CurrentContainerWeight - ContainersList[index].ContainerWeight - ContainersList[index].LoadWeight +
                    container.ContainerWeight + container.LoadWeight <= MaxContainerWeight)
                {
                    DeleteContainer(index);
                    AddContainer(container);
                }
            }
        }
    }

    public void changeContainersShip(int index, ContainerShip Destination)
    {
        if (Destination.CurrentContainerWeight + ContainersList[index].ContainerWeight +
            ContainersList[index].LoadWeight <= Destination.MaxContainerWeight &&
            Destination.ContainersList.Count < Destination.MaxContainerAmount)
        {
            Destination.AddContainer(ContainersList[index]);
            DeleteContainer(index);
        }
    }

    public override string ToString()
    {
        return
            $"Statek{Id} (speed={Speed}, maxContainerNum={maxContainerAmount},maxWeight={maxContainerWeight}, " +
            $"currentContainerWeight={CurrentContainerWeight}, currecontainerAmount={ContainersList.Count})";
    }
}