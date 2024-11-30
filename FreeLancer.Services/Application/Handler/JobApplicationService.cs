using FreeLancer.Data;
using FreeLancer.Models.Models.Domain;
using FreeLancer.Models.Models.ViewModel;
using FreeLancer.Services.Application.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResponseModelTitobi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeLancer.Services.Application.Handler
{
    public class JobApplicationService :IApplication
    {
        public readonly AppDbContext _db;
        public readonly ILogger<JobApplicationService> _logger;
        public JobApplicationService(AppDbContext db, ILogger<JobApplicationService> logger)
        {
            _db = db;
            _logger = logger;
        }
        public async Task<ResponseModel<JobApplication>> CreateApplication(ApplyVM model)
        {
            ResponseModel<JobApplication> responseModel = new();
            try
            {
                if(model == null)
                {
                    responseModel.IsSuccessful = false;
                    responseModel.Message = "Null value";
                    responseModel.ErrorCodes = ErrorCodes.ServerError;
                    return responseModel;
                }
                var result = new JobApplication
                {
                    Application = model.JobApplication,
                    FreelancerID = model.FreelancerID,
                    JobId = model.JobID,
                    TimeCreated = DateTime.Now,
                };
               
               
                await _db.AddAsync(result);
                await _db.SaveChangesAsync();
                responseModel.IsSuccessful = true;
                responseModel.Message = "Application created Successfully";
                responseModel.ErrorCodes = ErrorCodes.Successful;
                return responseModel;

            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message.ToString());
                responseModel.IsSuccessful = false;
                responseModel.Message = "An Error Occured";
                responseModel.ErrorCodes = ErrorCodes.Failed;
                return responseModel;

            }
        }

        public async Task<ResponseModel<JobApplication>> AcceptApplication(ApplyVM model, int Id)
        {
            ResponseModel<JobApplication> responseModel = new();
            try
            {
                if (model == null)
                {
                    responseModel.IsSuccessful = false;
                    responseModel.Message = "Null value";
                    responseModel.ErrorCodes = ErrorCodes.ServerError;
                    return responseModel;
                }
                var apply = await _db.JobApplications.SingleOrDefaultAsync(x=>x.Id == Id) ?? throw new ArgumentNullException();
                var job = await _db.Jobs.FirstOrDefaultAsync(x=>x.Id == model.JobID) ?? throw new ArgumentNullException();
                //var freelancer = await _db.freelancers.FirstOrDefaultAsync(x=>x.Id == model.FreelancerID) ?? throw new ArgumentNullException("freelancer");
                apply.IsApplied = true;
                //job.FreeLancer_Id = freelancer.Id;
                job.IsApplied = true;
                
                
                await _db.SaveChangesAsync();
                responseModel.IsSuccessful = true;
                responseModel.Message = "Application created Successfully";
                responseModel.ErrorCodes = ErrorCodes.Successful;
                return responseModel;

            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message.ToString());
                responseModel.IsSuccessful = false;
                responseModel.Message = "An Error Occured";
                responseModel.ErrorCodes = ErrorCodes.Failed;
                return responseModel;

            }
        }

        public ResponseModel<IEnumerable<JobApplication>> ListApplicant()
        {
            ResponseModel<IEnumerable<JobApplication>> responseModel = new();
            try
            {
                var result = _db.JobApplications.ToList();
                responseModel.data = result;
                responseModel.IsSuccessful = true;
                responseModel.Message = "Success";
                responseModel.ErrorCodes = ErrorCodes.Successful;
                return responseModel;

            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message.ToString());
                responseModel.IsSuccessful = false;
                responseModel.Message = "An Error Occured";
                responseModel.ErrorCodes = ErrorCodes.Failed;
                return responseModel;

            }
        }
    }
}
