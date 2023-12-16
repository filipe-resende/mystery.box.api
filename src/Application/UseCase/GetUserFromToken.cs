namespace Application.UseCase
{
    using Domain.DTO;
    using Domain.Interfaces.Repositories;
    using Microsoft.AspNetCore.Http;
    using System.Security.Claims;

    public class GetUserFromToken: IGetUserFromToken
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IUserRepository _repository;

        public GetUserFromToken(IHttpContextAccessor contextAccessor, IUserRepository _repository )
        {
            this.contextAccessor = contextAccessor;
            this._repository = _repository;
        }

        public async Task<UserDTO> GetUserIdFromToken()
        {
            // Acesse as reivindicações do token JWT através do HttpContext
            var sid = 
                contextAccessor.
                HttpContext?.
                User?.
                Claims?.
                FirstOrDefault(c => c.Type == ClaimTypes.Sid)?
                .Value;

            if (sid != null)
            {
                var id = Guid.Parse(sid);
                return await _repository.GetById(id);
            }
            else
            {
                return null;
            }
        }
    }

}
