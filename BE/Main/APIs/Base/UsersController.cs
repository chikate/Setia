using Main.Data.Models;
using Main.Services.Interfaces;
using Main.Standards.Controllers;

namespace Main.APIs.Base;

public class UsersController(ICRUD<AuditModel> CRUD) : CRUDController<AuditModel>(CRUD);