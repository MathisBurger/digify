namespace digify.Fixtures;

public interface IFixture
{
    void Load();
    Task<bool> NotExists();
}