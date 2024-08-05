using AppCoem.Mobile.ViewModels;
namespace AppCoem.Mobile.Views;

public partial class EAfiliadoPage : ContentPage
{
	public EAfiliadoPage(EAfiliadoViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;

    }
}