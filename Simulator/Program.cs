var mainSystem = new MainSystem();

const int numberOfMachines = 3;
var productionSite = new ProductionSite(numberOfMachines, mainSystem);

mainSystem.AddProductionSite(productionSite);
mainSystem.StartSimulation();