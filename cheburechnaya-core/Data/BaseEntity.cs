using System.ComponentModel.DataAnnotations.Schema;

namespace cheburechnaya_core.Data {
    public class BaseEntity {
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
