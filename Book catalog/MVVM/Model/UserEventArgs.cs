namespace Book_catalog.MVVM.Model;

public class UserEventArgs
{
    public User User { get; }

    public UserEventArgs(User user)
    {
        User = user;
    }
}