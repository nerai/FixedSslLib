using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Text;

namespace FixedSslLib
{
	/// <summary>
	/// FixedSslStream is an SslStream that properly sends a close_notify message when closing
	/// the connection. This is required per RFC 5246 to avoid truncation attacks.
	/// For more information, see https://tools.ietf.org/html/rfc5246#section-7.2.1
	///
	/// The SslStream included in .NET 4 does not adhere to this requirement. Somewhat
	/// surprisingly, it was decided by Microsoft to not fix the security problem, citing
	/// backwards compatibility as the reason.
	/// For more information, see https://connect.microsoft.com/VisualStudio/feedback/details/788752/sslstream-does-not-properly-send-the-close-notify-alert
	/// </summary>
	public class FixedSslStream : SslStream
	{
		public FixedSslStream (Stream innerStream)
			: base (innerStream)
		{
		}

		public FixedSslStream (Stream innerStream, bool leaveInnerStreamOpen)
			: base (innerStream, leaveInnerStreamOpen)
		{
		}

		public FixedSslStream (
			Stream innerStream,
			bool leaveInnerStreamOpen,
			RemoteCertificateValidationCallback userCertificateValidationCallback)
			: base (
				  innerStream,
				  leaveInnerStreamOpen,
				  userCertificateValidationCallback)
		{
		}

		public FixedSslStream (
			Stream innerStream,
			bool leaveInnerStreamOpen,
			RemoteCertificateValidationCallback userCertificateValidationCallback,
			LocalCertificateSelectionCallback userCertificateSelectionCallback)
			: base (
				  innerStream,
				  leaveInnerStreamOpen,
				  userCertificateValidationCallback,
				  userCertificateSelectionCallback)
		{
		}

		public FixedSslStream (
			Stream innerStream,
			bool leaveInnerStreamOpen,
			RemoteCertificateValidationCallback userCertificateValidationCallback,
			LocalCertificateSelectionCallback userCertificateSelectionCallback,
			EncryptionPolicy encryptionPolicy)
			: base (
				  innerStream,
				  leaveInnerStreamOpen,
				  userCertificateValidationCallback,
				  userCertificateSelectionCallback,
				  encryptionPolicy)
		{
		}

		private bool _Closed = false;

		protected override void Dispose (bool disposing)
		{
			try {
				if (!_Closed) {
					_Closed = true;
					SslDirectCall.CloseNotify (this);
				}
			}
			finally {
				base.Dispose (disposing);
			}
		}
	}
}
