using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonLib
{
    public static class PokemonData
    {

        private const string POKEMON_TABLE = "Pokemons";
        private const string POKEMON_COLUMN_ID = "PokemonId";
        private const string POKEMON_COLUMN_NUMBER = "Number";
        private const string POKEMON_COLUMN_NAME= "Name";
        private const string POKEMON_COLUMN_TYPE_1 = "Type1";
        private const string POKEMON_COLUMN_TYPE_2 = "Type2";

        private static DataTable dtPokemon;

        static PokemonData()
        {
            InitializePokemonDataTable();
        }
        private static void InitializePokemonDataTable()
        {
            dtPokemon = new DataTable(POKEMON_TABLE);

            dtPokemon.Columns.Add(POKEMON_COLUMN_ID, typeof(int));
            dtPokemon.Columns.Add(POKEMON_COLUMN_NUMBER, typeof(int));
            dtPokemon.Columns.Add(POKEMON_COLUMN_NAME, typeof(string));
            dtPokemon.Columns.Add(POKEMON_COLUMN_TYPE_1, typeof(string));
            dtPokemon.Columns.Add(POKEMON_COLUMN_TYPE_2, typeof(string));

            SqlConnection conn = new SqlConnection(@"Server=(localdb)\MSSQLLocalDB;Database=Pokemon;Trusted_Connection=True;"); // Replace with your connection string
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from Pokemons";
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter daPokemons = new SqlDataAdapter(cmd);

            dtPokemon.Copy();
            daPokemons.Fill(dtPokemon);
        }
        public static DataView GetPokemonDataView()
        {
            return dtPokemon.DefaultView;
        }
        public static DataView GetPokemonDataViewByNameFilter(string text)
        {
           return  (from p in dtPokemon.AsEnumerable()
                      where p[POKEMON_COLUMN_NAME].ToString().ToLower().Contains(text)
                      select p).AsDataView();

            //return dtPokemon.AsEnumerable().Where(t => t[POKEMON_COLUMN_NAME].ToString().Contains(text)).AsDataView();
        }
    }
}
