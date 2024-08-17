namespace Shared.Core.Entities;

public sealed class Document
{
    public int Id { get; set; }

    public string DocumentName { get; set; } = null!;

    public byte[] DocumentPdf { get; set; } = null!;

    public int LeaseLeaseId { get; set; }

    public int DateAdded { get; set; }

    public int LeaseId { get; set; }

    public int? MainDocumentId { get; set; }

    public ICollection<Document> InverseMainDocument { get; set; } = new List<Document>();

    public Lease Lease { get; set; } = null!;

    public Document? MainDocument { get; set; }
}