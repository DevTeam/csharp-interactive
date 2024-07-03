namespace CSharpInteractive.Core;

internal interface IPresenter<in T>
{
    public void Show(T data);
}