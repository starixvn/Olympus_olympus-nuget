
namespace Fanex.Excel.Repositories.CustomColors
{
    using System.Diagnostics.CodeAnalysis;
    using NPOI.HSSF.Util;

    public class ParentLevelFourColor : HSSFColor
    {
        public const string HexString = "";
        public const short Index = 41;
        private static byte[] _triplet = new byte[3] { 204, 255, 255 };

        public ParentLevelFourColor()
            : base()
        {
        }

        public static byte[] Triplet
        {
            get { return ParentLevelFourColor._triplet; }
            set { ParentLevelFourColor._triplet = value; }
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
