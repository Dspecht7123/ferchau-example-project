var mainSystem = new MainSystem();

var cycleTime = new TimeSpan(0, 0, 10);
var machine1 = new Machine(1, cycleTime);
var machine2 = new Machine(2, cycleTime);
var machine3 = new Machine(3, cycleTime);

var machines = new List<Machine>();
machines.Add(machine1);
machines.Add(machine2);
machines.Add(machine3);

var productionSite = new ProductionSite(machines, mainSystem);

mainSystem.AddProductionSite(productionSite);
mainSystem.StartSimulation();