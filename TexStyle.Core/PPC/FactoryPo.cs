using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace TexStyle.Core.PPC
{
 public   class FactoryPo : DefaultEntity
    {
        public FactoryPo()
        {
            FactoryPoDetail = new List<FactoryPoDetail>();
        }
        public ICollection<FactoryPoDetail> FactoryPoDetail { get; set; }
        public string Description { get; set; }
        [DisplayName("Sno")]
        public long Id { get; set; }
        [DisplayName("Date")]
        public DateTime Date { get; set; }
        private Boolean _isCancel = false;
        [DisplayName("Confirm?")]
        public Boolean IsCancel
        {
            get
            {
                return _isCancel;
            }
            set
            {
                _isCancel = value;
            }
        } 
        private Boolean _IsChecked = false;
        [DisplayName("Chceked?")]
        public Boolean IsChecked
        {
            get
            {
                return _IsChecked;
            }
            set
            {
                _IsChecked = value;
            }
        }
        [DisplayName("Buyer")]
        public long? BuyerId { get; set; }
        [ForeignKey(nameof(BuyerId))]
        public virtual Buyer Buyer { get; set; }
        [DisplayName("GSM")]
        public long? GSM { get; set; }
        [DisplayName("Po")]
        public long? Po { get; set; }

        public long? BuyerColorId { get; set; }
        [ForeignKey(nameof(BuyerColorId))]
        public virtual BuyerColor BuyerColor { get; set; }

    }
}
