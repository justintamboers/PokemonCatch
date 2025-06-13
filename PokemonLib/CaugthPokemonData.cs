using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonLib
{
    public static class CaugthPokemonData
    {

        private const string POKEMON_TABLE = "CaugthPokemons";
        private const string CAUGHTPOKEMON_COLUMN_ID = "CaugthPokemonId";
        private const string POKEMON_COLUMN_ID = "PokemonId";
        private const string TRAINER_COLUMN_ID = "TrainerId";

        private static DataTable Caugthdatatable;
        static CaugthPokemonData()
        {
            InitializeCaugthPokemonDataTable();
        }
        public static void InitializeCaugthPokemonDataTable()
        {
            Caugthdatatable = new DataTable(POKEMON_TABLE);

            Caugthdatatable.Columns.Add(CAUGHTPOKEMON_COLUMN_ID, typeof(int));
            Caugthdatatable.Columns.Add(POKEMON_COLUMN_ID, typeof(int));
            Caugthdatatable.Columns.Add(TRAINER_COLUMN_ID, typeof(int));
  
        }

        public static int GetNextCaugthPokemonId()
        {
            var res = (from tr in Caugthdatatable.AsEnumerable()
                       select tr[CAUGHTPOKEMON_COLUMN_ID]);
            int id = Convert.ToInt32(res.Max());

            return id + 1;
        }

        public static int GetCaugthPokemonIdsByTrainerId(int trainerId)
        {
            return Convert.ToInt32((from tr in Caugthdatatable.AsEnumerable()
                       where tr[TRAINER_COLUMN_ID].Equals(trainerId)
                       select tr[POKEMON_COLUMN_ID]).ToString());
        }

        public static void CatchPokemon(int trainerid, int pokemonNumber)
        {
            Caugthdatatable.Rows.Add(GetNextCaugthPokemonId(), trainerid, pokemonNumber);
        }
    }
}
