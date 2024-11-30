using FreeLancer.Models.Models.Domain;
using FreeLancer.Models.Models.ViewModel;
using ResponseModelTitobi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeLancer.Services.Application.Interface
{
    public interface IApplication
    {
        public Task<ResponseModel<JobApplication>> CreateApplication(ApplyVM model);
        public Task<ResponseModel<JobApplication>> AcceptApplication(ApplyVM model, int Id);

        public ResponseModel<IEnumerable<JobApplication>> ListApplicant();
    }
}
