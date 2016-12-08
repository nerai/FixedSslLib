# FixedSslLib
The FixedSslStream class is an SslStream that properly sends a close_notify message when closing the connection. This is required per [RFC 5246](https://tools.ietf.org/html/rfc5246#section-7.2.1) to avoid truncation attacks.

The SslStream included in .NET 4 does not adhere to this requirement. Somewhat surprisingly, it was decided by Microsoft to not fix the security problem, citing backwards compatibility as the reason. ([Further reading](https://connect.microsoft.com/VisualStudio/feedback/details/788752/sslstream-does-not-properly-send-the-close-notify-alert))

## Authors and license
This library is licensed under [LGPLv3](https://www.gnu.org/licenses/lgpl-3.0.en.html).

The original code was [written](http://stackoverflow.com/a/22626756/39590) by user [Neco](http://stackoverflow.com/users/1655991/neco) on Stackoverflow. Many thanks to them!

I slightly refactored the code and included changes that help improve interaction in more complex environments.
