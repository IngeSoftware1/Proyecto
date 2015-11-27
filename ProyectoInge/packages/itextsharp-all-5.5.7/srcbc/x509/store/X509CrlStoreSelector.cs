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
using Org.BouncyCastle.Utilities.Date;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.X509.Extension;

namespace Org.BouncyCastle.X509.Store
{
	public class X509CrlStoreSelector
		: IX509Selector
	{
		// TODO Missing criteria?

		private X509Certificate certificateChecking;
		private DateTimeObject dateAndTime;
		private ICollection issuers;
		private BigInteger maxCrlNumber;
		private BigInteger minCrlNumber;

		private IX509AttributeCertificate attrCertChecking;
		private bool completeCrlEnabled;
		private bool deltaCrlIndicatorEnabled;
		private byte[] issuingDistributionPoint;
		private bool issuingDistributionPointEnabled;
		private BigInteger maxBaseCrlNumber;

		public X509CrlStoreSelector()
		{
		}

		public X509CrlStoreSelector(
			X509CrlStoreSelector o)
		{
			this.certificateChecking = o.CertificateChecking;
			this.dateAndTime = o.DateAndTime;
			this.issuers = o.Issuers;
			this.maxCrlNumber = o.MaxCrlNumber;
			this.minCrlNumber = o.MinCrlNumber;

			this.deltaCrlIndicatorEnabled = o.DeltaCrlIndicatorEnabled;
			this.completeCrlEnabled = o.CompleteCrlEnabled;
			this.maxBaseCrlNumber = o.MaxBaseCrlNumber;
			this.attrCertChecking = o.AttrCertChecking;
			this.issuingDistributionPointEnabled = o.IssuingDistributionPointEnabled;
			this.issuingDistributionPoint = o.IssuingDistributionPoint;
		}

		public virtual object Clone()
		{
			return new X509CrlStoreSelector(this);
		}

		public X509Certificate CertificateChecking
		{
			get { return certificateChecking; }
			set { certificateChecking = value; }
		}

		public DateTimeObject DateAndTime
		{
			get { return dateAndTime; }
			set { dateAndTime = value; }
		}

		/// <summary>
		/// An <code>ICollection</code> of <code>X509Name</code> objects
		/// </summary>
		public ICollection Issuers
		{
			get { return Platform.CreateArrayList(issuers); }
            set { issuers = Platform.CreateArrayList(value); }
		}

		public BigInteger MaxCrlNumber
		{
			get { return maxCrlNumber; }
			set { maxCrlNumber = value; }
		}

		public BigInteger MinCrlNumber
		{
			get { return minCrlNumber; }
			set { minCrlNumber = value; }
		}

		/**
		 * The attribute certificate being checked. This is not a criterion.
		 * Rather, it is optional information that may help a {@link X509Store} find
		 * CRLs that would be relevant when checking revocation for the specified
		 * attribute certificate. If <code>null</code> is specified, then no such
		 * optional information is provided.
		 *
		 * @param attrCert the <code>IX509AttributeCertificate</code> being checked (or
		 *             <code>null</code>)
		 * @see #getAttrCertificateChecking()
		 */
		public IX509AttributeCertificate AttrCertChecking
		{
			get { return attrCertChecking; }
			set { this.attrCertChecking = value; }
		}

		/**
		 * If <code>true</code> only complete CRLs are returned. Defaults to
		 * <code>false</code>.
		 *
		 * @return <code>true</code> if only complete CRLs are returned.
		 */
		public bool CompleteCrlEnabled
		{
			get { return completeCrlEnabled; }
			set { this.completeCrlEnabled = value; }
		}

		/**
		 * Returns if this selector must match CRLs with the delta CRL indicator
		 * extension set. Defaults to <code>false</code>.
		 *
		 * @return Returns <code>true</code> if only CRLs with the delta CRL
		 *         indicator extension are selected.
		 */
		public bool DeltaCrlIndicatorEnabled
		{
			get { return deltaCrlIndicatorEnabled; }
			set { this.deltaCrlIndicatorEnabled = value; }
		}

		/**
		 * The issuing distribution point.
		 * <p>
		 * The issuing distribution point extension is a CRL extension which
		 * identifies the scope and the distribution point of a CRL. The scope
		 * contains among others information about revocation reasons contained in
		 * the CRL. Delta CRLs and complete CRLs must have matching issuing
		 * distribution points.</p>
		 * <p>
		 * The byte array is cloned to protect against subsequent modifications.</p>
		 * <p>
		 * You must also enable or disable this criteria with
		 * {@link #setIssuingDistributionPointEnabled(bool)}.</p>
		 *
		 * @param issuingDistributionPoint The issuing distribution point to set.
		 *                                 This is the DER encoded OCTET STRING extension value.
		 * @see #getIssuingDistributionPoint()
		 */
		public byte[] IssuingDistributionPoint
		{
			get { return Arrays.Clone(issuingDistributionPoint); }
			set { this.issuingDistributionPoint = Arrays.Clone(value); }
		}

		/**
		 * Whether the issuing distribution point criteria should be applied.
		 * Defaults to <code>false</code>.
		 * <p>
		 * You may also set the issuing distribution point criteria if not a missing
		 * issuing distribution point should be assumed.</p>
		 *
		 * @return Returns if the issuing distribution point check is enabled.
		 */
		public bool IssuingDistributionPointEnabled
		{
			get { return issuingDistributionPointEnabled; }
			set { this.issuingDistributionPointEnabled = value; }
		}

		/**
		 * The maximum base CRL number. Defaults to <code>null</code>.
		 *
		 * @return Returns the maximum base CRL number.
		 * @see #setMaxBaseCRLNumber(BigInteger)
		 */
		public BigInteger MaxBaseCrlNumber
		{
			get { return maxBaseCrlNumber; }
			set { this.maxBaseCrlNumber = value; }
		}

		public virtual bool Match(
			object obj)
		{
			X509Crl c = obj as X509Crl;

			if (c == null)
				return false;

			if (dateAndTime != null)
			{
				DateTime dt = dateAndTime.Value;
				DateTime tu = c.ThisUpdate;
				DateTimeObject nu = c.NextUpdate;

				if (dt.CompareTo(tu) < 0 || nu == null || dt.CompareTo(nu.Value) >= 0)
					return false;
			}

			if (issuers != null)
			{
				X509Name i = c.IssuerDN;

				bool found = false;

				foreach (X509Name issuer in issuers)
				{
					if (issuer.Equivalent(i, true))
					{
						found = true;
						break;
					}
				}

				if (!found)
					return false;
			}

			if (maxCrlNumber != null || minCrlNumber != null)
			{
				Asn1OctetString extVal = c.GetExtensionValue(X509Extensions.CrlNumber);
				if (extVal == null)
					return false;

				BigInteger cn = CrlNumber.GetInstance(
					X509ExtensionUtilities.FromExtensionValue(extVal)).PositiveValue;

				if (maxCrlNumber != null && cn.CompareTo(maxCrlNumber) > 0)
					return false;

				if (minCrlNumber != null && cn.CompareTo(minCrlNumber) < 0)
					return false;
			}

			DerInteger dci = null;
			try
			{
				Asn1OctetString bytes = c.GetExtensionValue(X509Extensions.DeltaCrlIndicator);
				if (bytes != null)
				{
					dci = DerInteger.GetInstance(X509ExtensionUtilities.FromExtensionValue(bytes));
				}
			}
			catch (Exception)
			{
				return false;
			}

			if (dci == null)
			{
				if (DeltaCrlIndicatorEnabled)
					return false;
			}
			else
			{
				if (CompleteCrlEnabled)
					return false;

				if (maxBaseCrlNumber != null && dci.PositiveValue.CompareTo(maxBaseCrlNumber) > 0)
					return false;
			}

			if (issuingDistributionPointEnabled)
			{
				Asn1OctetString idp = c.GetExtensionValue(X509Extensions.IssuingDistributionPoint);
				if (issuingDistributionPoint == null)
				{
					if (idp != null)
						return false;
				}
				else
				{
					if (!Arrays.AreEqual(idp.GetOctets(), issuingDistributionPoint))
						return false;
				}
			}

			return true;
		}
	}
}