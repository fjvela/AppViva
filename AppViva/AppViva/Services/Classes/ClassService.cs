using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AppViva.Models;

namespace AppViva.Services
{
    public class ClassService : IClassService
    {
        private readonly IHttpRequestProvider requestProvider;

        public ClassService(IHttpRequestProvider requestProvider)
        {
            this.requestProvider = requestProvider;
        }

        public async Task<ClassBookResult> AddBookingAsync(string token, int contractPersonId, int clubClassId, CancellationToken cancelToken, int? equipmentNo = null)
        {
            UriBuilder uribuilder = new UriBuilder(GlobalSetting.Instance.APIEndPoint)
            {
                Path = "/Class/AddBooking",
            };

            return await requestProvider.PostAsync<ClassBookResult>(uribuilder.ToString(),
                new ClassBook { ContractPersonId = contractPersonId, ClubClassId = clubClassId, EquipmentNo = equipmentNo },
                cancelToken, token: token);
        }

        public async Task<ClassBookResult> CancelBookingAsync(string token, int contractPersonId, int clubClassId, CancellationToken cancelToken)
        {
            UriBuilder uribuilder = new UriBuilder(GlobalSetting.Instance.APIEndPoint)
            {
                Path = "/Class/CancelBooking",
            };

            return await requestProvider.PostAsync<ClassBookResult>(uribuilder.ToString(),
                new ClassBook { ContractPersonId = contractPersonId, ClubClassId = clubClassId },
                cancelToken, token: token);
        }

        public async Task<IList<ClassModel>> GetClassAsync(string token, DateTime dateStart, int clubNo, int contractPersonId)
        {
            UriBuilder uribuilder = new UriBuilder(GlobalSetting.Instance.APIEndPoint)
            {
                Path = "/Class/ListClubClassesForPerson", 
                Query = $"classDate={dateStart:yyyy-MM-dd}&clubNo={clubNo}&contractPersonId={contractPersonId}",
            };

            return await requestProvider.GetAsync<IList<ClassModel>>(uribuilder.ToString(), token: token);
        }

        public async Task<IList<ClassWeeklyModel>> GetClassWeeklyAsync(string token, DateTime dateStart, int clubNo, int contractPersonId)
        {
            
            UriBuilder uribuilder = new UriBuilder(GlobalSetting.Instance.APIEndPoint)
            {
                Path = "Class/ListClubClassesForPerson",
                Query = $"classDateFrom={dateStart:yyyy-MM-dd}&clubNo={clubNo}&contractPersonId={contractPersonId}&noOfDays=7&studioNo=0",
            };

            return await requestProvider.GetAsync<IList<ClassWeeklyModel>>(
                uribuilder.ToString(), 
                token: token);
        }

        public async Task<IList<EquipmentModel>> GetListEquipmentForClass(string token, int clubClassId, CancellationToken cancellationToken)
        {
            UriBuilder uribuilder = new UriBuilder(GlobalSetting.Instance.APIEndPoint)
            {
                Path = "/Class/ListEquipmentForClass",
                Query = $"clubClassId={clubClassId}",
            };

            return await requestProvider.GetAsync<IList<EquipmentModel>>(
                uribuilder.ToString(), 
                token: token,
                cancelToken:cancellationToken);
        }

    }
}
