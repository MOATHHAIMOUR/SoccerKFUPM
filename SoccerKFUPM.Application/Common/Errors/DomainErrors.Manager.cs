using SoccerKFUPM.Application.Common.ResultPattern;

namespace SoccerKFUPM.Application.Common.Errors;

public partial class DomainErrors
{
    public static class Manager
    {
        public static Error NotFound => new(
            "Manager.NotFound",
            "The specified manager was not found.");

        public static Error FailedToAdd => new(
            "Manager.FailedToAdd",
            "Failed to add the manager.");

        public static Error FailedToGet => new(
            "Manager.FailedToGet",
            "Failed to retrieve the manager.");
    }
}