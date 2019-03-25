namespace CostControl.API.Controllers.Base
{
	using System;
	using System.Collections.Generic;
	using System.Net;
	using System.Net.Http;
	using System.Threading.Tasks;
	using API.Models;
	using BusinessEntity.Models.Base.Interfaces;
	using BusinessLogic.Logics.Base;
	using Microsoft.AspNetCore.Mvc;

	public abstract class BaseApiController<TEntity, Service, Key> : ControllerBase, IDisposable
			where TEntity : class, IEntity<Key>, new()
			where Service : class, IGenericLogic<TEntity>, new()
	{
		protected IGenericLogic<TEntity> PDKBusinessLogic;

		public BaseApiController()
		{
			PDKBusinessLogic = new Service();
		}

		protected BaseApiController(IGenericLogic<TEntity> service)
		{
			PDKBusinessLogic = service;
		}

		[HttpGet("Get1")]
		public ActionResult<ServiceResponse<TEntity>> Get1(int id)
		{
			try
			{
				return GenerateResponse(null,
					PDKBusinessLogic.Get(pageSize: 10, page: id));
			}
			catch (Exception e)
			{
				return GenerateExceptionResponse(e, "Exception!");
			}
		}

		[HttpGet("Get")]
		public virtual ActionResult<ServiceResponse<TEntity>> Get([FromQuery]Pagination paginate = null, string token = "")
		{
			try
			{
				paginate = (paginate == null || paginate.PageSize <= 0) ? new Pagination() : paginate;
				paginate.TotalCount = PDKBusinessLogic.GetCount();

				return GenerateResponse(paginate,
					PDKBusinessLogic.Get(pageSize: paginate.PageSize, page: paginate.PageNumber));
			}
			catch (Exception e)
			{
				return GenerateExceptionResponse(e, "Exception!");
			}
		}

		[HttpGet]
		public async Task<ActionResult<ServiceResponse<TEntity>>> GetAsync(Pagination paginate, string searchKey = null)
		{
			try
			{
				paginate = (paginate == null || paginate.PageSize <= 0) ? new Pagination() : paginate;
				paginate.SearchKey = searchKey;
				paginate.TotalCount = await PDKBusinessLogic.GetCountAsync();

				return GenerateResponse(paginate,
					await PDKBusinessLogic.GetAsync(pageSize: paginate.PageSize, page: paginate.PageNumber));
			}
			catch (Exception e)
			{
				return GenerateExceptionResponse(e, "Exception!");
			}
		}

		[HttpGet("GetById")/*, Route("{id:long}")*/]
		public virtual ActionResult<ServiceResponse<TEntity>> GetById(Key id)
		{
			try
			{
				return GenerateResponse(null, PDKBusinessLogic.GetById(id));

				//Pagination paginate = new Pagination();

				//paginate.SearchKey = "id=" + id.ToString();
				////paginate.SortOrder = "id";
				//paginate.RowCount = 1;

				//var a = UniversalTypeConverter.ConvertTo<Key>(id);

				//Expression<Func<TEntity, bool>> f = t => t.Id.Equals(a);

				//PDKBusinessLogic.SingleOrDefault(i => i.Id.Equals(a));

				//return GenerateResponse(null,
				//    PDKBusinessLogic.Get(filter: i => i.Id.Equals(a), pageSize: paginate.PageSize, page: paginate.PageNumber)
				//    .SingleOrDefault());
			}
			catch (Exception e)
			{
				return GenerateExceptionResponse(e, "Exception!");
			}
		}

		[HttpGet]
		public async Task<ActionResult<ServiceResponse<TEntity>>> GetByIdAsync(Key id)
		{
			try
			{
				return GenerateResponse(null,
					new List<TEntity>() { await PDKBusinessLogic.GetByIdAsync(id) });
			}
			catch (Exception e)
			{
				return GenerateExceptionResponse(e, "Exception!");
			}
		}

		[HttpPost("Post")]
		public ActionResult<ServiceResponse<TEntity>> Post([FromBody]TEntity entity)
		{
			try
			{
				//entity.Id = 0;
				//entity.InstanceId = Guid.NewGuid();
				//entity.State = BusinessEntity.Models.Base.Enums.ObjectState.Active;

				//ModelState.Remove("Id");
				return GenerateResponse(null,
					new List<TEntity>() { PDKBusinessLogic.Add(entity) });
			}
			catch (Exception e)
			{
				return GenerateExceptionResponse(e, "Exception!");
			}
		}

		[HttpPost]
		public async Task<ActionResult<ServiceResponse<TEntity>>> PostAsync([FromBody]TEntity entity)
		{
			try
			{
				return GenerateResponse(null,
					new List<TEntity>() { await PDKBusinessLogic.AddAsync(entity) });
			}
			catch (Exception e)
			{
				return GenerateExceptionResponse(e, "Exception!");
			}
		}

		[HttpPost("Put")]
		public ActionResult<ServiceResponse<TEntity>> Put(Key id, [FromBody]TEntity entity)
		{
			if (entity != null /*&& id == entity.Id*/)
			{
				try
				{
					return GenerateResponse(null,
						new List<TEntity>() { PDKBusinessLogic.Update(entity) });
				}
				catch (Exception e)
				{
					return GenerateExceptionResponse(e, "Exception!");
				}
			}

			return GenerateExceptionResponse(new Exception("Model is null!"), "Exception!");
		}

		[HttpPost]
		public async Task<ActionResult<ServiceResponse<TEntity>>> PutAsync(Key id, [FromBody]TEntity entity)
		{
			if (entity != null /*&& id == entity.Id*/)
			{
				try
				{
					return GenerateResponse(null,
						new List<TEntity>() { await PDKBusinessLogic.UpdateAsync(entity) });
				}
				catch (Exception e)
				{
					return GenerateExceptionResponse(e, "Exception!");
				}
			}

			return GenerateExceptionResponse(new Exception("Model is null!"), "Exception!");
		}

		[HttpPost]
		[HttpPost("Delete")]
		public ActionResult<ServiceResponse<TEntity>> Delete(Key id)
		{
			if (id != null)
			{
				return GenerateResponse(null,
					new List<TEntity>() { PDKBusinessLogic.Remove(id) });
			}

			return GenerateExceptionResponse(new Exception("Model is null!"), "Exception!");
		}

		[HttpPost]
		public async Task<ActionResult<ServiceResponse<TEntity>>> DeleteAsync(Key id)
		{
			try
			{
				if (id == null)
				{
					return GenerateResponse(null, await PDKBusinessLogic.RemoveAsync(id));
				}

				return GenerateExceptionResponse(new Exception("Model is null!"), "Exception!");
			}
			catch (Exception e)
			{
				return GenerateExceptionResponse(e, "Exception!");
			}
		}

		private bool _disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					PDKBusinessLogic = null;
				}
			}
			_disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		~BaseApiController()
		{
			Dispose(true);
		}

		protected HttpResponseMessage CreateHttpResponse(HttpRequestMessage request, Func<HttpResponseMessage> function)
		{
			HttpResponseMessage response = null;

			try
			{
				response = function.Invoke();
			}
			//catch (DbUpdateException ex)
			//{
			//    LogError(ex);
			//    response = request.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException.Message);
			//}
			catch (Exception ex)
			{
				LogError(ex);
				response = //request.CreateResponse(HttpStatusCode.InternalServerError);
					new HttpResponseMessage()
					{
						StatusCode = HttpStatusCode.InternalServerError,
						Content = new StringContent(ex.Message, System.Text.Encoding.UTF8)
					};
			}

			return response;
		}

		private void LogError(Exception exception)
		{
			try
			{
				//ExceptionContext error = new ExceptionContext();
				//Error _error = new Error()
				//{
				//    Message = ex.Message,
				//    StackTrace = ex.StackTrace,
				//    DateCreated = DateTime.Now
				//};

				//_errorsRepository.Add(_error);
				//_unitOfWork.Commit();
			}
			catch { }
		}

		private void Validate()
		{
			//if (!ModelState.IsValid)
			//{
			//    throw new Exception("Is Not Valid");
			//}
		}

		protected ActionResult<ServiceResponse<TEntity>> GenerateResponse
			(Pagination paginate, IEnumerable<TEntity> entities)
		{
			return
				new ServiceResponse<TEntity>()
				{
					statusCode = HttpStatusCode.OK,
					data = entities,
					result = true,
					messages = null,
					totalRowCount = paginate != null ? paginate.TotalCount : 0,
					pageSize = paginate != null ? paginate.PageSize : 0,
					currentPage = paginate != null ? paginate.PageNumber : 0,
					totalPage = paginate != null ? paginate.TotalPages : 0,
					sortOrder = paginate != null ? paginate.SortOrder : null,
					sortDirection = paginate != null ? paginate.SortDirection : null,
					searchKey = paginate != null ? paginate.SearchKey : null,
					hasPreviousPage = paginate != null ? paginate.HasPreviousPage : false,
					hasNextPage = paginate != null ? paginate.HasNextPage : false
				};
		}

		protected ActionResult<ServiceResponse<TEntity>> GenerateResponse
			(Pagination paginate, TEntity entity)
		=> GenerateResponse(paginate, new List<TEntity> { entity });
				
		protected ActionResult<ServiceResponse<TEntity>> GenerateExceptionResponse(Exception exception, string defaultMessage)
		{
			var errMessage = (exception.InnerException?.Message ?? exception.Message) ?? defaultMessage;
			//ModelState.AddModelError(errMessage, errMessage);

			return
				new ServiceResponse<TEntity>()
				{
					statusCode = HttpStatusCode.ExpectationFailed,
					data = null,
					result = false,
					messages = new[]
							{
								defaultMessage,
								exception.Message,
								exception.InnerException?.Message
							}
				};
		}

		//public override Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext,
		//    CancellationToken cancellationToken)
		//{
		//    //var a =
		//    //        controllerContext.ControllerDescriptor.ControllerType + Environment.NewLine +
		//    //        controllerContext.ControllerDescriptor.ControllerName + Environment.NewLine +
		//    //        controllerContext.Request.ToString() + Environment.NewLine +
		//    //        controllerContext.RequestContext.ToString();

		//    return base.ExecuteAsync(controllerContext, cancellationToken);
		//}
	}
}