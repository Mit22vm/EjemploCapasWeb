using System;

namespace Entidades
{
    public class Producto
    {
        //atributos
        private int id;
        private string descripcion;
        private int cantidad;
        private decimal precio;
        //private decimal valorInventario;
        private bool existe;

        //constructores
        public Producto() {
            id = 0;
            descripcion = string.Empty;
            cantidad = 0;
            precio = 0;
            existe = false;
        }

        public Producto(int _id, string _descripcion, int _cantidad, decimal _precio)
        {
            id = _id;
            descripcion = _descripcion;
            cantidad = _cantidad;
            precio = _precio;
            existe = true;
        }

        //public decimal ValorInventario
        //{
        //    get {
        //        return cantidad * precio;
        //    }
        //}

        //propiedades
        public int Id { 
            get => id; 
            set => id = value; 
        }
        public string Descripcion { 
            get => descripcion; 
            set => descripcion = value; 
        }
        public int Cantidad { 
            get => cantidad; 
            set => cantidad = value; 
        }
        public decimal Precio { 
            get => precio; 
            set => precio = value; 
        }
        public bool Existe { 
            get => existe; 
            set => existe = value; 
        }
        public override string ToString()
        {
            return $"Id: {id}, Descipción: {descripcion}, Cantidad: {cantidad}, Precio: {precio}";
        }
    }
    
}
