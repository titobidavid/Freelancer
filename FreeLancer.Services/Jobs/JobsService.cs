using FreeLancer.Data;
using FreeLancer.Models.Models.Domain;
using FreeLancer.Models.Models.DTO;
using FreeLancer.Models.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResponseModelTitobi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeLancer.Services.Jobs
{
    public class JobsService : IJobService
	{
		public readonly AppDbContext _db;
		public readonly ILogger<JobsService> _logger;
		public JobsService(AppDbContext db, ILogger<JobsService> logger)
		{
			_db = db;
			_logger = logger;
		}
		public async Task<ResponseModel<Job>> CreateJob(JobDTO model)
		{
			ResponseModel<Job> response = new();
			try
			{
				if(model == null)
				{
					response.Message = "Empty Object";
					response.IsSuccessful = false;
					response.ErrorCodes = ErrorCodes.DataNotFound;
					return response;
				}
                int skill = _db.Skills.AsNoTracking().SingleOrDefault(x => x.SkillName.ToString() == model.SkillName).Id;
				_db.Add(new Job
				{
					PaymentAmount = model.PaymentAmount,
					Description = model.Description,
					Duration = model.Duration,
					Id = model.Id,
					SkillLevel = (Experience)Enum.Parse(typeof(Experience), model.SkillLevel),
                    SkillId = skill,
				});

                await _db.SaveChangesAsync();
				response.Message = "Added Successfully";
				response.IsSuccessful = true;
				response.ErrorCodes = ErrorCodes.Successful;
				return response;
			}
			catch (Exception ex)
			{
				_logger.LogError("Failed to execute");
				response.Message = string.Format("Error is {0}", ex.Message);
				response.IsSuccessful = false;
				response.ErrorCodes = ErrorCodes.Failed;
				return response;

			}
		}

		public async Task<ResponseModel<Job>> DeleteJob(int jobId)
		{
			ResponseModel<Job> response = new();
			try
			{
				var result = await _db.Jobs.SingleOrDefaultAsync(x => x.Id == jobId);
				if(result == null)
				{
					response.Message = "Error";
					response.IsSuccessful = false;
					response.ErrorCodes = ErrorCodes.ServerError;
					return response;
				}
				_db.Jobs.Remove(result);
				await _db.SaveChangesAsync();
				response.Message = "Deleted Successfully";
				response.IsSuccessful = true;
				response.ErrorCodes = ErrorCodes.Successful;
				return response;
			}
			catch (Exception ex)
			{
				_logger.LogError("Failed to execute");
				response.Message = string.Format("Error is {0}", ex.Message);
				response.IsSuccessful = false;
				response.ErrorCodes = ErrorCodes.Failed;
				return response;

			}
		}

		public async Task<ResponseModel<JobDTO>> GetJobById(int jobId)
		{
			ResponseModel<JobDTO> response = new();
			try
			{
				var result = await _db.Jobs.Select(x=>new JobDTO
				{
                    PaymentAmount = x.PaymentAmount,
                    Description = x.Description,
                    Duration = x.Duration,
                    SkillLevel = x.SkillLevel.ToString(),
                    Id = jobId,
                    SkillName = _db.Skills.FirstOrDefault(u => u.Id == x.Id).SkillName
                }).FirstOrDefaultAsync(x => x.Id == jobId);
                if (result == null)
                {
                    response.Message = "Error";
                    response.IsSuccessful = false;
                    response.ErrorCodes = ErrorCodes.ServerError;
                    return response;
                }
				response.data = result;
				response.Message = "Successfull";
				response.IsSuccessful = true;
				response.ErrorCodes = ErrorCodes.Successful;
				return response;
			}
			catch (Exception ex)
			{
				_logger.LogError("Failed to execute");
				response.Message = string.Format("Error is {0}", ex.Message);
				response.IsSuccessful = false;
				response.ErrorCodes = ErrorCodes.Failed;
				return response;

			}
		}

        public async Task<ResponseModel<JobDTO>> GetJobById(string jobName)
        {
            ResponseModel<JobDTO> response = new();
            try
            {
                var result = await _db.Jobs.Select(x => new JobDTO
                {
                    PaymentAmount = x.PaymentAmount,
                    Description = x.Description,
                    Duration = x.Duration,
                    SkillLevel = x.SkillLevel.ToString(),
                    SkillName = _db.Skills.FirstOrDefault(u => u.Id == x.Id).SkillName
                }).FirstOrDefaultAsync(x => x.SkillName.Contains(jobName));
                if (result == null)
                {
                    response.Message = "Error";
                    response.IsSuccessful = false;
                    response.ErrorCodes = ErrorCodes.ServerError;
                    return response;
                }
                response.data = result;
                response.Message = "Successfull";
                response.IsSuccessful = true;
                response.ErrorCodes = ErrorCodes.Successful;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to execute");
                response.Message = string.Format("Error is {0}", ex.Message);
                response.IsSuccessful = false;
                response.ErrorCodes = ErrorCodes.Failed;
                return response;

            }
        }

        public async Task<ResponseModel<IEnumerable<JobDTO>>> GetJobs()
		{
			ResponseModel<IEnumerable<JobDTO>> response = new();
			try
			{
				var result = await (from x in _db.Jobs
							 join s in _db.Skills on x.SkillId equals s.Id
							 select new JobDTO()
							 {
								 Id = x.Id,
								 SkillLevel = x.SkillLevel.ToString(),
								 SkillName = s.SkillName,
								 Description = x.Description,
								 Duration = x.Duration,
								 PaymentAmount = x.PaymentAmount,
							 }).ToListAsync();
				if (result == null)
				{
					response.Message = "Error";
					response.IsSuccessful = false;
					response.ErrorCodes = ErrorCodes.ServerError;
					return response;
				}
				_logger.LogInformation($"{result.Count} objects were found successfully");
                response.data = result;
				response.Message = "Successfull";
				response.IsSuccessful = true;
				response.ErrorCodes = ErrorCodes.Successful;
				return response;
			}
			catch (Exception ex)
			{
				_logger.LogError("Failed to execute");
				response.Message = string.Format("Error is {0}", ex.Message);
				response.IsSuccessful = false;
				response.ErrorCodes = ErrorCodes.Failed;
				return response;

			}
		}

		public async Task<ResponseModel<Job>> ApplyJob(JobDTO model, int Id)
		{
			ResponseModel<Job> response = new();
			try
			{
				if (model == null)
				{
					response.Message = "Error";
					response.IsSuccessful = false;
					response.ErrorCodes = ErrorCodes.ServerError;
					return response;
				}
				var result = await _db.Jobs.FirstOrDefaultAsync(x => x.Id == Id);
				if (result == null)
				{
					response.Message = "Error";
					response.IsSuccessful = false;
					response.ErrorCodes = ErrorCodes.ServerError;
					return response;
				}
				result.SkillLevel = (Experience)Enum.Parse(typeof(Experience), model.SkillLevel);
				result.Description = model.Description;
				result.Duration = model.Duration;
				result.SkillId = _db.Skills.SingleOrDefaultAsync(x => x.SkillName == model.SkillName).Id;
				result.PaymentAmount = model.PaymentAmount;
				await _db.SaveChangesAsync();
				response.Message = "Updated Successfully";
				response.IsSuccessful = true;
				response.ErrorCodes = ErrorCodes.Successful;
				return response;
			}
			catch (Exception ex)
			{
				_logger.LogError("Failed to execute");
				response.Message = string.Format("Error is {0}", ex.Message);
				response.IsSuccessful = false;
				response.ErrorCodes = ErrorCodes.Failed;
				return response;

			}
		}
            public async Task<ResponseModel<Job>> UpdateJob(JobDTO model, int Id)
            {
                ResponseModel<Job> response = new();
                try
                {
                    if (model == null)
                    {
                        response.Message = "Error";
                        response.IsSuccessful = false;
                        response.ErrorCodes = ErrorCodes.ServerError;
                        return response;
                    }
                    var result = await _db.Jobs.FirstOrDefaultAsync(x => x.Id == Id);
                    if (result == null)
                    {
                        response.Message = "Error";
                        response.IsSuccessful = false;
                        response.ErrorCodes = ErrorCodes.ServerError;
                        return response;
                    }
                    result.SkillLevel = (Experience)Enum.Parse(typeof(Experience), model.SkillLevel);
                    result.Description = model.Description;
                    result.Duration = model.Duration;
                    result.SkillId = _db.Skills.SingleOrDefaultAsync(x => x.SkillName == model.SkillName).Id;
                    result.PaymentAmount = model.PaymentAmount;
                    await _db.SaveChangesAsync();
                    response.Message = "Updated Successfully";
                    response.IsSuccessful = true;
                    response.ErrorCodes = ErrorCodes.Successful;
                    return response;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Failed to execute");
                    response.Message = string.Format("Error is {0}", ex.Message);
                    response.IsSuccessful = false;
                    response.ErrorCodes = ErrorCodes.Failed;
                    return response;

                }
            }
	}
}
