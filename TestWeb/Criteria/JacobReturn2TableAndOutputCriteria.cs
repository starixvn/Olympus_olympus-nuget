namespace TestWeb.Criteria
{
    using Fanex.Data.Repository;

    public class JacobReturn2TableAndOutputCriteria : CriteriaBase
    {
        public int MyOutput { get; set; }
        public override string GetSettingKey() => "JacobReturn2TableAndOutput";
        public override bool IsValid() => true;
    }
}
