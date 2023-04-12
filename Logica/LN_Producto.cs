using Entidades;
using AccesoDatos;
using System;
using System.Collections.Generic;

namespace Logica
{
    public class LN_Producto
    {

        private string _cadenaConexion;

        public string CadenaConexion { 
            get => _cadenaConexion; 
            set => _cadenaConexion = value; 
        }

        public LN_Producto() {
            _cadenaConexion = string.Empty;
        }

        public LN_Producto(string cadenaConexion)
        {
            _cadenaConexion = cadenaConexion;
        }

        public int Insertar(Producto producto) {
            int resultado = -1;
            AD_Producto AccesoDatos = new AD_Producto(_cadenaConexion);
            try
            {
                resultado = AccesoDatos.Insertar(producto);
            }
            catch (Exception e)
            {
                throw e;
            }
            return resultado;
        }

        public int InsertarModificar(Producto producto)
        {
            int resultado = -1;
            AD_Producto AccesoDatos = new AD_Producto(_cadenaConexion);
            try
            {
                resultado = AccesoDatos.InsertarModificar(producto);
            }
            catch (Exception e)
            {
                throw e;
            }
            return resultado;
        }
        public Producto ObtenerProducto(string condicion)
        {
            Producto resultado;
            AD_Producto AccesoDatos = new AD_Producto(_cadenaConexion);
            try
            {
                resultado = AccesoDatos.ObtenerProducto(condicion);
            }
            catch (Exception e)
            {
                throw e;
            }
            return resultado;
        }

        public List<Producto> ListarProductos(string condicion="")
        {
            List<Producto> resultado;
            AD_Producto AccesoDatos = new AD_Producto(_cadenaConexion);
            try
            {
                resultado = AccesoDatos.ListarProductos(condicion);
            }
            catch (Exception e)
            {
                throw e;
            }
            return resultado;
        }
        public bool Eliminar(int idproducto)
        {
            bool resultado = false;
            AD_Producto AccesoDatos = new AD_Producto(_cadenaConexion);
            AD_Detalle AccesoDetalle = new AD_Detalle(_cadenaConexion);
            try
            {
                string condicion = $"productoId={idproducto}";
                if (AccesoDetalle.ListarDetalles(condicion).Count > 0)
                {
                    resultado = AccesoDatos.Eliminar(idproducto, false);
                }
                else {
                    resultado = AccesoDatos.Eliminar(idproducto, true);
                }
                
            }
            catch (Exception e)
            {
                throw e;
            }
            return resultado;
        }

        
    }
}
