using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PokemonLib
{
    public static class TrainerData
    {
       private static DataTable Trainerdatatable;
       private const string TRAINER_COLUMN_ID = "TrainerId";
       private const string TRAINER_COLUMN_NAME = "TrainerName";
       private const string TRAINER_COLUMN_PASS = "TrainerPass";

        static TrainerData()
        {
            InitializeTrainderDataTable();    
        }

        public static void InitializeTrainderDataTable()
        {
            Trainerdatatable = new DataTable("Trainers");

            DataColumn TrainerId=  new DataColumn(TRAINER_COLUMN_ID, typeof(int));
            DataColumn TrainerName = new DataColumn(TRAINER_COLUMN_NAME, typeof(string));
            //DataColumn TrainerPass = new DataColumn("TrainerPass", typeof(string));
                Trainerdatatable.Columns.Add(TrainerId);    
                Trainerdatatable.Columns.Add(TrainerName);
                //trainerdatatable.Columns.Add(TrainerPass);
            Trainerdatatable.Columns.Add(TRAINER_COLUMN_PASS, typeof(string));
        }

        public static void CreateTrainer(string trainerName, string password)
        {
            if (CheckUniqueTrainerName(trainerName))
            {
                Trainerdatatable.Rows.Add(GetNextTrainerId(), trainerName, password);   
            }
            else
            {
                throw new Exception("trainer naam bestaat al");
            }
        }

        public static int GetNextTrainerId()
        {
            //if (Trainerdatatable.Rows.Count == 0)
            //{
            //    return 1;
            //}
            //else return Trainerdatatable.AsEnumerable().Max(t => (int)t["TrainerId"]) + 1;

            var res = (from tr in Trainerdatatable.AsEnumerable()
                       select tr[TRAINER_COLUMN_ID]);
            int id = Convert.ToInt32(res.Max());

            return id + 1;
        }

        public static bool CheckTrainerLogin(string trainerName, string password)
        {
            //Querry Linq
            //var res = (from tr in Trainerdatatable.AsEnumerable()
            //           where tr[TRAINER_COLUMN_NAME].ToString() == trainerName && tr[TRAINER_COLUMN_PASS].ToString() == password
            //           select tr);

            ////!(res is null);
            
            //return (res.Count() > 0);

            //Methode Linq
            return Trainerdatatable.AsEnumerable().Any(t => t[TRAINER_COLUMN_NAME].Equals(trainerName) && t[TRAINER_COLUMN_PASS].Equals(password));
        }
        
        private static bool CheckUniqueTrainerName(string trainerName)
        {
            //var res = (from tr in Trainerdatatable.AsEnumerable()
            //           where tr[TRAINER_COLUMN_NAME].ToString() == trainerName
            //           select tr);
            //if (res == null)
            //{
            //    return true;
            //}
            //else return false;

            return !Trainerdatatable.AsEnumerable().Any(t => t[TRAINER_COLUMN_NAME].Equals(trainerName));
        }
        
        public static int GetTrainerIdByTrainerName(string trainerName)
        {
            var trainer =
            (from t in Trainerdatatable.AsEnumerable()
             where t[TRAINER_COLUMN_NAME] == trainerName
             select t[TRAINER_COLUMN_ID]);

            var res = (from tr in Trainerdatatable.AsEnumerable()
                       select tr[TRAINER_COLUMN_ID]);
            int id = Convert.ToInt32(res.Max());

            if (trainer == null)
            {
                throw new ExecutionEngineException();
            }
            else return id;
        }
    }
}
