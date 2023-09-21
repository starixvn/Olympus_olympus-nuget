
namespace Fanex.Excel.Repositories.CustomColors
{
    using System.Diagnostics.CodeAnalysis;
    using NPOI.HSSF.Util;

    public class NoteInformationColor : HSSFColor
    {
        public const string HexString = "";
        public const short Index = 39;
        private static byte[] _triplet = new byte[3] { 181, 196, 208 };

        public NoteInformationColor()
            : base()
        {
        }

        public static byte[] Triplet
        {
            get { return NoteInformationColor._triplet; }
            set { NoteInformationColor._triplet = value; }
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
