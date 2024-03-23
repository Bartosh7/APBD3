// See https://aka.ms/new-console-template for more information

using System.Data;
using APBD3;

class Program
{
    static List<Container> ContainersList = new List<Container>();
    static List<ContainerShip> ContainerShipsList = new List<ContainerShip>();

    static void Main(string[] args)
    {
        bool Running = true;
        while (Running)
        {
            if (ContainerShipsList.Count == 0)
            {
                EmptyShipsMenu();
            }
            else
            {
                ShowShipList();
                ShowContainerList();
                if (ContainersList.Count == 0)
                {
                    EmptyContainersMenu();
                }
                else
                {
                    ShowFullMenu();
                }
            }

            Console.WriteLine("=======================================");
        }
    }

    private static void ShowFullMenu()
    {
        ShowShipList();
        ShowContainerList();
        Console.WriteLine();

        Console.WriteLine("Możliwe akcje:");
        Console.WriteLine("1. Dodaj kontenerowiec");
        Console.WriteLine("2. Usun kontenerowiec");
        Console.WriteLine("3. Dodaj kontener");
        Console.WriteLine("4. Zarzadzaj kontenerem");

        Console.WriteLine("Proszę wprowadź numer komendy: ");
        string command = Console.ReadLine();
        if (command == "1")
        {
            AddingShipToList();
        }
        else if (command == "2")
        {
            DeletingShipFromList();
        }
        else if (command == "3")
        {
            AddingContainer();
        }
        else if (command == "4")
        {
            EditingContainer();
        }
    }

    private static void EditingContainer()
    {
        ShowContainerList();
        Console.WriteLine("Podaj indeks do edycji:");
        int index = Convert.ToInt32(Console.ReadLine());
        Container choosen = ContainersList[index - 1];
        if (choosen is LiquidContainer)
        {
            LiquidContainer container = (LiquidContainer)choosen;
            string command=EditingContainerMenu();
           
            if (command == "1")
            {
                Console.WriteLine("Podaj wage do dodania:");
                double weight = double.Parse(Console.ReadLine());
                Console.WriteLine("Czy to niebezpieczny towar(1-tak, 2-nie");
                string answer = Console.ReadLine();
                if (answer == "1")
                {
                    container.AddToContainer(weight, true);
                }else if (answer == "2")
                {
                    container.AddToContainer(weight, false);
                }

                ContainersList[index-1] = container;
            }
            else if (command == "2")
            {
                container.EmptyContainer();
                ContainersList[index-1] = container;
            }
            else if (command == "3")
            {
                ShowShipList();
                Console.WriteLine("Podaj indeks do statku:");
                int i = Convert.ToInt32(Console.ReadLine());
                ContainerShipsList[i-1].AddContainer(container);
                ContainersList.RemoveAt(i-1);
            }
            
        }else if (choosen is GasContainer)
        {
            GasContainer container = (GasContainer)choosen;
            string command=EditingContainerMenu();
           
            if (command == "1")
            {
                Console.WriteLine("Podaj wage do dodania:");
                double weight = double.Parse(Console.ReadLine());
                container.AddToContainer(weight);
                ContainersList[index-1] = container;

            }
            else if (command == "2")
            {
                container.EmptyContainer();
                ContainersList[index-1] = container;
            }
            else if (command == "3")
            {
                ShowShipList();
                Console.WriteLine("Podaj indeks do statku:");
                int i = Convert.ToInt32(Console.ReadLine());
                ContainerShipsList[i-1].AddContainer(container);
                ContainersList.RemoveAt(i-1);
            }
             
        }else if (choosen is CoolingContainer)
        {
            CoolingContainer container = (CoolingContainer)choosen;
            string command=EditingContainerMenu();
           
            if (command == "1")
            {
                Console.WriteLine("Podaj wage do dodania:");
                double weight = double.Parse(Console.ReadLine());
                container.AddToContainer(weight);
                ContainersList[index-1] = container;
                
                Console.WriteLine("Podaj typ produktu (wpisz odpowiednią liczbę):");
                foreach (var value in Enum.GetValues(typeof(Product)))
                {
                    Console.WriteLine($"{(int)value}: {value}");
                }

                int productTypeInput;
                bool isValidInput = false;
                do
                {
                    Console.Write("Wybierz typ produktu: ");
                    isValidInput = int.TryParse(Console.ReadLine(), out productTypeInput);

                    if (!isValidInput || !Enum.IsDefined(typeof(Product), productTypeInput))
                    {
                        Console.WriteLine("Błąd! Wprowadź prawidłową liczbę odpowiadającą typowi produktu.");
                    }
                } while (!isValidInput || !Enum.IsDefined(typeof(Product), productTypeInput));
                
                container.AddToContainer(weight, (Product)productTypeInput);
                ContainersList[index] = container;

            }
            else if (command == "2")
            {
                container.EmptyContainer();
                ContainersList[index-1] = container;
            }
            else if (command == "3")
            {
                ShowShipList();
                Console.WriteLine("Podaj indeks do statku:");
                int i = Convert.ToInt32(Console.ReadLine());
                ContainerShipsList[i-1].AddContainer(container);
                ContainersList.RemoveAt(i-1);
            }
        }
        
        
    }

