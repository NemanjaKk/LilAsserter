namespace LilAsserter.Asserter;
public interface IAsserter
{
    public IAsserter Assert(bool condition);
    public IAsserter AssertBreak(bool condition);
    public List<ErrorModel> GetErrorModels();
    public string GenerateErrorMessage();
}
