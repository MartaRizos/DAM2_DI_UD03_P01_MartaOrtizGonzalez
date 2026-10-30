
using DAM2_DI_UD03_P01.Models;
using DAM2_DI_UD03_P01.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DAM2_DI_UD03_P01_MartaOrtizGonzalez
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel();
        }

        // Actualiza la prioridad seleccionada en el ViewModel.
        private void Prioridad_Checked(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainViewModel vm &&
                sender is RadioButton radioButton &&
                radioButton.Tag is string prioridad)
            {
                vm.NuevaPrioridad = prioridad;
            }
        }

        // Añade una nueva tarea.
        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainViewModel vm)
            {
                int numeroTareasAntes = vm.Tareas.Count;

                vm.AgregarTarea();

                // Si se ha añadido una tarea, restablecemos la prioridad.
                if (vm.Tareas.Count > numeroTareasAntes)
                {
                    RbPrioridadMedia.IsChecked = true;
                }
            }
        }

        // Limpia los campos del formulario sin eliminar tareas.
        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainViewModel vm)
            {
                vm.FormularioLimpio();

                RbPrioridadMedia.IsChecked = true;
            }
        }

        // Elimina la tarea correspondiente al botón pulsado.
        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainViewModel vm &&
                sender is Button boton &&
                boton.Tag is Tarea tarea)
            {
                vm.EliminarTarea(tarea);
            }
        }

        private void ListaTareas_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;

            if (sender is DependencyObject elemento)
            {
                DependencyObject? padre = elemento;

                while (padre != null && padre is not ScrollViewer)
                {
                    padre = VisualTreeHelper.GetParent(padre);
                }

                if (padre is ScrollViewer scrollViewer)
                {
                    scrollViewer.ScrollToVerticalOffset(
                        scrollViewer.VerticalOffset - e.Delta);
                }
            }
        }
    }
}

