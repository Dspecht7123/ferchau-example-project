public class Machine
{
    private int _id;
    public int Id { get; private set; }
    private TimeSpan CycleTime;
    private ProductionSite? ProductionSite;
    public MachineStates MachineState { get; private set; }

    public Machine(int id, TimeSpan cycleTime, ProductionSite productionSite)
    {
        this.Id = id;
        this.CycleTime = cycleTime;
        this.ProductionSite = productionSite;
        this.MachineState = MachineStates.Idle;
    }

    public async void Start()
    {
        this.MachineState = MachineStates.Busy;
        Console.WriteLine($"Machine with Id {this.Id} switched to state Busy");

        await Task.Delay(this.CycleTime); //maybe use startTimer of API

        this.MachineState = MachineStates.Finished;
        Console.WriteLine($"Machine with Id {this.Id} switched to state Finished");

        this.ProductionSite?.onMachineStateChangedToFinished();
    }

    public void Reset()
    {
        this.MachineState = MachineStates.Idle;
        Console.WriteLine($"Machine with Id {this.Id} switched to state Idle");

        this.ProductionSite?.onMachineStateChangedToIdle();
    }
}