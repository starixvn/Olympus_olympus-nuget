// -----------------------------------------------------------------------
// <copyright file="ParentLevelFiveColor.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Excel.Repositories.CustomColors
{
    using System.Diagnostics.CodeAnalysis;
    using NPOI.HSSF.Util;

    /// <summary>
    /// The ParentColor class.
    /// </summary>
    public class ParentLevelFiveColor : HSSFColor
    {
        public const string HexString = "";
        public const short Index = 41;
        private static byte[] _triplet = new byte[3] { 204, 255, 255 };

        public ParentLevelFiveColor()
            : base()
        {
        }

        public static byte[] Triplet
        {
            get { return ParentLevelFiveColor._triplet; }
            set { ParentLevelFiveColor._triplet = value; }
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
