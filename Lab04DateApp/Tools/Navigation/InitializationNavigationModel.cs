using System;
using KMA.ProgrammingInCSharp2019.Lab01.DateApp.Views;

namespace KMA.ProgrammingInCSharp2019.Lab01.DateApp.Tools.Navigation
{
    internal class InitializationNavigationModel : BaseNavigationModel
    {
        public InitializationNavigationModel(IContentOwner contentOwner) : base(contentOwner)
        {

        }

        protected override void InitializeView(ViewType viewType)
        {
            switch (viewType)
            {
                  case ViewType.DbList:
                    ViewsDictionary.Add(viewType, new DBListView());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType), viewType, null);
            }
        }

        protected override void DeInitializeView(ViewType viewType)
        {
            ViewsDictionary.Remove(viewType);
        }
    }
}
