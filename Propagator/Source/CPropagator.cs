using System.Collections.Generic;
using System.Linq;

public class CPropagator : IPropagator
{
    private List<IPropagator> _dependencyList = new List<IPropagator>();

    private Dictionary<System.Type, List<System.Action<StateChange>>> _handlerDict = new Dictionary<System.Type, List<System.Action<StateChange>>>(); 

    protected IPropagator AddHandler<U>(System.Action<StateChange> handler)
    {
        var type = typeof(U);
        if(!_handlerDict.ContainsKey(type))
        {
            _handlerDict[type] = new List<System.Action<StateChange>>();
        }

        if(!_handlerDict[type].Contains(handler))
        {
            _handlerDict[type].Add(handler);
        }

        return this;
    }

    public void Process(StateChange newState)
    {
        if(_handlerDict.ContainsKey(newState.UnderlyingType))
        {
            _handlerDict[newState.UnderlyingType].ForEach(x =>
            {
                x.Invoke(newState);
            });
        }
    }

    public void Process(StateChange newState, StateChangeOptions options)
    {
        if((options & StateChangeOptions.Update) != 0)
            Process(newState);
        
        if((options & StateChangeOptions.Notify) != 0)
        {
            _dependencyList.ForEach(x => x.Process(newState, StateChangeOptions.UpdateAndNotify, this));
        }
    }

    public void Process(StateChange newState, StateChangeOptions options, IPropagator sender)
    {
        System.Console.WriteLine(options);
        if((options & StateChangeOptions.Update) != 0)
            Process(newState);

        if((options & StateChangeOptions.Notify) != 0)
        {
            _dependencyList.ForEach(x => 
            {
                if(x != sender)
                {
                    x.Process(newState, options, this);
                }
            });
        }
    }

    public void RemoveAllDependents(bool biDirectional)
    {
        if(biDirectional)
        {
            _dependencyList.ForEach(x =>
            {
                x.RemoveDependent(this, false);
            });
        }

        _dependencyList.Clear();
    }

    public IPropagator AddDependent(IPropagator dependent, bool biDirectional)
    {
        if(!_dependencyList.Contains(dependent))
        {
            _dependencyList.Add(dependent);
        }

        if(biDirectional)
        {
            dependent.AddDependent(this, false);
        }

        return this;
    }

    public IPropagator RemoveDependent(IPropagator dependent, bool biDirectional)
    {
        if(_dependencyList.Contains(dependent))
        {
            _dependencyList.Remove(dependent);
            
            if(biDirectional)
            {
                dependent.RemoveDependent(this, false);
            }
        }

        return this;
    }
}