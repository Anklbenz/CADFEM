using UnityEngine.Networking;

public class ForceAcceptAllCertificates : CertificateHandler {
    protected override bool ValidateCertificate(byte[] certificateData) => true;
}