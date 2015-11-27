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
using System.Collections;

using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;
using Org.BouncyCastle.Utilities.Date;
using Org.BouncyCastle.X509.Extension;

namespace Org.BouncyCastle.X509.Store
{
	public class X509CertStoreSelector
		: IX509Selector
	{
		// TODO Missing criteria?

		private byte[] authorityKeyIdentifier;
		private int basicConstraints = -1;
		private X509Certificate certificate;
		private DateTimeObject certificateValid;
		private ISet extendedKeyUsage;
		private X509Name issuer;
		private bool[] keyUsage;
		private ISet policy;
		private DateTimeObject privateKeyValid;
		private BigInteger serialNumber;
		private X509Name subject;
		private byte[] subjectKeyIdentifier;
		private SubjectPublicKeyInfo subjectPublicKey;
		private DerObjectIdentifier subjectPublicKeyAlgID;

		public X509CertStoreSelector()
		{
		}

		public X509CertStoreSelector(
			X509CertStoreSelector o)
		{
			this.authorityKeyIdentifier = o.AuthorityKeyIdentifier;
			this.basicConstraints = o.BasicConstraints;
			this.certificate = o.Certificate;
			this.certificateValid = o.CertificateValid;
			this.extendedKeyUsage = o.ExtendedKeyUsage;
			this.issuer = o.Issuer;
			this.keyUsage = o.KeyUsage;
			this.policy = o.Policy;
			this.privateKeyValid = o.PrivateKeyValid;
			this.serialNumber = o.SerialNumber;
			this.subject = o.Subject;
			this.subjectKeyIdentifier = o.SubjectKeyIdentifier;
			this.subjectPublicKey = o.SubjectPublicKey;
			this.subjectPublicKeyAlgID = o.SubjectPublicKeyAlgID;
		}

		public virtual object Clone()
		{
			return new X509CertStoreSelector(this);
		}

		public byte[] AuthorityKeyIdentifier
		{
			get { return Arrays.Clone(authorityKeyIdentifier); }
			set { authorityKeyIdentifier = Arrays.Clone(value); }
		}

		public int BasicConstraints
		{
			get { return basicConstraints; }
			set
			{
				if (value < -2)
					throw new ArgumentException("value can't be less than -2", "value");

				basicConstraints = value;
			}
		}

		public X509Certificate Certificate
		{
			get { return certificate; }
			set { this.certificate = value; }
		}

		public DateTimeObject CertificateValid
		{
			get { return certificateValid; }
			set { certificateValid = value; }
		}

		public ISet ExtendedKeyUsage
		{
			get { return CopySet(extendedKeyUsage); }
			set { extendedKeyUsage = CopySet(value); }
		}

		public X509Name Issuer
		{
			get { return issuer; }
			set { issuer = value; }
		}

		[Obsolete("Avoid working with X509Name objects in string form")]
		public string IssuerAsString
		{
			get { return issuer != null ? issuer.ToString() : null; }
		}

		public bool[] KeyUsage
		{
			get { return CopyBoolArray(keyUsage); }
			set { keyUsage = CopyBoolArray(value); }
		}

		/// <summary>
		/// An <code>ISet</code> of <code>DerObjectIdentifier</code> objects.
		/// </summary>
		public ISet Policy
		{
			get { return CopySet(policy); }
			set { policy = CopySet(value); }
		}

		public DateTimeObject PrivateKeyValid
		{
			get { return privateKeyValid; }
			set { privateKeyValid = value; }
		}

		public BigInteger SerialNumber
		{
			get { return serialNumber; }
			set { serialNumber = value; }
		}

		public X509Name Subject
		{
			get { return subject; }
			set { subject = value; }
		}

		public string SubjectAsString
		{
			get { return subject != null ? subject.ToString() : null; }
		}

		public byte[] SubjectKeyIdentifier
		{
			get { return Arrays.Clone(subjectKeyIdentifier); }
			set { subjectKeyIdentifier = Arrays.Clone(value); }
		}

		public SubjectPublicKeyInfo SubjectPublicKey
		{
			get { return subjectPublicKey; }
			set { subjectPublicKey = value; }
		}

		public DerObjectIdentifier SubjectPublicKeyAlgID
		{
			get { return subjectPublicKeyAlgID; }
			set { subjectPublicKeyAlgID = value; }
		}

		public virtual bool Match(
			object obj)
		{
			X509Certificate c = obj as X509Certificate;

			if (c == null)
				return false;

			if (!MatchExtension(authorityKeyIdentifier, c, X509Extensions.AuthorityKeyIdentifier))
				return false;

			if (basicConstraints != -1)
			{
				int bc = c.GetBasicConstraints();

				if (basicConstraints == -2)
				{
					if (bc != -1)
						return false;
				}
				else
				{
					if (bc < basicConstraints)
						return false;
				}
			}

			if (certificate != null && !certificate.Equals(c))
				return false;

			if (certificateValid != null && !c.IsValid(certificateValid.Value))
				return false;

			if (extendedKeyUsage != null)
			{
				IList eku = c.GetExtendedKeyUsage();

				// Note: if no extended key usage set, all key purposes are implicitly allowed

				if (eku != null)
				{
					foreach (DerObjectIdentifier oid in extendedKeyUsage)
					{
						if (!eku.Contains(oid.Id))
							return false;
					}
				}
			}

			if (issuer != null && !issuer.Equivalent(c.IssuerDN, true))
				return false;

			if (keyUsage != null)
			{
				bool[] ku = c.GetKeyUsage();

				// Note: if no key usage set, all key purposes are implicitly allowed

				if (ku != null)
				{
					for (int i = 0; i < 9; ++i)
					{
						if (keyUsage[i] && !ku[i])
							return false;
					}
				}
			}

			if (policy != null)
			{
				Asn1OctetString extVal = c.GetExtensionValue(X509Extensions.CertificatePolicies);
				if (extVal == null)
					return false;

				Asn1Sequence certPolicies = Asn1Sequence.GetInstance(
					X509ExtensionUtilities.FromExtensionValue(extVal));

				if (policy.Count < 1 && certPolicies.Count < 1)
					return false;

				bool found = false;
				foreach (PolicyInformation pi in certPolicies)
				{
					if (policy.Contains(pi.PolicyIdentifier))
					{
						found = true;
						break;
					}
				}

				if (!found)
					return false;
			}

			if (privateKeyValid != null)
			{
				Asn1OctetString extVal = c.GetExtensionValue(X509Extensions.PrivateKeyUsagePeriod);
				if (extVal == null)
					return false;

				PrivateKeyUsagePeriod pkup = PrivateKeyUsagePeriod.GetInstance(
					X509ExtensionUtilities.FromExtensionValue(extVal));

				DateTime dt = privateKeyValid.Value;
				DateTime notAfter = pkup.NotAfter.ToDateTime();
				DateTime notBefore = pkup.NotBefore.ToDateTime();

				if (dt.CompareTo(notAfter) > 0 || dt.CompareTo(notBefore) < 0)
					return false;
			}

			if (serialNumber != null && !serialNumber.Equals(c.SerialNumber))
				return false;

			if (subject != null && !subject.Equivalent(c.SubjectDN, true))
				return false;

			if (!MatchExtension(subjectKeyIdentifier, c, X509Extensions.SubjectKeyIdentifier))
				return false;

			if (subjectPublicKey != null && !subjectPublicKey.Equals(GetSubjectPublicKey(c)))
				return false;

			if (subjectPublicKeyAlgID != null
				&& !subjectPublicKeyAlgID.Equals(GetSubjectPublicKey(c).AlgorithmID))
				return false;

			return true;
		}

		internal static bool IssuersMatch(
			X509Name	a,
			X509Name	b)
		{
			return a == null ? b == null : a.Equivalent(b, true);
		}

		private static bool[] CopyBoolArray(
			bool[] b)
		{
			return b == null ? null : (bool[]) b.Clone();
		}

		private static ISet CopySet(
			ISet s)
		{
			return s == null ? null : new HashSet(s);
		}

		private static SubjectPublicKeyInfo GetSubjectPublicKey(
			X509Certificate c)
		{
			return SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(c.GetPublicKey());
		}

		private static bool MatchExtension(
			byte[]				b,
			X509Certificate		c,
			DerObjectIdentifier	oid)
		{
			if (b == null)
				return true;

			Asn1OctetString extVal = c.GetExtensionValue(oid);

			if (extVal == null)
				return false;

			return Arrays.AreEqual(b, extVal.GetOctets());
		}
	}
}