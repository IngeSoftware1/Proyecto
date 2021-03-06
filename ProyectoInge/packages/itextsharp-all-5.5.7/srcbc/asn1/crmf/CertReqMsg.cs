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

namespace Org.BouncyCastle.Asn1.Crmf
{
    public class CertReqMsg
        : Asn1Encodable
    {
        private readonly CertRequest certReq;
        private readonly ProofOfPossession popo;
        private readonly Asn1Sequence regInfo;

        private CertReqMsg(Asn1Sequence seq)
        {
            certReq = CertRequest.GetInstance(seq[0]);

            for (int pos = 1; pos < seq.Count; ++pos)
            {
                object o = seq[pos];

                if (o is Asn1TaggedObject || o is ProofOfPossession)
                {
                    popo = ProofOfPossession.GetInstance(o);
                }
                else
                {
                    regInfo = Asn1Sequence.GetInstance(o);
                }
            }
        }

        public static CertReqMsg GetInstance(object obj)
        {
            if (obj is CertReqMsg)
                return (CertReqMsg)obj;

            if (obj != null)
                return new CertReqMsg(Asn1Sequence.GetInstance(obj));

            return null;
        }

        /**
         * Creates a new CertReqMsg.
         * @param certReq CertRequest
         * @param popo may be null
         * @param regInfo may be null
         */
        public CertReqMsg(
            CertRequest				certReq,
            ProofOfPossession		popo,
            AttributeTypeAndValue[]	regInfo)
        {
            if (certReq == null)
                throw new ArgumentNullException("certReq");

            this.certReq = certReq;
            this.popo = popo;

            if (regInfo != null)
            {
                this.regInfo = new DerSequence(regInfo);
            }
        }

        public virtual CertRequest CertReq
        {
            get { return certReq; }
        }

        public virtual ProofOfPossession Popo
        {
            get { return popo; }
        }

        public virtual AttributeTypeAndValue[] GetRegInfo()
        {
            if (regInfo == null)
                return null;

            AttributeTypeAndValue[] results = new AttributeTypeAndValue[regInfo.Count];
            for (int i = 0; i != results.Length; ++i)
            {
                results[i] = AttributeTypeAndValue.GetInstance(regInfo[i]);
            }
            return results;
        }

        /**
         * <pre>
         * CertReqMsg ::= SEQUENCE {
         *                    certReq   CertRequest,
         *                    pop       ProofOfPossession  OPTIONAL,
         *                    -- content depends upon key type
         *                    regInfo   SEQUENCE SIZE(1..MAX) OF AttributeTypeAndValue OPTIONAL }
         * </pre>
         * @return a basic ASN.1 object representation.
         */
        public override Asn1Object ToAsn1Object()
        {
            Asn1EncodableVector v = new Asn1EncodableVector(certReq);
            v.AddOptional(popo);
            v.AddOptional(regInfo);
            return new DerSequence(v);
        }
    }
}
