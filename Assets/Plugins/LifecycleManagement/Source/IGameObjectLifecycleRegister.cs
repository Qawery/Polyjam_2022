namespace Lifecycle
{
	public interface IGameObjectLifecycleRegister
	{
		void RegisterInitializationInterface<InterfaceType>(System.Action<InterfaceType> initMethod);
		void RegisterDestructionInterface<InterfaceType>(System.Action<InterfaceType> initMethod);
	}
}