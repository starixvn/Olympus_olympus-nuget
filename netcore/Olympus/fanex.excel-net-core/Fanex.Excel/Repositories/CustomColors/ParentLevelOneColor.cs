
namespace Fanex.Excel.Repositories.CustomColors
{
    using NPOI.HSSF.Util;

    public class ParentLevelOneColor : HSSFColor
    {
        public const string HexString = "";
        public const short Index = 25;
        private static byte[] _triplet = new byte[3] { 194, 217, 236 };

        public ParentLevelOneColor()
            : base()
        {
        }

        public static byte[] Triplet
        {
            get { return ParentLevelOneColor._triplet; }
            set { ParentLevelOneColor._triplet = value; }
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
