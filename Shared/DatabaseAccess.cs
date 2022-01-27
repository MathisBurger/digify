namespace digify.Shared;

public class DatabaseAccess
{
    private readonly DatabaseContext ctx;

    public DatabaseAccess(DatabaseContext ctx)
    {
        this.ctx = ctx;
    }
}