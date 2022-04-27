namespace KWH.WebApi.Dtos
{
    public class SectionDtos
    {
        public Guid SectionId { get; set; }
        public string SectionName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}
