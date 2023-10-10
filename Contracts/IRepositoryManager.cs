namespace Contracts
{
    public interface IRepositoryManager
    {
        IUserRepository User { get; }
        IRoleRepository Role { get; }
        IUserIdentityRepository UserIdentity { get; }
        Task SaveAsync();
    }
}
