using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;
using AppCoem.Mobile.DataAccess;
using AppCoem.Mobile.DTOs;
using AppCoem.Mobile.Utilidades;
using AppCoem.Mobile.Modelos;

namespace AppCoem.Mobile.ViewModels
{
    public partial class EAfiliadoViewModel : ObservableObject, IQueryAttributable
    {
        private readonly EAfiliadoDbContext _dbContext;

        [ObservableProperty]
        private EAfiliadoDTO eAfiliadoDto = new EAfiliadoDTO();

        [ObservableProperty]
        private string? tituloPagina;

        private int IdAfiliado;

        [ObservableProperty]
        private bool loadingEsVisible = false;

        public EAfiliadoViewModel(EAfiliadoDbContext context)
        {
            _dbContext = context;
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var id = int.Parse(query["id"].ToString());
            IdAfiliado = id;
            if (IdAfiliado == 0)
            {
                TituloPagina = "Nuevo Afiliado";
            }
            else
            {
                TituloPagina = "Editar Afiliado";
                LoadingEsVisible = true;
                await Task.Run(async () =>
                {
                    var encontrado = await _dbContext.EAfiliados.FirstAsync(e => e.IdAfiliado == IdAfiliado);
                    EAfiliadoDto.IdAfiliado = encontrado.IdAfiliado;
                    EAfiliadoDto.Idasoci = encontrado.Idasoci;
                    EAfiliadoDto.NroCI = encontrado.NroCI;
                    EAfiliadoDto.Nombres = encontrado.Nombres;
                    EAfiliadoDto.Apellidos = encontrado.Apellidos;
                    EAfiliadoDto.Direccion = encontrado.Direccion;
                    EAfiliadoDto.Celular = encontrado.Celular;
                    EAfiliadoDto.Estado = encontrado.Estado;

                    MainThread.BeginInvokeOnMainThread(() => { LoadingEsVisible = false; });
                });
            }
            //throw new NotImplementedException();
        }

        [RelayCommand]
        private async Task Guardar()
        {
            LoadingEsVisible = true;
            EAfiliadoMensaje mensaje = new EAfiliadoMensaje();

            await Task.Run(async () =>
            {
                if (IdAfiliado == 0)
                {
                    var tbEAfiliado = new EAfiliado
                    {
                        Idasoci = EAfiliadoDto.Idasoci,
                        NroCI = EAfiliadoDto.NroCI,
                        Nombres = EAfiliadoDto.Nombres,
                        Apellidos = EAfiliadoDto.Apellidos,
                        Direccion = EAfiliadoDto.Direccion,
                        Celular = EAfiliadoDto.Celular,
                        Estado = EAfiliadoDto.Estado,
                    };

                    _dbContext.EAfiliados.Add(tbEAfiliado);
                    await _dbContext.SaveChangesAsync();

                    EAfiliadoDto.IdAfiliado = tbEAfiliado.IdAfiliado;
                    mensaje = new EAfiliadoMensaje()
                    {
                        EsCrear = true,
                        EAfiliadoDto = EAfiliadoDto
                    };
                }
                else
                {
                    var encontrado = await _dbContext.EAfiliados.FirstAsync(e => e.IdAfiliado == IdAfiliado);
                    encontrado.Idasoci = EAfiliadoDto.Idasoci;
                    encontrado.NroCI = EAfiliadoDto.NroCI;
                    encontrado.Nombres = EAfiliadoDto.Nombres;
                    encontrado.Apellidos = EAfiliadoDto.Apellidos;
                    encontrado.Direccion = EAfiliadoDto.Direccion;
                    encontrado.Celular = EAfiliadoDto.Celular;
                    encontrado.Estado = EAfiliadoDto.Estado;

                    await _dbContext.SaveChangesAsync();

                    mensaje = new EAfiliadoMensaje()
                    {
                        EsCrear = false,
                        EAfiliadoDto = EAfiliadoDto
                    };

                }

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    LoadingEsVisible = false;
                    WeakReferenceMessenger.Default.Send(new EAfiliadoMensajeria(mensaje));
                    await Shell.Current.Navigation.PopAsync();
                });
            });
        }
    }
}
