public class MainSystem
{
    private bool _emptyPlaceSensor;
    public bool EmptyPlaceSensor
    {
        get
        {
            return _emptyPlaceSensor;
        }
        set
        {
            if (value)
            {
                Console.WriteLine($"Empty Place is filled");
            }
            else
            {
                Console.WriteLine($"Empty Place is empty");

            }

            _emptyPlaceSensor = value;

            if (this.ProductionSite != null)
            {
                this.ProductionSite.onEmptyPlaceSensorChanged();
            }
        }
    }

    private bool _fullPlaceSensor;
    public bool FullPlaceSensor
    {
        get
        {
            return _fullPlaceSensor;
        }
        set
        {
            if (value)
            {
                Console.WriteLine($"Full Place is filled");
            }
            else
            {
                Console.WriteLine($"Full Place is empty");

            }

            _fullPlaceSensor = value;

            if (this.ProductionSite != null)
            {
                this.ProductionSite.onFullPlaceSensorChanged();

            }
        }
    }
    public ProductionSite? ProductionSite;

    public MainSystem()
    {
        this.EmptyPlaceSensor = false;
        this.FullPlaceSensor = false;
    }

    public void AddProductionSite(ProductionSite productionSite)
    {
        this.ProductionSite = productionSite;
    }

    public void StartSimulation()
    {

        Thread emptyPlaceThread = new Thread(() =>
        {
            Random random = new Random();
            while (true)
            {
                int randomSeconds = random.Next(5);
                Thread.Sleep(new TimeSpan(0, 0, randomSeconds));

                if (this.ProductionSite != null && this.EmptyPlaceSensor == false)
                {
                    this.EmptyPlaceSensor = true;
                };
            }
        });

        emptyPlaceThread.Start();


        Thread fullPlaceThread = new Thread(() =>
        {
            Random random = new Random();
            while (true)
            {
                int randomSeconds = random.Next(5);
                Thread.Sleep(new TimeSpan(0, 0, randomSeconds));

                if (this.ProductionSite != null && this.FullPlaceSensor == true)
                {
                    this.FullPlaceSensor = false;
                };
            }
        });

        fullPlaceThread.Start();

        emptyPlaceThread.Join();
        fullPlaceThread.Join();

    }
}