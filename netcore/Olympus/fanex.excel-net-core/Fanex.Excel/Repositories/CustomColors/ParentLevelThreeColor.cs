
namespace Fanex.Excel.Repositories.CustomColors
{
    using NPOI.HSSF.Util;

    public class ParentLevelThreeColor : HSSFColor
    {
        public const string HexString = "";
        public const short Index = 38;
        private static byte[] _triplet = new byte[3] { 235, 253, 253 };

        public ParentLevelThreeColor()
            : base()
        {
        }

        public static byte[] Triplet
        {
            get { return ParentLevelThreeColor._triplet; }
            set { ParentLevelThreeColor._triplet = value; }
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
