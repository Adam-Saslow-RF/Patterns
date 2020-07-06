public interface IPropagator
{
    void Process(StateChange newState);
    void Process(StateChange newState, StateChangeOptions options);
    void Process(StateChange newState, StateChangeOptions options, IPropagator sender);

    IPropagator AddDependent(IPropagator dependent, bool biDirectional);
    IPropagator RemoveDependent(IPropagator dependent, bool biDirectional);
    void RemoveAllDependents(bool biDirectional);
}