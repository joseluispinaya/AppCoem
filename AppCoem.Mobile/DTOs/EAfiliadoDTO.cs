using CommunityToolkit.Mvvm.ComponentModel;

namespace AppCoem.Mobile.DTOs
{
    public partial class EAfiliadoDTO : ObservableObject
    {
        [ObservableProperty]
        public int idAfiliado;
        //[ObservableProperty]
        //public int idasoci;
        [ObservableProperty]
        public string? nroCI;
        [ObservableProperty]
        public string? nombres;
        [ObservableProperty]
        public string? apellidos;
        [ObservableProperty]
        public string? direccion;
        [ObservableProperty]
        public string? celular;
        [ObservableProperty]
        public bool estado;
    }
}
