namespace CSharpInteractive;

internal interface IPresenter<in T>
{
    public void Show(T data);
}