namespace NimatorCouchBase.NimatorBooster.HttpCheckers.Callers
{
    public interface IHttpCallerParameters
    {
        string HttpUrl { get; }
        HttpAuthenticationSettings Authenticator { get; }

        HttpMethods Method { get; }
    }
}