using Nancy;
using Nancy.ModelBinding;
using Web.Api.Modules.Root.Get;

namespace Web.Api.Modules.Root
{
    public class RootModule : NancyModule
    {
        public RootModule()
        {
            Get["/api"] = _ => GetRoot(this.BindAndValidate<GetRoot>());
        }

        private dynamic GetRoot(GetRoot request)
        {
            return new Get.Root(Context);
        }
    }
}
