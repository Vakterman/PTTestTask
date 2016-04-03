namespace FinbonacciAsyncLogic.Interfaces
{
    public interface IConfigurationManager
    {
        string QueueFibonacciName { get; }

        string ServiceUserName { get; }
        string ServicePasswordUser { get; }
        
        string QueueFibonacciAdress { get; }


        string WebApiServerAddress { get; }


        int DefaultWaitingTimeout { get; }
    }
}
