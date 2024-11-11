public class ProductionSite
{
    private List<Machine> Machines;
    private MainSystem MainSystem;

    public ProductionSite(int numberOfMachines, MainSystem mainSystem)
    {
        var machines = new List<Machine>();
        var cycleTime = new TimeSpan(0, 0, 30);

        for (var i = 0; i < numberOfMachines; i++)
        {
            machines.Add(new Machine(i, cycleTime, this));
        }

        this.Machines = machines;
        this.MainSystem = mainSystem;
    }

    public void onEmptyPlaceSensorChanged()
    {
        this.MoveObjectFromEmptyPlaceToIdleMachine();
    }

    public void onFullPlaceSensorChanged()
    {
        this.MoveObjectFromFinishedMachineToFullPlace();
    }

    public void onMachineStateChangedToIdle()
    {
        this.MoveObjectFromEmptyPlaceToIdleMachine();
    }

    public void onMachineStateChangedToFinished()
    {
        this.MoveObjectFromFinishedMachineToFullPlace();
    }

    private void MoveObjectFromEmptyPlaceToIdleMachine()
    {
        if (this.MainSystem.EmptyPlaceSensor) //getEmptyPlaceSensor()
        {
            var idleMachine = this.Machines.Find(machine => machine.MachineState == MachineStates.Idle);

            if (idleMachine != null)
            {
                this.MainSystem.EmptyPlaceSensor = false;//setEmptyPlaceSensor(false);
                Console.WriteLine($"Empty Object was moved from Empty Place to Machine with Id {idleMachine.Id}");

                idleMachine.Start();
            }
        }
    }

    private void MoveObjectFromFinishedMachineToFullPlace()
    {
        if (!this.MainSystem.FullPlaceSensor) //!getFullPlaceSensor()
        {
            var finishedMachine = this.Machines.Find(machine => machine.MachineState == MachineStates.Finished);

            if (finishedMachine != null)
            {
                this.MainSystem.FullPlaceSensor = true;//setFullPlaceSensor(true);
                Console.WriteLine($"Full Object was moved from Machine with Id {finishedMachine.Id} to Full Place");

                finishedMachine.Reset();
            }
        }
    }
}