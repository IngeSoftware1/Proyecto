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

using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Asn1.X509
{
    public class X509Extensions
        : Asn1Encodable
    {
		/**
		 * Subject Directory Attributes
		 */
		public static readonly DerObjectIdentifier SubjectDirectoryAttributes = new DerObjectIdentifier("2.5.29.9");

		/**
         * Subject Key Identifier
         */
        public static readonly DerObjectIdentifier SubjectKeyIdentifier = new DerObjectIdentifier("2.5.29.14");

		/**
         * Key Usage
         */
        public static readonly DerObjectIdentifier KeyUsage = new DerObjectIdentifier("2.5.29.15");

		/**
         * Private Key Usage Period
         */
        public static readonly DerObjectIdentifier PrivateKeyUsagePeriod = new DerObjectIdentifier("2.5.29.16");

		/**
         * Subject Alternative Name
         */
        public static readonly DerObjectIdentifier SubjectAlternativeName = new DerObjectIdentifier("2.5.29.17");

		/**
         * Issuer Alternative Name
         */
        public static readonly DerObjectIdentifier IssuerAlternativeName = new DerObjectIdentifier("2.5.29.18");

		/**
         * Basic Constraints
         */
        public static readonly DerObjectIdentifier BasicConstraints = new DerObjectIdentifier("2.5.29.19");

		/**
         * CRL Number
         */
        public static readonly DerObjectIdentifier CrlNumber = new DerObjectIdentifier("2.5.29.20");

		/**
         * Reason code
         */
        public static readonly DerObjectIdentifier ReasonCode = new DerObjectIdentifier("2.5.29.21");

		/**
         * Hold Instruction Code
         */
        public static readonly DerObjectIdentifier InstructionCode = new DerObjectIdentifier("2.5.29.23");

		/**
         * Invalidity Date
         */
        public static readonly DerObjectIdentifier InvalidityDate = new DerObjectIdentifier("2.5.29.24");

		/**
         * Delta CRL indicator
         */
        public static readonly DerObjectIdentifier DeltaCrlIndicator = new DerObjectIdentifier("2.5.29.27");

		/**
         * Issuing Distribution Point
         */
        public static readonly DerObjectIdentifier IssuingDistributionPoint = new DerObjectIdentifier("2.5.29.28");

		/**
         * Certificate Issuer
         */
        public static readonly DerObjectIdentifier CertificateIssuer = new DerObjectIdentifier("2.5.29.29");

		/**
         * Name Constraints
         */
        public static readonly DerObjectIdentifier NameConstraints = new DerObjectIdentifier("2.5.29.30");

		/**
         * CRL Distribution Points
         */
        public static readonly DerObjectIdentifier CrlDistributionPoints = new DerObjectIdentifier("2.5.29.31");

		/**
         * Certificate Policies
         */
        public static readonly DerObjectIdentifier CertificatePolicies = new DerObjectIdentifier("2.5.29.32");

		/**
         * Policy Mappings
         */
        public static readonly DerObjectIdentifier PolicyMappings = new DerObjectIdentifier("2.5.29.33");

		/**
         * Authority Key Identifier
         */
        public static readonly DerObjectIdentifier AuthorityKeyIdentifier = new DerObjectIdentifier("2.5.29.35");

		/**
         * Policy Constraints
         */
        public static readonly DerObjectIdentifier PolicyConstraints = new DerObjectIdentifier("2.5.29.36");

		/**
         * Extended Key Usage
         */
        public static readonly DerObjectIdentifier ExtendedKeyUsage = new DerObjectIdentifier("2.5.29.37");

		/**
		 * Freshest CRL
		 */
		public static readonly DerObjectIdentifier FreshestCrl = new DerObjectIdentifier("2.5.29.46");

		/**
         * Inhibit Any Policy
         */
        public static readonly DerObjectIdentifier InhibitAnyPolicy = new DerObjectIdentifier("2.5.29.54");

		/**
         * Authority Info Access
         */
		public static readonly DerObjectIdentifier AuthorityInfoAccess = new DerObjectIdentifier("1.3.6.1.5.5.7.1.1");

		/**
		 * Subject Info Access
		 */
		public static readonly DerObjectIdentifier SubjectInfoAccess = new DerObjectIdentifier("1.3.6.1.5.5.7.1.11");

		/**
		 * Logo Type
		 */
		public static readonly DerObjectIdentifier LogoType = new DerObjectIdentifier("1.3.6.1.5.5.7.1.12");

		/**
		 * BiometricInfo
		 */
		public static readonly DerObjectIdentifier BiometricInfo = new DerObjectIdentifier("1.3.6.1.5.5.7.1.2");

		/**
		 * QCStatements
		 */
		public static readonly DerObjectIdentifier QCStatements = new DerObjectIdentifier("1.3.6.1.5.5.7.1.3");

		/**
		 * Audit identity extension in attribute certificates.
		 */
		public static readonly DerObjectIdentifier AuditIdentity = new DerObjectIdentifier("1.3.6.1.5.5.7.1.4");

		/**
		 * NoRevAvail extension in attribute certificates.
		 */
		public static readonly DerObjectIdentifier NoRevAvail = new DerObjectIdentifier("2.5.29.56");

		/**
		 * TargetInformation extension in attribute certificates.
		 */
		public static readonly DerObjectIdentifier TargetInformation = new DerObjectIdentifier("2.5.29.55");

        private readonly IDictionary extensions = Platform.CreateHashtable();
        private readonly IList ordering;

		public static X509Extensions GetInstance(
            Asn1TaggedObject	obj,
            bool				explicitly)
        {
            return GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
        }

		public static X509Extensions GetInstance(
            object obj)
        {
            if (obj == null || obj is X509Extensions)
            {
                return (X509Extensions) obj;
            }

			if (obj is Asn1Sequence)
            {
                return new X509Extensions((Asn1Sequence) obj);
            }

			if (obj is Asn1TaggedObject)
            {
                return GetInstance(((Asn1TaggedObject) obj).GetObject());
            }

			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		/**
         * Constructor from Asn1Sequence.
         *
         * the extensions are a list of constructed sequences, either with (Oid, OctetString) or (Oid, Boolean, OctetString)
         */
        private X509Extensions(
            Asn1Sequence seq)
        {
            this.ordering = Platform.CreateArrayList();

			foreach (Asn1Encodable ae in seq)
			{
				Asn1Sequence s = Asn1Sequence.GetInstance(ae.ToAsn1Object());

				if (s.Count < 2 || s.Count > 3)
					throw new ArgumentException("Bad sequence size: " + s.Count);

				DerObjectIdentifier oid = DerObjectIdentifier.GetInstance(s[0].ToAsn1Object());

				bool isCritical = s.Count == 3
					&& DerBoolean.GetInstance(s[1].ToAsn1Object()).IsTrue;

				Asn1OctetString octets = Asn1OctetString.GetInstance(s[s.Count - 1].ToAsn1Object());

				extensions.Add(oid, new X509Extension(isCritical, octets));
				ordering.Add(oid);
			}
        }

        /**
         * constructor from a table of extensions.
         * <p>
         * it's is assumed the table contains Oid/string pairs.</p>
         */
        public X509Extensions(
            IDictionary extensions)
            : this(null, extensions)
        {
        }

        /**
         * Constructor from a table of extensions with ordering.
         * <p>
         * It's is assumed the table contains Oid/string pairs.</p>
         */
        public X509Extensions(
            IList       ordering,
            IDictionary extensions)
        {
            if (ordering == null)
            {
                this.ordering = Platform.CreateArrayList(extensions.Keys);
            }
            else
            {
                this.ordering = Platform.CreateArrayList(ordering);
            }

            foreach (DerObjectIdentifier oid in this.ordering)
            {
                this.extensions.Add(oid, (X509Extension)extensions[oid]);
            }
        }

        /**
         * Constructor from two vectors
         *
         * @param objectIDs an ArrayList of the object identifiers.
         * @param values an ArrayList of the extension values.
         */
        public X509Extensions(
            IList oids,
            IList values)
        {
            this.ordering = Platform.CreateArrayList(oids);

            int count = 0;
            foreach (DerObjectIdentifier oid in this.ordering)
            {
                this.extensions.Add(oid, (X509Extension)values[count++]);
            }
        }

#if !SILVERLIGHT
		/**
         * constructor from a table of extensions.
         * <p>
         * it's is assumed the table contains Oid/string pairs.</p>
         */
        [Obsolete]
        public X509Extensions(
            Hashtable extensions)
             : this(null, extensions)
        {
        }

		/**
         * Constructor from a table of extensions with ordering.
         * <p>
         * It's is assumed the table contains Oid/string pairs.</p>
         */
        [Obsolete]
        public X509Extensions(
            ArrayList	ordering,
            Hashtable	extensions)
        {
            if (ordering == null)
            {
                this.ordering = Platform.CreateArrayList(extensions.Keys);
            }
            else
            {
                this.ordering = Platform.CreateArrayList(ordering);
            }

            foreach (DerObjectIdentifier oid in this.ordering)
			{
				this.extensions.Add(oid, (X509Extension) extensions[oid]);
			}
        }

		/**
		 * Constructor from two vectors
		 *
		 * @param objectIDs an ArrayList of the object identifiers.
		 * @param values an ArrayList of the extension values.
		 */
        [Obsolete]
		public X509Extensions(
			ArrayList	oids,
			ArrayList	values)
		{
            this.ordering = Platform.CreateArrayList(oids);

            int count = 0;
			foreach (DerObjectIdentifier oid in this.ordering)
			{
				this.extensions.Add(oid, (X509Extension) values[count++]);
			}
		}
#endif

        [Obsolete("Use ExtensionOids IEnumerable property")]
		public IEnumerator Oids()
		{
			return ExtensionOids.GetEnumerator();
		}

		/**
		 * return an Enumeration of the extension field's object ids.
		 */
		public IEnumerable ExtensionOids
        {
			get { return new EnumerableProxy(ordering); }
        }

		/**
         * return the extension represented by the object identifier
         * passed in.
         *
         * @return the extension if it's present, null otherwise.
         */
        public X509Extension GetExtension(
            DerObjectIdentifier oid)
        {
             return (X509Extension) extensions[oid];
        }

		/**
		 * <pre>
		 *     Extensions        ::=   SEQUENCE SIZE (1..MAX) OF Extension
		 *
		 *     Extension         ::=   SEQUENCE {
		 *        extnId            EXTENSION.&amp;id ({ExtensionSet}),
		 *        critical          BOOLEAN DEFAULT FALSE,
		 *        extnValue         OCTET STRING }
		 * </pre>
		 */
		public override Asn1Object ToAsn1Object()
        {
            Asn1EncodableVector	vec = new Asn1EncodableVector();

			foreach (DerObjectIdentifier oid in ordering)
			{
                X509Extension ext = (X509Extension) extensions[oid];
                Asn1EncodableVector	v = new Asn1EncodableVector(oid);

				if (ext.IsCritical)
                {
                    v.Add(DerBoolean.True);
                }

				v.Add(ext.Value);

				vec.Add(new DerSequence(v));
            }

			return new DerSequence(vec);
        }

		public bool Equivalent(
			X509Extensions other)
		{
			if (extensions.Count != other.extensions.Count)
				return false;

			foreach (DerObjectIdentifier oid in extensions.Keys)
			{
				if (!extensions[oid].Equals(other.extensions[oid]))
					return false;
			}

			return true;
		}

		public DerObjectIdentifier[] GetExtensionOids()
		{
			return ToOidArray(ordering);
		}

		public DerObjectIdentifier[] GetNonCriticalExtensionOids()
		{
			return GetExtensionOids(false);
		}

		public DerObjectIdentifier[] GetCriticalExtensionOids()
		{
			return GetExtensionOids(true);
		}

		private DerObjectIdentifier[] GetExtensionOids(bool isCritical)
		{
			IList oids = Platform.CreateArrayList();

			foreach (DerObjectIdentifier oid in this.ordering)
            {
				X509Extension ext = (X509Extension)extensions[oid];
				if (ext.IsCritical == isCritical)
				{
					oids.Add(oid);
				}
            }

			return ToOidArray(oids);
		}

		private static DerObjectIdentifier[] ToOidArray(IList oids)
		{
			DerObjectIdentifier[] oidArray = new DerObjectIdentifier[oids.Count];
			oids.CopyTo(oidArray, 0);
			return oidArray;
		}
	}
}
