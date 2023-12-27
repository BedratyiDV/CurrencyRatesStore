using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyRatesStore
{
    [Serializable]
    public class CurrencyRate
    {
        private Guid _id;
        private string _ccy;
        private string _base_Ccy;
        private double _buy;
        private double _sale;

        public Guid Id
        {
            get => _id;
            set => _id = value;
        }
        public string Ccy
        {
            get => _ccy;
            set => _ccy = value;
        }
        public string Base_Ccy
        {
            get => _base_Ccy;
            set => _base_Ccy = value;
        }
        public double Buy
        {
            get => _buy;
            set => _buy = value;
        }
        public double Sale
        {
            get => _sale;
            set => _sale = value;
        }
    }
}
