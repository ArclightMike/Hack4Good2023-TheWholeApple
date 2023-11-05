using MediatR;
using GoodDadsAPI.Services.Models;
using GoodDadsAPI.Services.Repositories;
using GoodDadsAPI.Services.Schema;

namespace GoodDads.Services.Intake.SaveIntakeInformation
{
    public sealed class SaveIntakeRequest : IRequest<SaveIntakeResponse>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address1  { get; set; }

        public string? Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string PrimaryPhone { get; set; }

        public string WorkPhone { get; set; }

        public string AlternateContact { get; set; }

        public string Email { get; set; }

        public List<DependentRequestModel> Dependents { get; set; } = new List<DependentRequestModel>();

        public MaritalStatus MaritalStatus { get; set; }
    }

    public sealed class SaveIntakeResponse
    {
        public bool Success { get; set; }
    }

    public sealed class SaveIntakeRequestHandler : IRequestHandler<SaveIntakeRequest, SaveIntakeResponse>
    {
        private readonly IDadRepository dadRepository;
        private readonly IDependentRepository dependentRepository;
        private readonly IAddressRepository addressRepository;

        public SaveIntakeRequestHandler(IDadRepository dadRepository, IDependentRepository dependentRepository, IAddressRepository addressRepository)
        {
            this.dadRepository = dadRepository;
            this.dependentRepository = dependentRepository;
            this.addressRepository = addressRepository;
        }

        public async Task<SaveIntakeResponse> Handle(SaveIntakeRequest request, CancellationToken cancellationToken)
        {

            #region Not sure we need the service models anymore. Left the map here in this region.
            //var dadServiceModel = new DadServiceModel
            //{
            //    FirstName = request.FirstName,
            //    LastName = request.LastName,
            //    PrimaryPhone = request.PrimaryPhone,
            //    WorkPhone = request.WorkPhone,
            //    Email = request.Email,
            //    Address1 = request.Address1,
            //    Address2 = request.Address2,
            //    City = request.City,
            //    State = request.State,
            //    PostalCode = request.PostalCode,
            //};

            //var dependents = new List<DependentServiceModel>();
            //foreach (var requestDependent in request.Dependents)
            //{
            //    dependents.Add(new DependentServiceModel
            //    {
            //        BirthDate = requestDependent.BirthDate,
            //        SupportPerMonth = requestDependent.SupportPerMonth,
            //        Dad = dadServiceModel,
            //    });
            //}

            //dadServiceModel.Dependents = dependents;

            //var dadInsertData = new Dad
            //{
            //    FirstName = dadServiceModel.FirstName,
            //    LastName = dadServiceModel.LastName,
            //    Phone = dadServiceModel.PrimaryPhone,
            //    WorkPhone = dadServiceModel.WorkPhone,
            //    Email = dadServiceModel.Email,
            //};
            #endregion
            
            //Insert dad/user data.
            var dadInsertData = new Dad
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Phone = request.PrimaryPhone,
                WorkPhone = request.WorkPhone,
                Email = request.Email,
            };
            var dadKey = await dadRepository.Insert(dadInsertData);

            //Insert address info
            var addressInsertData = new Address
            {
                UserID = dadKey,
                LineOne = request.Address1,
                LineTwo = request.Address2 ?? string.Empty,
                City = request.City,
                State = request.State,
                Zip = request.PostalCode,
            };
            var addressKey = await addressRepository.Insert(addressInsertData);

            //Insert dependent info
            foreach (var requestDependent in request.Dependents)
            {
                var dadDependentData = new Dependent
                {
                    BirthDate = requestDependent.BirthDate,
                    ContactStatusId = 1,
                    SupportPerMonth = requestDependent.SupportPerMonth,
                    UserId = dadKey,
                };

                await dependentRepository.Insert(dadDependentData);
            }

            return new SaveIntakeResponse
            {
                Success = true,  //Could probably just be a 200 no content
            };
        }
    }
}

