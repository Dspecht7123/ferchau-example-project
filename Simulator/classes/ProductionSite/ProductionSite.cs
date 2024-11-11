public class ProductionSite
{
    private List<Machine> Machines;
    const int MachineNotFoundPollingLoopDelayInSeconds = 2;
    private MainSystem MainSystem;

    public ProductionSite(List<Machine> machines, MainSystem mainSystem)
    {
        this.Machines = machines;
        this.MainSystem = mainSystem;

        //initialize polling loop which looks for finished mashines
        this.MoveObjectFromFinishedMachineToFullPlace();
    }

    public void onEmptyPlaceSensorChanged()
    {
        if (this.MainSystem.EmptyPlaceSensor) //getEmptyPlaceSensor()
        {
            this.MoveObjectFromEmptyPlaceToIdleMachine();
        }
    }

    public void onFullPlaceSensorChanged()
    {
        if (!this.MainSystem.FullPlaceSensor) //!getFullPlaceSensor()
        {
            this.MoveObjectFromFinishedMachineToFullPlace();
        }
    }

    private async void MoveObjectFromEmptyPlaceToIdleMachine()
    {
        bool idleMachineFound = false;
        while (!idleMachineFound)
        {
            var idleMachine = this.Machines.Find(machine => machine.MachineState == MachineStates.Idle);

            if (idleMachine == null)
            {
                await Task.Delay(GetMachineNotFoundTimeSpan());
                continue;
            }
            else
            {
                idleMachineFound = true;

                this.MainSystem.EmptyPlaceSensor = false;//setEmptyPlaceSensor(false);
                Console.WriteLine($"Empty Object moved from Empty Place to Machine with Id {idleMachine.Id}");

                idleMachine.Start();
            }
        }
    }

    private async void MoveObjectFromFinishedMachineToFullPlace()
    {
        bool finishedMachineFound = false;
        while (!finishedMachineFound)
        {
            var finishedMachine = this.Machines.Find(machine => machine.MachineState == MachineStates.Finished);

            if (finishedMachine == null)
            {
                await Task.Delay(GetMachineNotFoundTimeSpan());
                continue;
            }
            else
            {
                finishedMachineFound = true;

                this.MainSystem.FullPlaceSensor = true;//setFullPlaceSensor(true);
                Console.WriteLine($"Full Object moved from Machine with Id {finishedMachine.Id} to Full Place");

                finishedMachine.Reset();
            }
        }
    }

    private TimeSpan GetMachineNotFoundTimeSpan()
    {
        return new TimeSpan(0, 0, MachineNotFoundPollingLoopDelayInSeconds);
    }
}