
namespace Fanex.Excel.Repositories.CustomColors
{
    using System.Diagnostics.CodeAnalysis;
    using NPOI.HSSF.Util;

    public class EvenRowColor : HSSFColor
    {
        public const string HexString = "";
        public const short Index = 32;
        private static byte[] _triplet = new byte[3] { 228, 242, 253 };

        public EvenRowColor()
            : base()
        {
        }

        public static byte[] Triplet
        {
            get { return EvenRowColor._triplet; }
            set { EvenRowColor._triplet = value; }
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
