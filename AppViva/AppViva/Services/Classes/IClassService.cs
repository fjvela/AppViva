using AppViva.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppViva.Services
{
    public interface IClassService
    {
        Task<IList<ClassModel>> GetClassAsync(string token, DateTime dateStart, int clubNo, int contractPersonId);

        Task<IList<ClassWeeklyModel>> GetClassWeeklyAsync(string token, DateTime dateStart, int clubNo, int contractPersonId);
        Task<ClassBookResult> AddBookingAsync(string token, int contractPersonId, int clubClassId, CancellationToken cancelToken, int? equipmentNo = null);
        Task<ClassBookResult> CancelBookingAsync(string token, int contractPersonId, int clubClassId, CancellationToken cancelToken);
        Task<IList<EquipmentModel>> GetListEquipmentForClass(string token, int clubClassId, CancellationToken cancelToken);

    }
}
