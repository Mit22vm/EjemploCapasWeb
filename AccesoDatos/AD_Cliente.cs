using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;

namespace AccesoDatos
{
    public class AD_Cliente
    {
        private string _cadenaConexion;
        

        public AD_Cliente(string cadenaConexion)
        {
            _cadenaConexion = cadenaConexion;
        }

        public Cliente ObtenerCliente(string condicion)
        {
            Cliente cliente = new Cliente();
            SqlConnection cnn = new SqlConnection(_cadenaConexion);
            SqlCommand comando = new SqlCommand();
            comando.Connection = cnn;
            SqlDataReader datos;
            string sentencia = "Select id,nombre,apellido,telefono from Clientes";
            if (!string.IsNullOrEmpty(condicion))
            {
                sentencia = $"{sentencia} where {condicion}";
            }
            comando.CommandText = sentencia;
            try
            {
                cnn.Open();
                datos = comando.ExecuteReader();
                if (datos.HasRows)
                {
                    datos.Read();
                    cliente.Id = datos.GetInt32(0);
                    cliente.Nombre= datos.GetString(1);
                    cliente.Apellido= datos.GetString(2);
                    cliente.Telefono= datos.GetString(3);
                    cliente.Existe = true;
                }
                cnn.Close();
            }
            catch (Exception e)
            {

                throw e;
            }

            return cliente;
        }

        public List<Cliente> ListarClientes(string condicion = "")
        {
            DataSet datos = new DataSet();
            SqlConnection cnn = new SqlConnection(_cadenaConexion);
            SqlDataAdapter adapter;
            List<Cliente> clientes = new List<Cliente>();

            string sentencia = "Select id,nombre, apellido, telefono from Clientes";
            if (!string.IsNullOrEmpty(condicion))
            {
                sentencia = $"{sentencia} where {condicion}";
            }
            try
            {
                adapter = new SqlDataAdapter(sentencia, cnn);
                adapter.Fill(datos, "Clientes");
                //linq lenguaje de c# para manejo de consultas
                if (datos.Tables[0].Rows.Count > 0) { 
                    clientes = (from DataRow registro in datos.Tables[0].Rows
                                select new Cliente()
                                {
                                    Id = Convert.ToInt32(registro[0]),
                                    Nombre = registro[1].ToString(),
                                    Apellido = registro[2].ToString(),
                                    Telefono = registro[3].ToString(),
                                    Existe = true
                                }
                           ).ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return clientes;

        }
    }
}
