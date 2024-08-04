using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AppCoem.Mobile.Utilidades
{
    public class EAfiliadoMensajeria : ValueChangedMessage<EAfiliadoMensaje>
    {
        public EAfiliadoMensajeria(EAfiliadoMensaje value) : base(value)
        {

        }
    }
}
