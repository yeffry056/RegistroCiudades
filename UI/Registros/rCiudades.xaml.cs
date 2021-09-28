using RegistroCiudades.BLL;
using RegistroCiudades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RegistroCiudades.UI.Registros
{
    /// <summary>
    /// Interaction logic for rCiudades.xaml
    /// </summary>
    public partial class rCiudades : Window
    {
        private Ciudades ciudad = new Ciudades();
        public rCiudades()
        {
            InitializeComponent();
            this.DataContext = ciudad;
        }

        private void BtnBuscar(object sender, RoutedEventArgs e)
        {
            if (TextCiudadID.Text.Length == 0 || Convert.ToInt32(TextCiudadID.Text) == 0)
            {

                MessageBox.Show("Ciudad Id vacio", "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var ciudad = CiudadesBLL.Buscar(Convert.ToInt32(TextCiudadID.Text));
            if (ciudad != null)
                this.ciudad = ciudad;
            else
            {
                Limpiar();
                MessageBox.Show("No existe la ciudad que intenta buscar", "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }


            this.DataContext = this.ciudad;
        }
        public void Limpiar()
        {
            this.ciudad = new Ciudades();
            this.DataContext = ciudad;
        }
        private bool Validar()
        {
            bool esValido = true;
            if (TextCiudadID.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Transaccion Fallida", "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if (TextNombre.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Transaccion Fallida", "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            return esValido;
        }

        private void BtnNuevo(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void BtnGuardar(object sender, RoutedEventArgs e)
        {
            if (!Validar())
                return;

            var paso = CiudadesBLL.Guardar(ciudad);

            if (paso)
            {
                
                Limpiar();
                MessageBox.Show("Transaccion Exitosa", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }

        private void BtnEliminar(object sender, RoutedEventArgs e)
        {
            if (TextCiudadID.Text.Length != 0)
            {

                if (!CiudadesBLL.Existe(Convert.ToInt32(TextCiudadID.Text)))
                {
                    MessageBox.Show("El registro no existe", "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Ciudad Id vacio", "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            if (TextCiudadID.Text.Length == 0 || TextNombre.Text.Length == 0)
            {
                MessageBox.Show("Para eliminar un registro primero debe buscarlo", "Exito", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (CiudadesBLL.Eliminar(Convert.ToInt32(TextCiudadID.Text)))
            {
                Limpiar();
                MessageBox.Show("Registro eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("No fue posible eliminar el registro", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
