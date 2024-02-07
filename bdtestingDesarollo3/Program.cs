using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdtestingDesarollo3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnection sqlConnection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\aleja\\source\\repos\\bdtestingDesarollo3\\bdtestingDesarollo3\\Database1.mdf;Integrated Security=True");
            sqlConnection.Open();
            SqlCommand cmd = null;
            bool salir = true;
            while (salir)
            {
                //añadir cliente
                Console.WriteLine(sqlConnection.State);
                string tipoDocumento, documento, nombre, apellido, sexo, fechanacimiento, comentario, localidad = "San Isidro";
                Console.WriteLine("Introduzca su tipo de documento: ");
                tipoDocumento = Console.ReadLine();
                Console.WriteLine("Introduzca su documento: ");
                documento = Console.ReadLine();
                Console.WriteLine("Introduzca su nombre: ");
                nombre = Console.ReadLine();
                Console.WriteLine("Introduzca su apellido: ");
                apellido = Console.ReadLine();
                Console.WriteLine("Introduzca su sexo: ");
                sexo = Console.ReadLine();
                Console.WriteLine("Introduzca su fecha de nacimiento: ");
                fechanacimiento = Console.ReadLine();
                //añadir visita
                Console.WriteLine("Introduzca su comentario: ");
                comentario = Console.ReadLine();
                Console.WriteLine("Localidad: "+localidad);
                cmd = new SqlCommand("ppInsertaCliente", sqlConnection);
                cmd.Parameters.AddWithValue("@tipoDocumento", tipoDocumento);
                cmd.Parameters.AddWithValue("@documento", documento);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@apellido", apellido);
                cmd.Parameters.AddWithValue("@sexo", sexo);
                cmd.Parameters.AddWithValue("@fechanacimiento", fechanacimiento);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                int Respuesta = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cmd.CommandText = "ppInsertarVisita";
                cmd.Parameters.AddWithValue("@tipoDocumento", tipoDocumento);
                cmd.Parameters.AddWithValue("@documento", documento);
                cmd.Parameters.AddWithValue("@comentario", comentario);
                cmd.Parameters.AddWithValue("@localidad", localidad);
                Respuesta = cmd.ExecuteNonQuery();
                cmd.CommandText = "ppUpsertaCliente";

                Console.WriteLine(Respuesta);
                Console.WriteLine("¿Salir? y/n");
                string respuesta = Console.ReadLine().ToLower();
                if(respuesta == "y") {
                    salir = false;
                } else {
                    salir= true;
                }
            }

            cmd.Parameters.Clear();
            cmd.CommandText = "ppGetClientes";
            SqlDataReader dr = cmd.ExecuteReader();
            Console.WriteLine("Nombres\tApellidos");
            while (dr.Read()){
                Console.WriteLine($"{dr["nombre"]}\t{dr["apellido"]}");
            }
        }
    }
}
