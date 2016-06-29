using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MegaZord.Framework.Interfaces;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MegaZord.Framework.EF {
    [JsonObject(IsReference = true)]
    public class MZEntity : IMZEntity {
        [DataMember]
        public long ID { get; set; }

        [JsonIgnore]
        public bool IsNew {
            get { return ID == 0; }
        }
    }  

    [Table("Parametros", Schema = "MZ")]
    [JsonObject(IsReference = true)]
    public class MZParametro : MZEntity {
        [Required, Display(Name="Nome do Parâmetro")]
        public string NomeParametro { get; set; }

        [Required, Display(Name = "Valor do Parâmetro")]
        public string ValorParametro { get; set; }
    }
}
