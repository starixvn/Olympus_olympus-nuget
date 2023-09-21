
namespace Fanex.Excel.Repositories.CustomColors
{
    using System.Diagnostics.CodeAnalysis;
    using NPOI.HSSF.Util;

    public class TitleColor : HSSFColor
    {
        public const string HexString = "";
        public const short Index = 37;
        private static byte[] _triplet = new byte[3] { 60, 94, 165 };

        public TitleColor()
            : base()
        {
        }

        public static byte[] Triplet
        {
            get { return TitleColor._triplet; }
            set { TitleColor._triplet = value; }
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
