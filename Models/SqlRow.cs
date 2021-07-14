/*
 SQLROW
 Hector Martin Ruiz Rosas
 
 Traducido de C++:
 
  class Row : public std::map <std::string, std::any>
    {
    private:
        int index;
    };


class Rows : public std::list<Row*>
    {
    private:
        int index;

    public:
        static Rows GetColumns(string Query);
    };
 
 */
using EsquemaDinamico.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace EsquemaDinamico.Models
{
    public class SqlRow : Dictionary<string,object>
    {
        public int Id { set; get; }
     

    }

    public class SqlRows : List<SqlRow>
    {
        public int Id { set; get; }


        public static async Task<SqlRows> GetColumnsAsync(DbCommand command, List<Campos> columns)
        {
            SqlRows Rows =  new SqlRows();

            var result = await command.ExecuteReaderAsync();

            while (result.Read())
            {
                SqlRow Row = new SqlRow();

                foreach(Campos column in columns)
                {

                    switch (column.Tipo)
                    {
                        case Tipo.NUMERIC:
                            Row.Add( column.Nombre, result.GetInt32(column.Nombre));
                            break;
                        case Tipo.TEXT:
                            Row.Add(column.Nombre, result.GetString(column.Nombre));
                            break;
                    }


                }

                Rows.Add(Row);
                
               // var tem2 = result.GetColumnSchema();
            }

            return Rows;

        }
    }



}