    private static string EditingContainerMenu()
    {
        Console.WriteLine("Możliwe akcje:");
        Console.WriteLine("1. Dodaj zaladunek");
        Console.WriteLine("2. Usun zaladunek");
        Console.WriteLine("3. Dodaj na statek");
        string command = Console.ReadLine();

        return command;

    }

    private static void EmptyShipsMenu()
    {
        ShowShipList();
        ShowContainerList();
        Console.WriteLine();

        Console.WriteLine("Możliwe akcje:");
        Console.WriteLine("1. Dodaj kontenerowiec");

        Console.WriteLine("Proszę wprowadź numer komendy: ");
        string command = Console.ReadLine();

        if (command == "1")
        {
            AddingShipToList();
        }
    }

    private static void EmptyContainersMenu()
    {
        Console.WriteLine();

        Console.WriteLine("Możliwe akcje:");
        Console.WriteLine("1. Dodaj kontenerowiec");
        Console.WriteLine("2. Usun kontenerowiec");
        Console.WriteLine("3. Dodaj kontener");

        Console.WriteLine("Proszę wprowadź numer komendy: ");
        string command = Console.ReadLine();

        if (command == "1")
        {
            AddingShipToList();
        }
        else if (command == "2")
        {
            DeletingShipFromList();
        }
        else if (command == "3")
        {
            AddingContainer();
        }
    }

    private static void AddingContainer()
    {
        Console.WriteLine("Podaj wysokość kontenera:");
        double height = double.Parse(Console.ReadLine());

        Console.WriteLine("Podaj wagę kontenera:");
        double containerWeight = double.Parse(Console.ReadLine());

        Console.WriteLine("Podaj głębokość kontenera:");
        double depth = double.Parse(Console.ReadLine());

        Console.WriteLine("Podaj maksymalną wagę ładunku:");
        double maxLoadWeight = double.Parse(Console.ReadLine());


        Console.WriteLine("1. Na ciecz");
        Console.WriteLine("2. Na gas");
        Console.WriteLine("3. Chłodniczy");
        Console.WriteLine("Podaj typ kontenera, jaki chcesz utworzyc:");
        string command = Console.ReadLine();

        if (command == "1")
        {
            ContainersList.Add(new LiquidContainer(0, height, containerWeight, depth, maxLoadWeight));
        }
        else if (command == "2")
        {
            Console.WriteLine("Podaj cisnienie:");
            double pressure = double.Parse(Console.ReadLine());
            ContainersList.Add(new GasContainer(0, height, containerWeight, depth, maxLoadWeight, 'G',
                pressure));
        }
        else if (command == "3")
        {
            Console.WriteLine("Podaj typ produktu (wpisz odpowiednią liczbę):");
            foreach (var value in Enum.GetValues(typeof(Product)))
            {
                Console.WriteLine($"{(int)value}: {value}");
            }

            int productTypeInput;
            bool isValidInput = false;
            do
            {
                Console.Write("Wybierz typ produktu: ");
                isValidInput = int.TryParse(Console.ReadLine(), out productTypeInput);

                if (!isValidInput || !Enum.IsDefined(typeof(Product), productTypeInput))
                {
                    Console.WriteLine("Błąd! Wprowadź prawidłową liczbę odpowiadającą typowi produktu.");
                }
            } while (!isValidInput || !Enum.IsDefined(typeof(Product), productTypeInput));

            Console.WriteLine("Podaj temeprature srodka:");
            double temperatureInContainer = double.Parse(Console.ReadLine());


            ContainersList.Add(new CoolingContainer(0, height, containerWeight, depth, maxLoadWeight, 'C',
                (Product)productTypeInput, temperatureInContainer));
        }
    }

    private static void DeletingShipFromList()
    {
        ShowShipList();
        Console.WriteLine("Podaj indeks do usuniecia:");
        int index = Convert.ToInt32(Console.ReadLine());
        ContainerShipsList.RemoveAt(index - 1);
        Console.WriteLine("Statek usuniety");
    }

    private static void AddingShipToList()
    {
        Console.WriteLine("Podaj prędkość statku:");
        double speed = double.Parse(Console.ReadLine());

        Console.WriteLine("Podaj maksymalną ilość kontenerów:");
        int maxContainerAmount = int.Parse(Console.ReadLine());

        Console.WriteLine("Podaj maksymalną wagę kontenerów:");
        double maxContainerWeight = double.Parse(Console.ReadLine());

        ContainerShipsList.Add(new ContainerShip(speed, maxContainerAmount, maxContainerWeight));
        Console.WriteLine("Dodano statek");
    }

    private static void ShowShipList()
    {
        Console.WriteLine("Lista statkow:");
        for (int i = 0; i < ContainerShipsList.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {ContainerShipsList[i]}");
        }

        Console.WriteLine();
    }

    private static void ShowContainerList()
    {
        Console.WriteLine("Lista kontenerow:");
        for (int i = 0; i < ContainersList.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {ContainersList[i]}");
        }

        Console.WriteLine();
    }
}