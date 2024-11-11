public class ProductionSite
{
    private List<Machine> Machines;
    const int MachineNotFoundPollingLoopDelayInSeconds = 2;


    public ProductionSite(List<Machine> machines)
    {
        this.Machines = machines;
    }

    public void onEmptyPlaceSensorChanged()
    {
        if (getEmptyPlaceSensor())
        {
            this.MoveObjectFromEmptyPlaceToIdleMachine();
        }
    }

    public void onFullPlaceSensorChanged()
    {
        if (!getFullPlaceSensor())
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
                setEmptyPlaceSensor(false);
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
                setFullPlaceSensor(true);
                finishedMachine.Reset();
            }
        }
    }

    private TimeSpan GetMachineNotFoundTimeSpan()
    {
        return new TimeSpan(0, 0, MachineNotFoundPollingLoopDelayInSeconds);
    }
}