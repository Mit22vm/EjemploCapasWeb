using Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Linq;

namespace AccesoDatos
{
    public class AD_Detalle
    {
        private string _cadenaConexion;
        
        public AD_Detalle(string cadenaConexion)
        {
            _cadenaConexion = cadenaConexion;
        }

        public List<Detalle> ListarDetalles(string condicion = "")
        {
            DataSet datos = new DataSet();
            SqlConnection cnn = new SqlConnection(_cadenaConexion);
            SqlDataAdapter adapter;
            List<Detalle> detalles=new List<Detalle>();

            string sentencia = "Select id,ventaId,productoId,descripcion,cantidad,precioventa,subtotal from DetallesVenta";
            if (!string.IsNullOrEmpty(condicion))
            {
                sentencia = $"{sentencia} where {condicion}";
            }
            try
            {
                adapter = new SqlDataAdapter(sentencia, cnn);
                adapter.Fill(datos, "Detalle");
                //linq lenguaje de c# para manejo de consultas
                //detalles = (from DataRow registro in datos.Tables["Detalle"].Rows
                //             select new Detalle()
                //             {
                //                 Id = Convert.ToInt32(registro[0]),
                //                 VentaId = Convert.ToInt32(registro[1]),
                //                 ProductoId = Convert.ToInt32(registro[2]),
                //                 Descripcion = registro[3].ToString(),
                //                 Cantidad = Convert.ToInt32(registro[4]),
                //                 PrecioVenta = Convert.ToDecimal(registro[5]),
                //                 Subtotal = Convert.ToDecimal(registro[6]),
                //                 Existe=true
                //             }
                //           ).ToList();
                foreach (DataRow fila in datos.Tables["Detalle"].Rows)
                {
                    Detalle detalle = new Detalle()
                    {
                        Id = Convert.ToInt32(fila[0]),
                        VentaId = Convert.ToInt32(fila[1]),
                        ProductoId = Convert.ToInt32(fila[2]),
                        Descripcion = fila[3].ToString(),
                        Cantidad = Convert.ToInt32(fila[4]),
                        PrecioVenta = Convert.ToDecimal(fila[5]),
                        Subtotal = Convert.ToDecimal(fila[6]),
                        Existe = true
                    };
                    detalles.Add(detalle);
                    
                }

            }
            catch (Exception e)
            {

                throw e;
            }

            return detalles;

        }

        public Detalle ObtenerDetalle(string condicion = "")
        {
            DataSet datos = new DataSet();
            SqlConnection cnn = new SqlConnection(_cadenaConexion);
            SqlDataAdapter adapter;
            Detalle detalle = new Detalle();

            string sentencia = "Select id,ventaId,productoId,descripcion,cantidad,precioventa,subtotal from DetallesVenta";
            if (!string.IsNullOrEmpty(condicion))
            {
                sentencia = $"{sentencia} where {condicion}";
            }
            try
            {
                adapter = new SqlDataAdapter(sentencia, cnn);
                adapter.Fill(datos, "Detalle");
                //linq lenguaje de c# para manejo de consultas
                if (datos.Tables[0].Rows.Count > 0) {
                    detalle = (from DataRow registro in datos.Tables["Detalle"].Rows
                               select new Detalle()
                               {
                                   Id = Convert.ToInt32(registro[0]),
                                   VentaId = Convert.ToInt32(registro[1]),
                                   ProductoId = Convert.ToInt32(registro[2]),
                                   Descripcion = registro[3].ToString(),
                                   Cantidad = Convert.ToInt32(registro[4]),
                                   PrecioVenta = Convert.ToDecimal(registro[5]),
                                   Subtotal = Convert.ToDecimal(registro[6]),
                                   Existe = true
                               }).FirstOrDefault();
                }
                
            }
            catch (Exception e)
            {

                throw e;
            }

            return detalle;

        }

        public bool Eliminar(int id)
        {
            bool resultado = false;
            SqlConnection cnn = new SqlConnection(_cadenaConexion);
            SqlCommand comando = new SqlCommand();
            string sentencia;
            comando.Connection = cnn;
            
            sentencia = "Delete Detalle where id=@Id";
            comando.CommandText = sentencia;
            comando.Parameters.AddWithValue("@Id", id);
            try
            {
                cnn.Open();
                if (comando.ExecuteNonQuery() > 0)
                {
                    resultado = true;
                }
                cnn.Close();
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                cnn.Dispose();
                comando.Dispose();
            }
            return resultado;
        }
    }
}
