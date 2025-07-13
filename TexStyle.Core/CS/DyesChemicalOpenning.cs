using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TexStyle.Core.CS
{
   public class DyesChemicalOpenning:DefaultEntity
    {
        public DyesChemicalOpenning()
        {
            DyesChemicalOpenningDetails = new List<DyesChemicalOpenningDetail>();
        }
        [DisplayName("Opening No")]
        public long Id { get; set; }
        [DisplayName("Transaction Date")]
        public DateTime TransactionDate { get; set; }
        private Boolean _isConfirm = false;
        public Boolean IsConfirm
        {
            get
            {
                return _isConfirm;
            }
            set
            {
                _isConfirm = value;
            }
        }
        public ICollection<DyesChemicalOpenningDetail> DyesChemicalOpenningDetails { get; set; }

    }
}
