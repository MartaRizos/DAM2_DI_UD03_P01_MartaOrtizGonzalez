
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using DAM2_DI_UD03_P01.Models;

namespace DAM2_DI_UD03_P01.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _nuevoTitulo = string.Empty;

        public string NuevoTitulo
        {
            get => _nuevoTitulo;
            set
            {
                if (_nuevoTitulo != value)
                {
                    _nuevoTitulo = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(PuedeAgregarTarea));
                }
            }
        }

        public bool PuedeAgregarTarea =>
            !string.IsNullOrWhiteSpace(NuevoTitulo);

        private string _nuevoModulo = "DI";
        private DateTime? _nuevaFechaLimite;
        private string _nuevaPrioridad = "Media";
        private bool _nuevaTareaUrgente;

        public string NuevoModulo
        {
            get => _nuevoModulo;
            set
            {
                if (_nuevoModulo != value)
                {
                    _nuevoModulo = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime? NuevaFechaLimite
        {
            get => _nuevaFechaLimite;
            set
            {
                if (_nuevaFechaLimite != value)
                {
                    _nuevaFechaLimite = value;
                    OnPropertyChanged();
                }
            }
        }

        public string NuevaPrioridad
        {
            get => _nuevaPrioridad;
            set
            {
                if (_nuevaPrioridad != value)
                {
                    _nuevaPrioridad = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool NuevaTareaUrgente
        {
            get => _nuevaTareaUrgente;
            set
            {
                if (_nuevaTareaUrgente != value)
                {
                    _nuevaTareaUrgente = value;
                    OnPropertyChanged();
                }
            }
        }



        // Colección de tareas iniciales.
        public ObservableCollection<Tarea> Tareas { get; } =
            new ObservableCollection<Tarea>
            {
                new Tarea
                {
                    Titulo = "Diseñar la interfaz de TaskFlow",
                    Modulo = "DI",
                    Prioridad = "Alta",
                    EsUrgente = true,
                    Completada = true
                },
                new Tarea
                {
                    Titulo = "Estudiar gestión de procesos",
                    Modulo = "PSP",
                    Prioridad = "Alta",
                    EsUrgente = false,
                    Completada = true
                },
                new Tarea
                {
                    Titulo = "Practicar consultas SQL",
                    Modulo = "SGE",
                    Prioridad = "Media",
                    EsUrgente = false,
                    Completada = false
                },
                new Tarea
                {
                    Titulo = "Implementar navegación en Compose",
                    Modulo = "PMDM",
                    Prioridad = "Alta",
                    EsUrgente = true,
                    Completada = false
                },
                new Tarea
                {
                    Titulo = "Repasar programación multimedia",
                    Modulo = "PMDM",
                    Prioridad = "Baja",
                    EsUrgente = false,
                    Completada = false
                }
            };

        public ObservableCollection<string> Modulos { get; } =
            new ObservableCollection<string>
        {
            "DI",
            "SGE",
            "AD",
            "PSP",
            "PMDM",
            "EIE"
        };

        // Constructor: suscribe los eventos de la colección y sus tareas.
        public MainViewModel()
        {
            Tareas.CollectionChanged += Tareas_CollectionChanged;

            foreach (Tarea tarea in Tareas)
            {
                tarea.PropertyChanged += Tarea_PropertyChanged;
            }
        }

        // Porcentaje de tareas completadas.
        public int ProgresoPorcentaje
        {
            get
            {
                if (Tareas.Count == 0)
                {
                    return 0;
                }

                int tareasCompletadas =
                    Tareas.Count(tarea => tarea.Completada);

                return (int)Math.Round(
                    (double)tareasCompletadas / Tareas.Count * 100);
            }
        }

        // Texto informativo del progreso.
        public string TextoProgreso
        {
            get
            {
                int completadas =
                    Tareas.Count(tarea => tarea.Completada);

                return $"{completadas} de {Tareas.Count} completadas ({ProgresoPorcentaje}%)";
            }
        }

        // Número de tareas pendientes.
        public int TareasPendientesCount
        {
            get
            {
                return Tareas.Count(tarea => !tarea.Completada);
            }
        }

        // Mensaje de estado de la aplicación.
        public string EstadoApp { get; private set; } =
            "Listo para operar";

        // Detecta cuando se añaden o eliminan tareas.
        private void Tareas_CollectionChanged(
            object? sender,
            NotifyCollectionChangedEventArgs e)
        {
            // Suscripción a las tareas nuevas.
            if (e.NewItems != null)
            {
                foreach (Tarea tarea in e.NewItems)
                {
                    tarea.PropertyChanged += Tarea_PropertyChanged;
                }
            }

            // Cancelación de la suscripción a las tareas eliminadas.
            if (e.OldItems != null)
            {
                foreach (Tarea tarea in e.OldItems)
                {
                    tarea.PropertyChanged -= Tarea_PropertyChanged;
                }
            }

            ActualizarResumen();
        }

        // Detecta los cambios en las propiedades de cada tarea.
        private void Tarea_PropertyChanged(
            object? sender,
            PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Tarea.Completada))
            {
                ActualizarResumen();
            }
        }

        // Notifica que deben actualizarse los valores del resumen.
        private void ActualizarResumen()
        {
            OnPropertyChanged(nameof(ProgresoPorcentaje));
            OnPropertyChanged(nameof(TextoProgreso));
            OnPropertyChanged(nameof(TareasPendientesCount));
        }

        public void AgregarTarea()
        {
            if (!PuedeAgregarTarea)
            {
                EstadoApp = "El título de la tarea es obligatorio";
                OnPropertyChanged(nameof(EstadoApp));
                return;
            }

            Tarea nuevaTarea = new Tarea
            {
                Titulo = NuevoTitulo.Trim(),
                Modulo = NuevoModulo,
                FechaLimite = NuevaFechaLimite,
                Prioridad = NuevaPrioridad,
                EsUrgente = NuevaTareaUrgente,
                Completada = false
            };

            Tareas.Add(nuevaTarea);

            NuevoTitulo = string.Empty;
            NuevoModulo = "DI";
            NuevaFechaLimite = null;
            NuevaPrioridad = "Media";
            NuevaTareaUrgente = false;

            EstadoApp = "Tarea agregada con éxito";
            OnPropertyChanged(nameof(EstadoApp));
        }


        public void EliminarTarea(Tarea tarea)
        {
            if (Tareas.Remove(tarea))
            {
                EstadoApp = "Tarea eliminada correctamente";
                OnPropertyChanged(nameof(EstadoApp));
            }
        }

        public void FormularioLimpio()
        {
            NuevoTitulo = string.Empty;
            NuevoModulo = "DI";
            NuevaFechaLimite = null;
            NuevaPrioridad = "Media";
            NuevaTareaUrgente = false;

            EstadoApp = "Formulario limpio";
            OnPropertyChanged(nameof(EstadoApp));
        }

        // Evento de notificación de cambios en las propiedades.
        public event PropertyChangedEventHandler? PropertyChanged;

        // Lanza la notificación de una propiedad modificada.
        protected void OnPropertyChanged(
            [CallerMemberName] string? nombrePropiedad = null)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(nombrePropiedad));
        }
    }
}
