//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DiplomErshov.DataFolder
{
    using System;
    using System.Collections.Generic;
    
    public partial class PowerSupply
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PowerSupply()
        {
            this.Computer = new HashSet<Computer>();
        }
    
        public int IdPowerSupply { get; set; }
        public string NamePowerSupply { get; set; }
        public int IdWattage { get; set; }
        public string SerialNumberPowerSupply { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Computer> Computer { get; set; }
        public virtual Wattage Wattage { get; set; }
    }
}
