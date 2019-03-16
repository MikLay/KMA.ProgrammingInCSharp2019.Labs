using System;
using KMA.ProgrammingInCSharp2019.Lab01.DateApp.Views;
using KMA.ProgrammingInCSharp2019.Lab01.DateApp.Views.Submited;

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
                case ViewType.Input:
                    ViewsDictionary.Add(viewType, new InputView());
                    break;
                case ViewType.Processed:
                    ViewsDictionary.Add(viewType, new InfoView());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType), viewType, null);
            }
        }
    }
}
