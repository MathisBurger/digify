using digify.Shared;

namespace digify.Fixtures;

/// <summary>
/// Scaffold of a basic database fixture.
/// </summary>
public interface IFixture
{
    Task Load();
    Task<bool> NotExists();
}