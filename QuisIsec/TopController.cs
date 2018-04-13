namespace QuisIsec
{
    public class TopController : Controller
    {
        private IView _view;

        public override IView View
        {
            get => _view ?? new QuisIsec();
        }

        public override bool Loadable()
        {
            return true;
        }
    }
}