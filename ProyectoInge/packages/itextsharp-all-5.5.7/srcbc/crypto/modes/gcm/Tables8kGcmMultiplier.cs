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

using Org.BouncyCastle.Crypto.Utilities;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Modes.Gcm
{
    public class Tables8kGcmMultiplier
        : IGcmMultiplier
    {
        private byte[] H;
        private uint[][][] M;

        public void Init(byte[] H)
        {
            if (M == null)
            {
                M = new uint[32][][];
            }
            else if (Arrays.AreEqual(this.H, H))
            {
                return;
            }

            this.H = Arrays.Clone(H);

            M[0] = new uint[16][];
            M[1] = new uint[16][];
            M[0][0] = new uint[4];
            M[1][0] = new uint[4];
            M[1][8] = GcmUtilities.AsUints(H);

            for (int j = 4; j >= 1; j >>= 1)
            {
                uint[] tmp = (uint[])M[1][j + j].Clone();
                GcmUtilities.MultiplyP(tmp);
                M[1][j] = tmp;
            }

            {
                uint[] tmp = (uint[])M[1][1].Clone();
                GcmUtilities.MultiplyP(tmp);
                M[0][8] = tmp;
            }

            for (int j = 4; j >= 1; j >>= 1)
            {
                uint[] tmp = (uint[])M[0][j + j].Clone();
                GcmUtilities.MultiplyP(tmp);
                M[0][j] = tmp;
            }

            for (int i = 0; ; )
            {
                for (int j = 2; j < 16; j += j)
                {
                    for (int k = 1; k < j; ++k)
                    {
                        uint[] tmp = (uint[])M[i][j].Clone();
                        GcmUtilities.Xor(tmp, M[i][k]);
                        M[i][j + k] = tmp;
                    }
                }

                if (++i == 32) return;

                if (i > 1)
                {
                    M[i] = new uint[16][];
                    M[i][0] = new uint[4];
                    for (int j = 8; j > 0; j >>= 1)
                    {
                        uint[] tmp = (uint[])M[i - 2][j].Clone();
                        GcmUtilities.MultiplyP8(tmp);
                        M[i][j] = tmp;
                    }
                }
            }
        }

        public void MultiplyH(byte[] x)
        {
            uint[] z = new uint[4];
            for (int i = 15; i >= 0; --i)
            {
                //GcmUtilities.Xor(z, M[i + i][x[i] & 0x0f]);
                uint[] m = M[i + i][x[i] & 0x0f];
                z[0] ^= m[0];
                z[1] ^= m[1];
                z[2] ^= m[2];
                z[3] ^= m[3];
                //GcmUtilities.Xor(z, M[i + i + 1][(x[i] & 0xf0) >> 4]);
                m = M[i + i + 1][(x[i] & 0xf0) >> 4];
                z[0] ^= m[0];
                z[1] ^= m[1];
                z[2] ^= m[2];
                z[3] ^= m[3];
            }

            Pack.UInt32_To_BE(z, x, 0);
        }
    }
}
