public interface ICoffeeMachineState
{
    void InsertCoin(CoffeeMachine machine);
    void SelectDrink(CoffeeMachine machine, string drink);
    void DispenseDrink(CoffeeMachine machine);
    string GetState();
}

public class WaitingForCoinState : ICoffeeMachineState
{
    public void InsertCoin(CoffeeMachine machine)
    {
        Console.WriteLine("Coin inserted. Selecting drink...");
        machine.SetState(new SelectingDrinkState());
    }
    public void SelectDrink(CoffeeMachine machine, string drink) { }
    public void DispenseDrink(CoffeeMachine machine) { }
    public string GetState() => "Ожидание монеты";
}

public class SelectingDrinkState : ICoffeeMachineState
{
    public void InsertCoin(CoffeeMachine machine) { }
    public void SelectDrink(CoffeeMachine machine, string drink)
    {
        Console.WriteLine($"{drink} selected. Dispensing...");
        machine.SetState(new DispensingDrinkState());
    }
    public void DispenseDrink(CoffeeMachine machine) { }
    public string GetState() => "Выбор напитка";
}

public class DispensingDrinkState : ICoffeeMachineState
{
    public void InsertCoin(CoffeeMachine machine) { }
    public void SelectDrink(CoffeeMachine machine, string drink) { }
    public void DispenseDrink(CoffeeMachine machine)
    {
        Console.WriteLine("Drink dispensed. Ready for next customer.");
        Thread.Sleep(2000); 
        machine.SetState(new WaitingForCoinState());
    }
    public string GetState() => "Выдача напитка";
}

public class CoffeeMachine
{
    private ICoffeeMachineState state;

    public CoffeeMachine()
    {
        state = new WaitingForCoinState();
    }

    public void SetState(ICoffeeMachineState newState)
    {
        state = newState;
    }

    public void InsertCoin()
    {
        state.InsertCoin(this);
    }

    public void SelectDrink(string drink)
    {
        state.SelectDrink(this, drink);
    }

    public void DispenseDrink()
    {
        state.DispenseDrink(this);
    }

    public string GetCurrentState()
    {
        return state.GetState();
    }
}


public class Example
{
    public static void Main(string[] args)
    {
        CoffeeMachine machine = new CoffeeMachine();
        Console.WriteLine($"Автомат в состоянии: {machine.GetCurrentState()}");

        machine.InsertCoin();
        Console.WriteLine($"Автомат в состоянии: {machine.GetCurrentState()}");

        machine.SelectDrink("Espresso");
        Console.WriteLine($"Автомат в состоянии: {machine.GetCurrentState()}");

        machine.DispenseDrink();
        Console.WriteLine($"Автомат в состоянии: {machine.GetCurrentState()}");

    }
}
