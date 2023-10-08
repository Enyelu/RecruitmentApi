namespace RecruitmentDomain.Entities
{
    public class BaseEntity
    {
        public string id { get; set; } = Guid.NewGuid().ToString();
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedOn { get;} = DateTime.UtcNow;
    }
}