
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DAM2_DI_UD03_P01.Models
{
    public class Tarea : INotifyPropertyChanged
    {
        private string _titulo = string.Empty;
        private string _modulo = string.Empty;
        private DateTime? _fechaLimite;
        private string _prioridad = "Media";
        private bool _esUrgente;
        private bool _completada;

        public string Titulo
        {
            get => _titulo;
            set
            {
                if (_titulo != value)
                {
                    _titulo = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Modulo
        {
            get => _modulo;
            set
            {
                if (_modulo != value)
                {
                    _modulo = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime? FechaLimite
        {
            get => _fechaLimite;
            set
            {
                if (_fechaLimite != value)
                {
                    _fechaLimite = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Prioridad
        {
            get => _prioridad;
            set
            {
                if (_prioridad != value)
                {
                    _prioridad = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool EsUrgente
        {
            get => _esUrgente;
            set
            {
                if (_esUrgente != value)
                {
                    _esUrgente = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool Completada
        {
            get => _completada;
            set
            {
                if (_completada != value)
                {
                    _completada = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(
            [CallerMemberName] string? nombrePropiedad = null)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(nombrePropiedad));
        }
    }
}

