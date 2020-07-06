using System;
using System.Collections.Generic;

public abstract class StateChange
{
    public abstract System.Type UnderlyingType {get;}

    private List<IPropagator> _completedObservationList = new List<IPropagator>();

    public void AddObservedBy(IPropagator propagator)
    {
        _completedObservationList.Add(propagator);
    }

    bool HasBeenObservedBy(IPropagator propagator)
    {
        return _completedObservationList.Contains(propagator);
    }
}

public class IntStateChange : StateChange
{
    public override Type UnderlyingType => typeof(IntStateChange);
    public int Payload;
}