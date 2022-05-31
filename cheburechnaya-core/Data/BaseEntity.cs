using System.ComponentModel.DataAnnotations.Schema;

namespace cheburechnaya_core.Data {
    public class BaseEntity : IEntityDate {
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
    interface IEntityDate {
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
