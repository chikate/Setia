using Base.Gateways.Bases;
using Setia.Models.Gov;
using Setia.Services.Interfaces;

namespace Setia.Controllers
{
    public class PontajController(ICRUD<PontajModel> CRUD) : CRUDController<PontajModel>(CRUD);
}