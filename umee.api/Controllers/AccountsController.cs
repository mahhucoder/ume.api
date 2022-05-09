using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using umee.core.Entities;
using umee.core.Interfaces.Infrastructure;
using umee.core.Interfaces.Service;

namespace umee.api.Controllers
{
    public class AccountsController : UmeeBaseController<Account>
    {
        IAccountRepository _accountRepository;
        IAccountService _accountService;
        public AccountsController(IAccountRepository accountRepository, IAccountService accountService) : base(accountRepository, accountService)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet("firebaseUID")]
        public IActionResult GetAccout(string firebaseUID)
        {
            var res = _accountRepository.getAccount(firebaseUID);

            return Ok(res);
        }
    }
}
