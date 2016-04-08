namespace MvcTemplate.Data.Models
{
    using MvcTemplate.Data.Common.Models;

    public abstract class Image : BaseModel<int>
    {
        public byte[] Content { get; set; }

        public string Extension { get; set; }

        public string Name { get; set; }
    }
}
