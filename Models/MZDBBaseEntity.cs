using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MegaZord.Framework.Models {

    public class MZDBBaseEntity {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key]
        public long Id { get; set; }
    }

}
