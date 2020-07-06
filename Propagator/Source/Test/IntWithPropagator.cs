public class IntWithPropagator : CPropagator
{
    private int _value = 0;

    public IntWithPropagator(int startingVal)
    {
        AddHandler<IntStateChange>(i => 
        {
            int temp = _value;
            _value += ((IntStateChange)i).Payload;

            System.Console.WriteLine($"Handled state change. Was {temp}, now {_value}");
        });
        
        _value = startingVal;
    }

    public void Decrement()
    {
        _value--;
        StateChanged();
    }

    public void Increment()
    {
        _value++;
        StateChanged();
    }

    private void StateChanged()
    {
        int temp = _value;
        System.Console.WriteLine($"adjusted self: now {_value}");

        Process(new IntStateChange(){Payload = _value}, StateChangeOptions.Notify);
    }
}