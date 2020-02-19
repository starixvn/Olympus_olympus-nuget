using Fanex.Data.Repository;

namespace TestWeb.Criteria
{
    public class JacobReturnOutputAndRandomTableCriteria : CriteriaBase
    {
        public int MyOutput { get; set; }
        public override string GetSettingKey() => "JacobReturnOutputAndRandomTable";
        public override bool IsValid() => true;
    }
}