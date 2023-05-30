namespace Infrastructure.Loading
{
    public interface ILoadingService
    {
        void RequestLoadingImmediately(params ILoading[] processors);
        void RequestLoading(params ILoading[] processors);
    }
}