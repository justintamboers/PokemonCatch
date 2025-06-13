using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonLib
{
    public static class DataBewerking
    {

        private static DataSet ds;
        public static void InitializeDataBewerking()
        {
            InitializeNewDataSet();
        }
        public static void InitializeNewDataSet()
        {

            ds = new DataSet();

        }
        public static void SaveTrainerDataSet()
        {
            ds.WriteXml("trainer.xml");
        }
    }
}
