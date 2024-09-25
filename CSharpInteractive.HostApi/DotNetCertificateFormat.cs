namespace HostApi;

/// <summary>
/// Format of the certificate file.
/// </summary>
public enum DotNetCertificateFormat
{
    /// <summary>
    /// PFX (Personal Exchange Format) file is a digital certificate file format used in Microsoft Windows and other systems to store a private key and a corresponding public key certificate, along with any intermediate certificates that may be necessary to establish the trust chain. PFX files are often used for importing and exporting certificates between different systems or applications.
    /// </summary>
    Pfx,
    
    /// <summary>
    /// PEM (Privacy Enhanced Mail) is a file format used for storing and transmitting digital certificates, private keys, and other cryptographic information. PEM files use Base64 encoding and consist of a header, a footer, and the Base64-encoded data in between. 
    /// </summary>
    Pem
}