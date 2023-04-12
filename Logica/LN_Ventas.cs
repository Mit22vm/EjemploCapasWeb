using AccesoDatos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logica
{
    public class LN_Ventas
    {

        private string _cadenaConexion;
        private string _mensaje;
        private int _idventa;
       

        public string Mensaje { 
            get { 
                return _mensaje; 
            } 
        }

        public int IdVenta
        {
            get
            {
                return _idventa;
            }
        }

        public LN_Ventas(string cadenaConexion)
        {
            _cadenaConexion = cadenaConexion;
            _mensaje=string.Empty;
            _idventa=0;
        }

        public Venta ObtenerVenta(string condicion = "")
        {
            Venta resultado;
            AD_Ventas AccesoDatos = new AD_Ventas(_cadenaConexion);
            try
            {
                resultado = AccesoDatos.ObtenerVenta(condicion);
            }
            catch (Exception e)
            {
                throw e;
            }
            return resultado;
        }

        public int Insertar(Venta venta, Detalle detalle) { 
            int resultado = 0;
            AD_Ventas ADVenta = new AD_Ventas(_cadenaConexion);
            AD_Cliente ADCliente =new AD_Cliente(_cadenaConexion);
            AD_Producto ADProducto= new AD_Producto(_cadenaConexion);
            Producto producto;
            try
            {
                if (ADCliente.ObtenerCliente($"id={venta.ClienteId}").Existe)
                {
                    producto = ADProducto.ObtenerProducto($"id={detalle.ProductoId}");
                    if (producto.Existe)
                    {
                        detalle.PrecioVenta = producto.Precio + (producto.Precio * (decimal)0.35);
                        resultado=ADVenta.Insertar(venta, detalle);
                        _idventa = ADVenta.Idventa;
                        switch (resultado)
                        {
                            case 1:
                                _mensaje = "Venta ingresada satisfactoriamente";
                                break;
                            case 2:
                                _mensaje = "Venta modificada satisfactoriamente";
                                break;
                            case 3:
                                _mensaje = "Detalle ingresado";
                                break;
                            case 4:
                                _mensaje = "Imposible modificar la venta ya que está cancelada";
                                break;
                            default:
                                break;


                        }

                    }
                    else {
                        resultado = 6;
                        _mensaje= "Imposible insertar la venta ya que el producto no existe";
                    }
                }
                else {
                    resultado = 5;//cliente no existe
                    _mensaje = "Imposible insertar la venta ya que el cliente no existe";
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            return resultado;
        }

        public decimal CalcularTotal(List<Detalle> detalles) { 
            decimal resultado = 0;

            foreach (Detalle item in detalles)
            {
                resultado += item.Subtotal;
            }
            return resultado;
        
        }
    }
}
