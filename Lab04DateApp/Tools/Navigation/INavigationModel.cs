namespace KMA.ProgrammingInCSharp2019.Lab01.DateApp.Tools.Navigation
{
    internal enum ViewType
    {
        DbList
    }

    interface INavigationModel
    {
        void Navigate(ViewType viewType);

        void DeNavigate(ViewType viewType);
    }
}
