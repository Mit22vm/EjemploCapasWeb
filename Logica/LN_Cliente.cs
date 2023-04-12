using AccesoDatos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logica
{
    public class LN_Cliente
    {
        private string _cadenaConexion;

        public LN_Cliente(string cadenaConexion)
        {
            _cadenaConexion = cadenaConexion;
        }

        public Cliente ObtenerCliente(string condicion)
        {
            Cliente resultado;
            AD_Cliente AccesoDatos = new AD_Cliente(_cadenaConexion);
            try
            {
                resultado = AccesoDatos.ObtenerCliente(condicion);
            }
            catch (Exception e)
            {
                throw e;
            }
            return resultado;
        }

        public List<Cliente> ListarClientes(string condicion = "")
        {
            List<Cliente> resultado;
            AD_Cliente AccesoDatos = new AD_Cliente(_cadenaConexion);
            try
            {
                resultado = AccesoDatos.ListarClientes(condicion);
            }
            catch (Exception e)
            {
                throw e;
            }
            return resultado;
        }
    }
}
