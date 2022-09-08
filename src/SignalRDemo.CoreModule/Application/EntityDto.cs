namespace SignalRDemo.Application;

public class EntityDto : IEntityDto
{
    public int Id { get; set; }
}

public class AuditableEntityDto : EntityDto
{
    public int CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public int? LastModifiedBy { get; set; }
    public DateTime? LastModifiedDate { get; set; }
}
