using Microsoft.AspNetCore.Components;

namespace RadzenExample.Pages;

public class ComponentBase<T> : ComponentBase
{
    protected bool IsInitializedSync { get; set; }

    protected bool IsInitializedAsync { get; set; }

    protected bool IsInitialized => IsInitializedAsync && IsInitializedSync;

    #region OnInitializedAsync

    protected virtual Task OnInitializedInternalAsync() => Task.CompletedTask;

    protected sealed override async Task OnInitializedAsync()
    {
        async Task InternalOnInitializedAsync()
        {
            await base.OnInitializedAsync().ConfigureAwait(false);
            await OnInitializedInternalAsync().ConfigureAwait(false);
        }

        await HandleAsync(InternalOnInitializedAsync, nameof(OnInitializedAsync)).ConfigureAwait(false);
        IsInitializedAsync = true;
    }

    #endregion

    #region OnInitialized

    protected virtual void OnInitializedInternal()
    {
    }

    protected sealed override void OnInitialized()
    {
        void InternalOnInitialized()
        {
            base.OnInitialized();
            OnInitializedInternal();
        }

        Handle(InternalOnInitialized, nameof(OnInitialized));
        IsInitializedSync = true;
    }

    #endregion

    #region OnParameterSetAsync

    protected virtual Task OnParametersSetInternalAsync() => Task.CompletedTask;

    protected sealed override async Task OnParametersSetAsync()
    {
        async Task InternalOnParametersSetAsync()
        {
            await base.OnParametersSetAsync().ConfigureAwait(false);
            await OnParametersSetInternalAsync().ConfigureAwait(false);
        }

        await HandleAsync(InternalOnParametersSetAsync, nameof(OnParametersSetAsync)).ConfigureAwait(false);
    }

    #endregion

    #region OnParameterSet

    protected virtual void OnParametersSetInternal()
    {
    }

    protected sealed override void OnParametersSet()
    {
        void InternalOnParametersSet()
        {
            base.OnParametersSet();
            OnParametersSetInternal();
        }

        Handle(InternalOnParametersSet, nameof(OnParametersSet));
    }

    #endregion

    #region OnParameterSetAsync

    protected virtual Task OnAfterRenderInternalAsync(bool firstRender) => Task.CompletedTask;

    protected sealed override async Task OnAfterRenderAsync(bool firstRender)
    {
        async Task InternalOnAfterRenderAsync()
        {
            await base.OnAfterRenderAsync(firstRender).ConfigureAwait(false);
            await OnAfterRenderInternalAsync(firstRender).ConfigureAwait(false);
        }

        await HandleAsync(InternalOnAfterRenderAsync, nameof(OnAfterRenderAsync)).ConfigureAwait(false);
    }

    #endregion

    #region OnParameterSet

    protected virtual void OnAfterRenderInternal(bool firstRender)
    {
    }

    protected sealed override void OnAfterRender(bool firstRender)
    {
        void InternalOnAfterRender()
        {
            base.OnAfterRender(firstRender);
            OnAfterRenderInternal(firstRender);
        }

        Handle(InternalOnAfterRender, nameof(OnAfterRender));
    }

    #endregion

    protected virtual Task CustomInvokeAsync(Action act) => InvokeAsync(act);
    protected virtual Task CustomInvokeAsync(Func<Task> func) => InvokeAsync(func);
    protected virtual Task CustomStateHasChangedAsync() => CustomInvokeAsync(StateHasChanged);

    protected void Handle(Action act, string actionName, string? errorSummaryText = null, string? errorDetailsText = null)
    {
        try
        {
            act();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    protected async Task HandleAsync(Func<Task> act, string actionName, string? errorSummaryText = null, string? errorDetailsText = null)
    {
        try
        {
            await act().ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}