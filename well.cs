namespace aem4
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("well")]
    public partial class well
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int? platformId { get; set; }

        [StringLength(150)]
        public string uniqueName { get; set; }

        public decimal? latitude { get; set; }

        public decimal? longitude { get; set; }

        public DateTimeOffset? createdAt { get; set; }

        public DateTime updatedAt { get; set; }

        public virtual platform platform { get; set; }
    }
}
