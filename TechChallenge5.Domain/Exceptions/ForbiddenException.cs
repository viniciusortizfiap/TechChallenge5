namespace TechChallenge5.Domain.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string? message) : base(message)
        {
        }
    }
}
