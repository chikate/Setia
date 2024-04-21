using Setia.Models.Base;
using Setia.Models.Gov;
using Setia.Services.Interfaces;

namespace Setia.Controllers
{
    public class TagsController(ICRUD<TagModel> CRUD) : CRUDController<TagModel>(CRUD);
    public class UsersController(ICRUD<UserModel> CRUD) : CRUDController<UserModel>(CRUD);
    public class UserTagsController(ICRUD<UserTagModel> CRUD) : CRUDController<UserTagModel>(CRUD);
    public class VotesController(ICRUD<VoteModel> CRUD) : CRUDController<VoteModel>(CRUD);
}