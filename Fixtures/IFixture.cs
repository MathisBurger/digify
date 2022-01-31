namespace digify.Fixtures;

public interface IFixture
{
    Task Load();
    Task<bool> NotExists();
}