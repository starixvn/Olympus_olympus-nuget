namespace TestAspNetCore;

using Fanex.Data.Repository;
using Olympus.SimpleDataApi;

public class GetLastUpdatedPreMatchAssignmentCriteria : CriteriaBase
{
    public bool IsDay { get; set; }

    public override string GetSettingKey() => "OWP_PreMatchAssignment_GetLastUpdated";

    public override bool IsValid() => true;
}

public class SimpleCriteriaFactoryImp: SimpleCriteriaFactoryBase
{
    static SimpleCriteriaFactoryImp()
    {
        CriteriaTypes.Add("pre-match-assignment-get-last-updated", typeof(GetLastUpdatedPreMatchAssignmentCriteria));
    }
}

public class MyData
{
    public string Name { get; set; }

    public List<int> Ids { get; set; }
}