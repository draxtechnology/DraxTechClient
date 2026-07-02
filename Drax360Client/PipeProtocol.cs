namespace DraxClient
{
    /// <summary>
    /// Shared framing helpers for the named-pipe protocol with the DraxTechnology
    /// service. The service substitutes <see cref="EmptyResult"/> for a genuinely
    /// empty response, because a zero-length payload is a zero-length message that
    /// PipeTransmissionMode.Message cannot frame reliably. Every client pipe reader
    /// routes its decoded response through <see cref="Decode"/> so the sentinel is
    /// mapped back to "" transparently.
    /// </summary>
    internal static class PipeProtocol
    {
        // Must stay byte-for-byte identical to kpipeemptyresult on the service.
        // Control-char delimited so it can never collide with a real setting value.
        public const string EmptyResult = "\u0001EMPTY\u0001";

        /// <summary>Map the service's empty-response sentinel back to an empty string.</summary>
        public static string Decode(string raw) => raw == EmptyResult ? "" : raw;
    }
}
