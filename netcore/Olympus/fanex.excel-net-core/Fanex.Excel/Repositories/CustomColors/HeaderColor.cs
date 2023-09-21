// -----------------------------------------------------------------------
// <copyright file="HeaderColor.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Excel.Repositories.CustomColors
{
    using NPOI.HSSF.Util;

    /// <summary>
    /// Custom Color.
    /// </summary>
    public class HeaderColor : HSSFColor
    {
        public const string HexString = "";
        public const short Index = 27;
        private static byte[] _triplet = new byte[3] { 60, 94, 165 };

        public HeaderColor()
            : base()
        {
        }

        public static byte[] Triplet
        {
            get { return HeaderColor._triplet; }
            set { HeaderColor._triplet = value; }
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
