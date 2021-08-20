namespace Olympus.OWP.InfoSiteClient
{
    using System;
    using System.Collections.Generic;

    public class IsLiveSynchronizedResult
    {
        public List<(int, int?)> FailedMatches { get; set; }

        public List<(int, int?)> SuccessfulMatches { get; set; }

        public string ErrorMessage { get; set; }
    }
}