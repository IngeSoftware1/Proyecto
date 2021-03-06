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

using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Ess
{
	[Obsolete("Use version in Asn1.Esf instead")]
	public class OtherCertID
		: Asn1Encodable
	{
		private Asn1Encodable otherCertHash;
		private IssuerSerial issuerSerial;

		public static OtherCertID GetInstance(
			object o)
		{
			if (o == null || o is OtherCertID)
			{
				return (OtherCertID) o;
			}

			if (o is Asn1Sequence)
			{
				return new OtherCertID((Asn1Sequence) o);
			}

			throw new ArgumentException(
				"unknown object in 'OtherCertID' factory : "
				+ o.GetType().Name + ".");
		}

		/**
		 * constructor
		 */
		public OtherCertID(
			Asn1Sequence seq)
		{
			if (seq.Count < 1 || seq.Count > 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}

			if (seq[0].ToAsn1Object() is Asn1OctetString)
			{
				otherCertHash = Asn1OctetString.GetInstance(seq[0]);
			}
			else
			{
				otherCertHash = DigestInfo.GetInstance(seq[0]);
			}

			if (seq.Count > 1)
			{
				issuerSerial = IssuerSerial.GetInstance(Asn1Sequence.GetInstance(seq[1]));
			}
		}

		public OtherCertID(
			AlgorithmIdentifier	algId,
			byte[]				digest)
		{
			this.otherCertHash = new DigestInfo(algId, digest);
		}

		public OtherCertID(
			AlgorithmIdentifier	algId,
			byte[]				digest,
			IssuerSerial		issuerSerial)
		{
			this.otherCertHash = new DigestInfo(algId, digest);
			this.issuerSerial = issuerSerial;
		}

		public AlgorithmIdentifier AlgorithmHash
		{
			get
			{
				if (otherCertHash.ToAsn1Object() is Asn1OctetString)
				{
					// SHA-1
					return new AlgorithmIdentifier("1.3.14.3.2.26");
				}

				return DigestInfo.GetInstance(otherCertHash).AlgorithmID;
			}
		}

		public byte[] GetCertHash()
		{
			if (otherCertHash.ToAsn1Object() is Asn1OctetString)
			{
				// SHA-1
				return ((Asn1OctetString) otherCertHash.ToAsn1Object()).GetOctets();
			}

			return DigestInfo.GetInstance(otherCertHash).GetDigest();
		}

		public IssuerSerial IssuerSerial
		{
			get { return issuerSerial; }
		}

		/**
		 * <pre>
		 * OtherCertID ::= SEQUENCE {
		 *     otherCertHash    OtherHash,
		 *     issuerSerial     IssuerSerial OPTIONAL }
		 *
		 * OtherHash ::= CHOICE {
		 *     sha1Hash     OCTET STRING,
		 *     otherHash    OtherHashAlgAndValue }
		 *
		 * OtherHashAlgAndValue ::= SEQUENCE {
		 *     hashAlgorithm    AlgorithmIdentifier,
		 *     hashValue        OCTET STRING }
		 *
		 * </pre>
		 */
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector v = new Asn1EncodableVector(otherCertHash);

			if (issuerSerial != null)
			{
				v.Add(issuerSerial);
			}

			return new DerSequence(v);
		}
	}
}
