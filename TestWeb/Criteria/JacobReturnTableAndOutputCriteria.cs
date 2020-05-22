using Fanex.Data.Repository;

namespace TestWeb.Criteria
{
    public class JacobReturnTableAndOutputCriteria : CriteriaBase
    {
        public int MyOutput { get; set; }
        public override string GetSettingKey() => "JacobReturnTableAndOutput";
        public override bool IsValid() => true;
    }
}