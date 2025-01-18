using GainsTracker.Core.Users.Interfaces.Repositories;
using GainsTracker.Core.Users.Models;

namespace GainsTracker.Data.Users;

public class UserRepository(GainsDbContextFactory contextFactory) : GenericRepository<User>(contextFactory), IUserRepository;
