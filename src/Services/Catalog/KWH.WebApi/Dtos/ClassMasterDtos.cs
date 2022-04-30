namespace KWH.WebApi.Dtos
{
    public class ClassMasterDtos 
    {
        public Guid ClassId { get; set; }
        public Guid SectionId { get; set; }
        public string ClassName { get; set; } = string.Empty;
    }
}
