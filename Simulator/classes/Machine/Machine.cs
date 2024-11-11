public class Machine
{
    private int _id;
    public int Id {get; private set;}
    private TimeSpan CycleTime;
    public MachineStates MachineState { get; private set; }

    public Machine(int id, TimeSpan cycleTime)
    {
        this.Id = id;
        this.CycleTime = cycleTime;
        this.MachineState = MachineStates.Idle;
    }

    public async void Start()
    {
        this.MachineState = MachineStates.Busy;
        Console.WriteLine($"Machine with Id {this.Id} switched to state Busy");

        await Task.Delay(this.CycleTime); //maybe use startTimer of API

        this.MachineState = MachineStates.Finished;
        Console.WriteLine($"Machine with Id {this.Id} switched to state Finished");
    }

    public void Reset()
    {
        this.MachineState = MachineStates.Idle;
        Console.WriteLine($"Machine with Id {this.Id} switched to state Idle");
    }

}