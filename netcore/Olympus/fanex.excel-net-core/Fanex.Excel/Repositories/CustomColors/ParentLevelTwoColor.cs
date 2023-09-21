
namespace Fanex.Excel.Repositories.CustomColors
{
    using NPOI.HSSF.Util;

    public class ParentLevelTwoColor : HSSFColor
    {
        public const string HexString = "";
        public const short Index = 35;
        private static byte[] _triplet = new byte[3] { 212, 237, 251 };

        public ParentLevelTwoColor()
            : base()
        {
        }

        public static byte[] Triplet
        {
            get { return ParentLevelTwoColor._triplet; }
            set { ParentLevelTwoColor._triplet = value; }
        }

        public override short Indexed
        {
            get
            {
                return Index;
            }

        }

        public override byte[] GetTriplet()
        {
            return _triplet;
        }
    }
}
