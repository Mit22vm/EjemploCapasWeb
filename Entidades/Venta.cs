using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class Venta
    {
        private int id;
        private string nombreCliente;
        private DateTime fecha;
        private string tipo;
        private int clienteId;
        private decimal total;
        private string estado;
        private bool existe;

        public int Id { 
            get => id; 
            set => id = value; 
        }
        public string NombreCliente { 
            get => nombreCliente; 
            set => nombreCliente = value; 
        }

        public string Estado
        {
            get => estado;
            set => estado = value;
        }
        public DateTime Fecha { 
            get => fecha; 
            set => fecha = value; 
        }
        public string Tipo { 
            get => tipo; 
            set => tipo = value; 
        }
        public int ClienteId { 
            get => clienteId; 
            set => clienteId = value; 
        }
        public decimal Total { 
            get => total; 
            set => total = value; 
        }

        public bool Existe
        {
            get => existe;
            set => existe = value;
        }
        public Venta()
        {
            id= 0;
            nombreCliente= string.Empty;
            estado = string.Empty;
            fecha = DateTime.Today;
            tipo = string.Empty;
            clienteId= 0;
            total= 0;
            existe= false;
        }
    }
}
