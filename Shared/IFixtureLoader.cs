namespace digify.Shared;

public interface IFixtureLoader
{
    Task Load(IContext context);
}