namespace LilAsserter.Asserter
{
    public interface IAsserter
    {
        public AsserterService Assert(bool condition);
        public List<ErrorModel> GetErrorModels();
        public void SetContext();
        public void EndRequest(string? body = null);
    }
}
