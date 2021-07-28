namespace Olympus.OWP.InfoSiteClient
{
    using System.Collections.Generic;

    public class IsLiveSynchronizedResult
    {
        public List<int> FailedMatches { get; set; }

        public List<int> SuccessfulMatches { get; set; }

        public string ErrorMessage { get; set; }
    }
}
