namespace MyUnitTests
{
    using NUnit.Framework;
    using Olympus.HttpLogger;

    public class RegexUtilityTests
    {
        [Test]

        [TestCase("mydata=31232&password=lhdsadsadasdsd&username=aaaa", ExpectedResult = "mydata=31232&**** sensitive password ****&username=aaaa")]
        [TestCase("mydata=31232&userpassword=lhdsadsadasdsd&username=aaaa", ExpectedResult = "mydata=31232&**** sensitive password ****&username=aaaa")]
        [TestCase("mydata=31232&Userpassword=lhdsadsadasdsd&username=aaaa", ExpectedResult = "mydata=31232&**** sensitive password ****&username=aaaa")]
        [TestCase("mydata=31232&password = lhdsadsadasdsd &username=aaaa", ExpectedResult = "mydata=31232&**** sensitive password ****&username=aaaa")]
        [TestCase("\"mydata\":31232,\"password\":\"lhdsadsadasdsd\",\"username\"=\"aaaa\"", ExpectedResult = "\"mydata\":31232,**** sensitive password ****,\"username\"=\"aaaa\"")]
        [TestCase("mydata:31232,password:lhdsadsadasdsd,username=aaaa", ExpectedResult = "mydata:31232,**** sensitive password ****,username=aaaa")]
        [TestCase("mydata:31232 , password:lhdsadsadasdsd,username=aaaa", ExpectedResult = "mydata:31232 ,**** sensitive password ****,username=aaaa")]
        public string ReplaceSensitiveTextTests(string input)
        {
            return RegexUtility.ReplaceSensitiveText(input);
        }
    }
}
