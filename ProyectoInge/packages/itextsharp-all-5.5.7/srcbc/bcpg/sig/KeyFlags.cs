/*
 * $Id$
 *
 * This file is part of the iText (R) project.
 * Copyright (c) 1998-2015 iText Group NV
 * Authors: Bruno Lowagie, Paulo Soares, et al.
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License version 3
 * as published by the Free Software Foundation with the addition of the
 * following permission added to Section 15 as permitted in Section 7(a):
 * FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
 * ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
 * OF THIRD PARTY RIGHTS
 *
 * This program is distributed in the hope that it will be useful, but
 * WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
 * or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU Affero General Public License for more details.
 * You should have received a copy of the GNU Affero General Public License
 * along with this program; if not, see http://www.gnu.org/licenses or write to
 * the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
 * Boston, MA, 02110-1301 USA, or download the license from the following URL:
 * http://itextpdf.com/terms-of-use/
 *
 * The interactive user interfaces in modified source and object code versions
 * of this program must display Appropriate Legal Notices, as required under
 * Section 5 of the GNU Affero General Public License.
 *
 * In accordance with Section 7(b) of the GNU Affero General Public License,
 * a covered work must retain the producer line in every PDF that is created
 * or manipulated using iText.
 *
 * You can be released from the requirements of the license by purchasing
 * a commercial license. Buying such a license is mandatory as soon as you
 * develop commercial activities involving the iText software without
 * disclosing the source code of your own applications.
 * These activities include: offering paid services to customers as an ASP,
 * serving PDFs on the fly in a web application, shipping iText with a closed
 * source product.
 *
 * For more information, please contact iText Software Corp. at this
 * address: sales@itextpdf.com
 */

using System;

using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Bcpg.Sig
{
    /**
    * Packet holding the key flag values.
    */
    public class KeyFlags
        : SignatureSubpacket
    {
		public const int CertifyOther	= 0x01;
		public const int SignData		= 0x02;
		public const int EncryptComms	= 0x04;
		public const int EncryptStorage	= 0x08;
		public const int Split			= 0x10;
		public const int Authentication	= 0x20;
		public const int Shared			= 0x80;

        private static byte[] IntToByteArray(
            int v)
        {
			byte[] tmp = new byte[4];
			int size = 0;

			for (int i = 0; i != 4; i++)
			{
				tmp[i] = (byte)(v >> (i * 8));
				if (tmp[i] != 0)
				{
					size = i;
				}
			}

			byte[] data = new byte[size + 1];
			Array.Copy(tmp, 0, data, 0, data.Length);
			return data;
		}

		public KeyFlags(
            bool	critical,
            byte[]	data)
            : base(SignatureSubpacketTag.KeyFlags, critical, data)
        {
        }

		public KeyFlags(
			bool	critical,
			int		flags)
            : base(SignatureSubpacketTag.KeyFlags, critical, IntToByteArray(flags))
        {
        }

		/// <summary>
		/// Return the flag values contained in the first 4 octets (note: at the moment
		/// the standard only uses the first one).
		/// </summary>
		public int Flags
        {
			get
			{
				int flags = 0;

				for (int i = 0; i != data.Length; i++)
				{
					flags |= (data[i] & 0xff) << (i * 8);
				}

				return flags;
			}
        }
    }
}