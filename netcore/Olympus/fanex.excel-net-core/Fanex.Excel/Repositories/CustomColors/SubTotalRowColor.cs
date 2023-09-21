// -----------------------------------------------------------------------
// <copyright file="SubTotalRowColor.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Excel.Repositories.CustomColors
{
    using System.Diagnostics.CodeAnalysis;
    using NPOI.HSSF.Util;

    /// <summary>
    /// The SubTotalRowColor class.
    /// </summary>
    public class SubTotalRowColor : HSSFColor
    {
        public const string HexString = "";
        public const short Index = 33;
        private static byte[] _triplet = new byte[3] { 244, 244, 190 };

        public SubTotalRowColor()
            : base()
        {
        }

        public static byte[] Triplet
        {
            get { return SubTotalRowColor._triplet; }
            set { SubTotalRowColor._triplet = value; }
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
