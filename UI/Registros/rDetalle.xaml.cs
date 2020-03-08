using rDetalle.BLL;
using rDetalle.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace rDetalle.UI.Registros
{
    /// <summary>
    /// Interaction logic for rDetalle.xaml
    /// </summary>
    public partial class RDetalle : Window
    {
        Personas persona = new Personas();
        public List<TelefonosDetalle> Detalle { get; set; }
        public RDetalle()
        {
            InitializeComponent();
            this.Detalle = new List<TelefonosDetalle>();
            this.DataContext = persona;
        }

        private void Limpiar()
        {
            IdTextBox.Text = string.Empty;
            NombreTextBox.Text = string.Empty;
            TelefonosTextBox.Text = string.Empty;
            DireccionTextBox.Text = string.Empty;
            FNacimientoDatePicker.Text = Convert.ToString(DateTime.Now);
            this.Detalle = new List<TelefonosDetalle>();
            CargaGrid();
        }
       
     

        private bool Validar()
        {
            bool paso = true;
            if(NombreTextBox.Text == string.Empty)
            {
                MessageBox.Show(NombreTextBox.Text,"El Campo Nombre no Puede estar vacio");
                NombreTextBox.Focus();
                paso = false;
            }
            if(string.IsNullOrWhiteSpace(DireccionTextBox.Text))
            {
                MessageBox.Show(DireccionTextBox.Text,"El Campo Direccion no puede estar vacio");
                DireccionTextBox.Focus();
                paso = false;
            }
            if(string.IsNullOrWhiteSpace(CedulaTextBox.Text))
            {
                MessageBox.Show(CedulaTextBox.Text,"El Campo Cedula no puede estar vacio");
                CedulaTextBox.Focus();
                paso = false;
            }
            if(this.Detalle.Count==0)
            {
                MessageBox.Show("Debe agregar algun telefono");
                TelefonosTextBox.Focus();
                paso = false;
            }
            return paso;
           
        }
        private bool ExisteEnLaBaseDeDatos()
        {
            Personas persona = PersonasBLL.Buscar(Convert.ToInt32(IdTextBox.Text));
            return (persona != null);

        }
        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

  

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            
            bool paso = false;
            if (!Validar())
                return;
            
            if (string.IsNullOrWhiteSpace(IdTextBox.Text) || IdTextBox.Text == "0")
                paso = PersonasBLL.Guardar(persona);
            else
            {
                if(!ExisteEnLaBaseDeDatos())
                {
                    MessageBox.Show("No se puede modificar porque no existe","Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                paso = PersonasBLL.Modificar(persona);
            }
            if(paso)
            {
                Limpiar();
            }
            else
            {
                MessageBox.Show("No fue posible Guardar!!","Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ElimarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            id = Convert.ToInt32(IdTextBox.Text);
            Limpiar();

            if (PersonasBLL.Eliminar(id))
                MessageBox.Show("Eliminado","Exito", MessageBoxButton.OK,MessageBoxImage.Information);
            else
                MessageBox.Show(IdTextBox.Text, "No se puede eliminar una persona que no existe");
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            int.TryParse(IdTextBox.Text, out id);
            Limpiar();


           var pers = PersonasBLL.Buscar(id);
            
            if (persona != null)
            {
                this.persona = pers; 
            }
            else
            {
                MessageBox.Show("Persona No Encontrada");
            }
        }
        private void CargaGrid()
        {
            DetalleDataGrid.ItemsSource = null;
            DetalleDataGrid.ItemsSource = this.Detalle;
        }

    

        private void AgregarButton_Click(object sender, RoutedEventArgs e)
        {
            if (DetalleDataGrid.ItemsSource != null)
                this.Detalle = (List<TelefonosDetalle>)DetalleDataGrid.ItemsSource;

            this.Detalle.Add(
                new TelefonosDetalle
                {
                    Telefono = TelefonosTextBox.Text,
                    TipoTelefono = TipoTextBox.Text
                }
                );
            CargaGrid();
            TelefonosTextBox.Focus();
            TipoTextBox.Text = string.Empty;
        }

        private void RemoverFilaButton_Click(object sender, RoutedEventArgs e)
        {
            if(DetalleDataGrid.Items.Count > 0 && DetalleDataGrid.SelectedItem!= null)
            {
                Detalle.RemoveAt(DetalleDataGrid.SelectedIndex);
                CargaGrid();
            }

        }
    }
}
