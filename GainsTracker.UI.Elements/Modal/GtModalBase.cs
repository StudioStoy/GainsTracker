using Microsoft.AspNetCore.Components;

namespace GainsTracker.UI.Elements.Modal;

public class GtModalBase : ComponentBase
{
    protected bool IsClosing;
    [Inject] protected ModalService ModalService { get; set; } = null!;

    public async Task HandleCloseModal()
    {
        IsClosing = true;
        await Task.Delay(ModalService.ModalAnimationDelay);
        ModalService.CloseLast();
    }
}
