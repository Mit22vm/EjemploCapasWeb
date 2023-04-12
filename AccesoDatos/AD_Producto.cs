using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;



namespace AccesoDatos
{
    public class AD_Producto
    {
        private string _cadenaConexion;
        private int id_producto;

        public AD_Producto(string cadenaConexion) {
            _cadenaConexion = cadenaConexion;
        }

        public int Id_producto {
            get => id_producto;
        }

        /// <summary>
        /// Inserta un producto en la base de datos
        /// </summary>
        /// <param name="producto">entidad a insertar</param>
        /// <returns>id del producto insertado</returns>
        public int Insertar(Producto producto) {
            
            int resultado=-1;
            SqlConnection conexion = new SqlConnection(_cadenaConexion);
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion;
            string sentencia;
            try
            {
                sentencia = "Insert into Productos(DESCRIPCION,CANTIDAD,PRECIO) values(@Descripcion,@Cantidad,@Precio) select SCOPE_IDENTITY()";
                comando.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                comando.Parameters.AddWithValue("@Cantidad", producto.Cantidad);
                comando.Parameters.AddWithValue("@Precio", producto.Precio);
                comando.CommandText = sentencia;

                conexion.Open();
                

                resultado = Convert.ToInt32(comando.ExecuteScalar());

                
                conexion.Close();

            }
            catch (Exception e)
            {

                throw e;
            }
            finally {
                conexion.Dispose();
                comando.Dispose();
            }


            return resultado;
        }
        public int InsertarModificar(Producto producto) {
            int resultado = -1;
            SqlConnection cnn=new SqlConnection(_cadenaConexion);
            SqlCommand comando = new SqlCommand();
            comando.Connection = cnn;
            string sentencia;
            try
            {
                sentencia = "SP_INSERTAR_MODIFICAR";
                comando.CommandText=sentencia;
                comando.CommandType= CommandType.StoredProcedure;
                //parametros de entrada
                comando.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                comando.Parameters.AddWithValue("@Cantidad", producto.Cantidad);
                comando.Parameters.AddWithValue("@Precio", producto.Precio);
                //parametros de salida
                comando.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.InputOutput;
                comando.Parameters["@Id"].Value = producto.Id;

                comando.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                cnn.Open();
                comando.ExecuteNonQuery();
                id_producto = Convert.ToInt32(comando.Parameters["@id"].Value);
                resultado = Convert.ToInt32(comando.Parameters["@resultado"].Value);
                cnn.Close();
            }
            catch (Exception)
            {

                throw;
            }

            return resultado;
        }

        public Producto ObtenerProducto(string condicion) {
            Producto producto=new Producto();
            SqlConnection cnn = new SqlConnection(_cadenaConexion);
            SqlCommand comando=new SqlCommand();
            comando.Connection = cnn;
            SqlDataReader datos;
            string sentencia = "Select id,descripcion,cantidad,precio from Productos where borrado=0 ";
            if (!string.IsNullOrEmpty(condicion)) {
                sentencia = $"{sentencia} and {condicion}";
            }
            comando.CommandText= sentencia;
            try
            {
                cnn.Open(); 
                datos= comando.ExecuteReader();
                if (datos.HasRows) { 
                    datos.Read();
                    producto.Id = datos.GetInt32(0);
                    producto.Descripcion= datos.GetString(1);
                    producto.Cantidad = datos.GetInt32(2);
                    producto.Precio=datos.GetDecimal(3);
                    producto.Existe = true;
                }
                cnn.Close();
            }
            catch (Exception e)
            {

                throw e;
            }

            return producto;
        }

        public List<Producto> ListarProductos(string condicion="")
        {
            DataSet datos=new DataSet();
            SqlConnection cnn=new SqlConnection(_cadenaConexion);
            SqlDataAdapter adapter;
            List<Producto> productos= new List<Producto>();
           
            string sentencia = "Select id,descripcion,cantidad,precio from Productos where borrado=0";
            if (!string.IsNullOrEmpty(condicion))
            {
                sentencia = $"{sentencia} and {condicion}";
            }
            try
            {
                adapter = new SqlDataAdapter(sentencia, cnn);
                adapter.Fill(datos, "Productos");
                //linq lenguaje de c# para manejo de consultas
                if (datos.Tables[0].Rows.Count > 0)
                    productos = (from DataRow registro in datos.Tables["Productos"].Rows
                             select new Producto()
                             {
                                 Id = Convert.ToInt32(registro[0]),
                                 Descripcion = registro[1].ToString(),
                                 Cantidad = Convert.ToInt32(registro[2]),
                                 Precio = Convert.ToDecimal(registro[3]),
                                 Existe= true
                             }
                           ).ToList();
            }
            catch (Exception e)
            {

                throw e;
            }
            
            return productos;

        }

        public bool Eliminar(int id,bool tipo)
        {
            bool resultado = false;
            SqlConnection cnn = new SqlConnection(_cadenaConexion);
            SqlCommand comando = new SqlCommand();
            string sentencia;
            comando.Connection = cnn;
            if (tipo)
            { //borrado físico
                sentencia = "Delete Productos where id=@Id";
               // sentencia = $"Delete Productos where id={id}";
            }
            else {
                sentencia = "Update Productos set Borrado=1 where id=@Id";
            }
            comando.CommandText = sentencia;
            comando.Parameters.AddWithValue("@Id", id);
            try
            {
                cnn.Open();
                if (comando.ExecuteNonQuery() > 0) { 
                    resultado= true;
                }
                cnn.Close();
            }
            catch (Exception e)
            {

                throw e;
            }
            finally { 
                cnn.Dispose(); 
                comando.Dispose();
            }
            return resultado;
        }
    }
}
