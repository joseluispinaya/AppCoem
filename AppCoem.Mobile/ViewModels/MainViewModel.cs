using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;
using AppCoem.Mobile.DataAccess;
using AppCoem.Mobile.DTOs;
using AppCoem.Mobile.Utilidades;
using AppCoem.Mobile.Modelos;
using System.Collections.ObjectModel;
using AppCoem.Mobile.Views;

namespace AppCoem.Mobile.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly EAfiliadoDbContext _dbContext;

        [ObservableProperty]
        private ObservableCollection<EAfiliadoDTO> listaAfiliados = new ObservableCollection<EAfiliadoDTO>();

        public MainViewModel(EAfiliadoDbContext context)
        {
            _dbContext = context;

            MainThread.BeginInvokeOnMainThread(new Action(async () => await Obtener()));

            WeakReferenceMessenger.Default.Register<EAfiliadoMensajeria>(this, (r, m) =>
            {
                EAfiliadoMensajeRecibido(m.Value);
            });
        }

        public async Task Obtener()
        {
            var lista = await _dbContext.EAfiliados.ToListAsync();
            if (lista.Any())
            {
                foreach (var item in lista)
                {
                    ListaAfiliados.Add(new EAfiliadoDTO
                    {
                        IdAfiliado = item.IdAfiliado,
                        NroCI = item.NroCI,
                        Nombres = item.Nombres,
                        Apellidos = item.Apellidos,
                        Direccion = item.Direccion,
                        Celular = item.Celular,
                        Estado = item.Estado,
                    });
                }
            }
        }

        private void EAfiliadoMensajeRecibido(EAfiliadoMensaje eafiliadoMensaje)
        {
            var eafiliadoDto = eafiliadoMensaje.EAfiliadoDto!;

            if (eafiliadoMensaje.EsCrear)
            {
                ListaAfiliados.Add(eafiliadoDto);
            }
            else
            {
                var encontrado = ListaAfiliados
                    .First(e => e.IdAfiliado == eafiliadoDto.IdAfiliado);

                encontrado.NroCI = eafiliadoDto.NroCI;
                encontrado.Nombres = eafiliadoDto.Nombres;
                encontrado.Apellidos = eafiliadoDto.Apellidos;
                encontrado.Direccion = eafiliadoDto.Direccion;
                encontrado.Celular = eafiliadoDto.Celular;
                encontrado.Estado = eafiliadoDto.Estado;
            }
        }

        [RelayCommand]
        private async Task Crear()
        {
            var uri = $"{nameof(EAfiliadoPage)}?id=0";
            await Shell.Current.GoToAsync(uri);
        }

        [RelayCommand]
        private async Task Editar(EAfiliadoDTO eAfiliadoDto)
        {
            var uri = $"{nameof(EAfiliadoPage)}?id={eAfiliadoDto.IdAfiliado}";
            await Shell.Current.GoToAsync(uri);
        }

        [RelayCommand]
        private async Task Eliminar(EAfiliadoDTO eAfiliadoDto)
        {
            bool answer = await Shell.Current.DisplayAlert("Mensaje", "Desea eliminar el Afiliado?", "Si", "No");

            if (answer)
            {
                var encontrado = await _dbContext.EAfiliados
                    .FirstAsync(e => e.IdAfiliado == eAfiliadoDto.IdAfiliado);

                _dbContext.EAfiliados.Remove(encontrado);
                await _dbContext.SaveChangesAsync();
                ListaAfiliados.Remove(eAfiliadoDto);

            }

        }
    }
}
